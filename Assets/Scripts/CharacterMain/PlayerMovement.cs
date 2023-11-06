using UnityEngine;
using System.Collections;
using System;
using Assets.Scripts.CharacterMain;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {


        [SerializeField] float m_speed = 4.0f;
        [SerializeField] public float m_jumpForce = 7.5f;
        [SerializeField] float m_rollForce = 6.0f;

        // Audio
        [SerializeField] AudioSource audioPlayerRun;
        [SerializeField] AudioSource audioPlayerJump;
        [SerializeField] AudioSource audioPlayerRoll;
        [SerializeField] AudioSource audioPlayerGetItem;


        private Animator m_animator;
        private Rigidbody2D m_body2d;
        private SensorPlayer m_groundSensor;
        private PlayerHealth m_playeHealth;

        private bool m_isGrounded = false;
        private bool m_isRolling = false;

        public int m_facingDirection = 1;
        private float m_delayToIdle = 0.0f;
        private readonly float m_rollDuration = 8f/14f;
        private float m_rollCurrentTime;
        [SerializeField] private Transform CheckWall;
        [SerializeField] private LayerMask wallMark;
        [SerializeField] private float checkLength;
        private RaycastHit2D hitWall;

        public Transform attackStartPosition;
        public GameObject powerBall;
        float fireRate = 0.5f;
        float nextFire = 0;

        // Use this for initialization
        void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_body2d = GetComponent<Rigidbody2D>();
            m_groundSensor = transform.Find("GroundSensor").GetComponent<SensorPlayer>();
            m_playeHealth = GetComponent<PlayerHealth>();
        }

        // Update is called once per frame
        void Update()
        {          
            // check if PlayerHeath is dead, stop move
            if (m_playeHealth.IsDead())
            {
                return;
            }


            // Increase timer that checks roll duration
            if (m_isRolling)
            {
                m_rollCurrentTime += Time.deltaTime;
                if (!checkPath())
                {
                    m_body2d.velocity = Vector2.zero;
                }
            }


            // Disable rolling if timer extends duration
            if (m_rollCurrentTime > m_rollDuration)
            {
                m_isRolling = false;
            }

            


            //Check if character just landed on the ground
            if (!m_isGrounded && m_groundSensor.State())
            {
                m_isGrounded = true;
                m_animator.SetBool("Grounded", m_isGrounded);
            }

            //Check if character just started falling
            if (m_isGrounded && !m_groundSensor.State())
            {
                m_isGrounded = false;
                m_animator.SetBool("Grounded", m_isGrounded);
            }
            RayCastDeBugger();

            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            Vector3 rotation = transform.eulerAngles;

            if (inputX > 0)
            {
                rotation.y = 0f;
                m_facingDirection = 1;
            }

            else if (inputX < 0)
            {
                rotation.y = 180f;
                m_facingDirection = -1;
            }
            transform.eulerAngles = rotation;
            // Move
            if (!m_isRolling)
            {
                if (inputX != 0 && m_isGrounded)
                {
                    audioPlayerRun.Play();
                }
                m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);
            }
            
            

            //Set AirSpeed in animator when jumping
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);

            // Attack powerball
            if (Input.GetKeyDown(KeyCode.L) && PlayerPrefs.GetInt(Key.Skill4) == 1)
            {
                m_animator.SetTrigger("Attack1" );
                FirePowerBall();
            }else
            // Block
            if (Input.GetMouseButtonDown(1) && !m_isRolling)
            {
                m_animator.SetTrigger("Block");
                m_animator.SetBool("IdleBlock", true);
            }

            else if (Input.GetMouseButtonUp(1))
                m_animator.SetBool("IdleBlock", false);

            // Roll
            else if (Input.GetKeyDown(KeyCode.K) && !m_isRolling)
            {
                audioPlayerRoll.Play();
                m_isRolling = true;
                m_rollCurrentTime = 0;
                m_animator.SetTrigger("Roll");
                if (!checkPath())
                {
                    m_body2d.velocity = Vector2.zero;
                }
                else
                {
                    m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, 0);
                }
            }

            //Jump
            else if ((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.W)) && m_isGrounded && !m_isRolling)
            {
                audioPlayerJump.Play();
                m_animator.SetTrigger("Jump");
                m_isGrounded = false;
                m_animator.SetBool("Grounded", m_isGrounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                //Debug.Log("run");
                m_delayToIdle = 0.05f;              
                m_animator.SetInteger("AnimState", 1);
            }

            //Idle
            else
            {
                // Prevents flickering transitions to idle
                m_delayToIdle -= Time.deltaTime;
                if (m_delayToIdle < 0)
                {
                    // Debug.Log(" stop run");
                    m_animator.SetInteger("AnimState", 0);
                }
            }
        }

        void FirePowerBall()
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                if (m_facingDirection == 1)
                {
                    Instantiate(powerBall, attackStartPosition.position, Quaternion.Euler(new Vector3(0, 0, 0)));
                }
                else
                {
                    Instantiate(powerBall, attackStartPosition.position, Quaternion.Euler(new Vector3(0, 0, 180)));
                }
            }
        }

        private bool checkPath()
        {
            if (m_facingDirection > 0)
            {
                hitWall = Physics2D.Raycast(CheckWall.position, Vector2.right, checkLength, wallMark);
            }
            else
            {
                hitWall = Physics2D.Raycast(CheckWall.position, Vector2.left, checkLength, wallMark);
            }
            return hitWall.collider == null;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "ItemHealth")
            {
                audioPlayerGetItem.Play();
            }
        }

        public void StopMove()
        {
            Debug.Log("Stop move");
            m_body2d.velocity = Vector2.zero;
        }

        private void RayCastDeBugger()
        {
            if (m_facingDirection > 0)
            {
                Debug.DrawRay(CheckWall.position, Vector2.right * checkLength, Color.red);
            }
            else
            {
                Debug.DrawRay(CheckWall.position, Vector2.left * checkLength, Color.red);
            }
        }
    }
}

