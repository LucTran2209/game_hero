
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public DetectionZone zone;

    public float flySpeed;
    public float wayPointsReachedDistance = 0.1f;
    public List<Transform> wayPoints;
    
    bool _hasTarget = false;


    Transform nextWayPoint;
    public int waypointNum = 0;

    Animator _animator;
    Rigidbody2D rigi;
    Collider2D body;


    public bool HasTarget { 
        get { return _hasTarget; } 
        private set { 
            _hasTarget = value;
            _animator.SetBool("hasTarget", value);
        }
    }

    bool CanMove { get { return _animator.GetBool("canMove"); } }


    void Awake()
    {
        _animator = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        body = GetComponent<Collider2D>();
    }

    private void Start()
    {
        nextWayPoint = wayPoints[waypointNum];
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = zone.getDetectionColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rigi.velocity = Vector2.zero;
            }
        }
        
            
    }

    private void Flight()
    {
        Vector2 direction = (nextWayPoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWayPoint.position, transform.position);

        rigi.velocity = direction * flySpeed;
        Flip();

        if(distance < wayPointsReachedDistance)
        {
            waypointNum++;
            if(waypointNum>= wayPoints.Count)
            {
                waypointNum = 0;
            }
            nextWayPoint = wayPoints[waypointNum];
        }
    }

    private void Flip()
    {
        Vector2 localScale = transform.localScale;
        if ((rigi.velocity.x < 0 && localScale.x > 0) || (rigi.velocity.x > 0 && localScale.x < 0))
        {
            transform.localScale = new Vector2(-1 * localScale.x, localScale.y);
        }
    }

    private void OnTriggerStay2D(Collider2D trig)
    {
        //Set enemies to pass through each other
        if (trig.tag == "Monster")
        {
            Physics2D.IgnoreCollision(body, trig.GetComponent<Collider2D>());
        }
    }
}
