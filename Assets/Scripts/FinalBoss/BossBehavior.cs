using System;
using UnityEngine;

public class BossBehavior : EnemyBehavior
{
    // Start is called before the first frame update
    #region Public Variables
    [SerializeField] private float smashTimer;
    [SerializeField] private float smashDistance;

    [SerializeField] private float breathTimer;
    [SerializeField] private float breathDistance;

    [SerializeField] private float castTimer;
    [SerializeField] private float castDistance;
    [SerializeField] private Transform castPosition;
    
    #endregion

    #region Private Variables
    private bool smashCooling;
    private float intSmashTimer;
    private float smashHeight;

    private bool breathCooling;
    private float intBreathTimer;

    private bool castCooling;
    private float intCastTimer;
    private GameObject fireball;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        intSmashTimer = smashTimer;
        intBreathTimer = breathTimer;
        intCastTimer = castTimer;
    }

    protected override void Update()
    {
        base.Update();

        if (smashCooling)
        {
            Cooldown(1);
        }

        if (breathCooling)
        {
            Cooldown(2);
        }

        if(castCooling)
        {
            Cooldown(3);
        }
    }


    protected override void EnemyLogic()
    {
        Flip();
        int skillNum = UnityEngine.Random.Range(1, 4);
        if (!cheackAnimationAttack() && !cooling)
        {
            switch (skillNum)
            {
                default:
                    break;
                case 1:
                    if (distance >= smashDistance && !smashCooling)
                    {
                        Smash();
                        return;
                    }
                    break;
                case 2:
                    if (distance <= breathDistance && !breathCooling)
                    {
                        Breath();
                        return;
                    }
                    break;
                case 3:
                    if (distance >= attackDistance && distance <= castDistance && !castCooling)
                    {
                        Cast();
                        return;
                    }
                    break;
            }

        }

        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance && !cheackAnimationAttack())
        {
            Flip();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        else if (attackDistance >= distance && !cheackAnimationAttack())
        {
            Stand();
        }
    }

    private void Cast()
    {
        AttackMode();
        animator.SetTrigger("Cast");
    }

    public void CreateFireBall()
    {
        fireball = null;
        fireball = Instantiate(AttackMethod[1], castPosition.position, Quaternion.identity);
        fireball.GetComponent<Projectiles>().SetDirection(new Vector2(target.position.x-transform.position.x, 0));
    }

    public void shootFireBall()
    {
        try
        {
            fireball.GetComponent<Projectiles>().Shoot();

        }
        catch (MissingReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }

    private void Breath()
    {
        AttackMode();
        animator.SetTrigger("Breath");
    }

    private void Smash()
    {
        AttackMode();
        animator.SetTrigger("Smash");


    }

    public void Jump()
    {
        var distance = target.position - transform.position;
        smashHeight = Math.Clamp(distance.y * 2, 3, float.MaxValue);
        Debug.Log($"smmashHeight: {smashHeight}");
        float gravity = Mathf.Abs(Physics2D.gravity.y);
        float initialVelocityY = MathF.Sqrt(2 * gravity * smashHeight);
        Debug.Log($"initialVelocityY: {initialVelocityY}");
        float time = (initialVelocityY + MathF.Sqrt(initialVelocityY * initialVelocityY - 2 * gravity * distance.y)) / gravity; ;
        Debug.Log($"time: {time}");
        float speed = distance.x / time;
        Debug.Log($"speed: {speed}");
        rigi.velocity = new Vector2(speed, initialVelocityY);
    }

    protected override void Move()
    {
        AttackMode();
        Flip();
        animator.SetBool("Walk", true);
        if (!cheackAnimationAttack())
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            Vector2 direction = (targetPosition - rigi.position).normalized;
            rigi.velocity = new Vector2(direction.x * moveSpeed, rigi.velocity.y);
        }
    }

    #region Cooldown
    protected override void Cooldown(int skill)
    {
        switch (skill)
        {
            case 1:
                smashTimer -= Time.deltaTime;
                if (smashTimer <= 0 && smashCooling)
                {
                    smashCooling = false;
                    smashTimer = 0;
                }
                break;
            case 2:
                breathTimer -= Time.deltaTime;
                if (breathTimer <= 0 && breathCooling)
                {
                    breathCooling = false;
                    breathTimer = 0;
                }
                break;
            case 3:
                castTimer -= Time.deltaTime;
                if (castTimer <= 0 && castCooling)
                {
                    castCooling = false;
                    castTimer = 0;
                }
                break;

            default:
                break;

        }
    }
    public override void TriggerSkill(int skill)
    {
        switch (skill)
        {
            case 1:
                smashCooling = true; smashTimer = intSmashTimer; break;
            case 2:
                breathCooling = true; breathTimer = intBreathTimer; break;
            case 3:
                castCooling = true; castTimer = intCastTimer; break;
            default: break;
        }
    }
    #endregion

    protected override bool cheackAnimationAttack()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Cast") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Breath") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Ready") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Smash") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Fall")
         ;
    }
}
