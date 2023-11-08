using UnityEngine;

public class RangerBehavior : EnemyBehavior
{

    [SerializeField] private Transform rangePosition;

    [SerializeField] private float changeTimer;
    [SerializeField] private float slideSpeed;
    [SerializeField] private Transform pivot;

    [SerializeField] private float skill1Timer;
    [SerializeField] private Collider2D AttackRain;
    [SerializeField] private int RainNumber = 2;


    [SerializeField] private float skill2Timer;


    private Transform changeTarget;
    private bool changeCooling;
    private float intChangeTimer;

    private float intskill1Timer;
    private bool skill1Cooling;

    private float intskill2Timer;
    private bool skill2Cooling;

    protected override void Awake()
    {
        base.Awake();
        intChangeTimer = changeTimer;
        intskill1Timer = skill1Timer;
        intskill2Timer = skill2Timer;
    }

    protected override void Update()
    {
        base.Update();

        if (changeCooling)
        {
            Cooldown(1);
        }

        if (skill1Cooling)
        {
            Cooldown(2);
        }

        if (skill2Cooling)
        {
            Cooldown(3);
        }
    }

    protected override bool cheackAnimationAttack()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Range") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Skill_1") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Skill_2") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Air") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Ready") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Smash") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Slide") ||
         animator.GetCurrentAnimatorStateInfo(0).IsName("Fall");
    }

    protected override void EnemyLogic()
    {
/*        Flip();
*/        distance = Vector2.Distance(transform.position, target.position);

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            float distanceTarget = Vector2.Distance(transform.position, changeTarget.position);
            if (distanceTarget <= attackDistance)
            {
                StopSlide();
            }
            SlideRun();
        }

        if (!cheackAnimationAttack() && !cooling)
        {
            int skillNum = UnityEngine.Random.Range(1, 4);
            switch (skillNum)
            {
                default:
                    break;
                case 1:
                    if (!skill1Cooling)
                    {
                        Skill1();
                        return;
                    }
                    break;
                case 2:
                    if (!skill2Cooling)
                    {
                        Skill2();
                        return;
                    }
                    break;
            }

        }
        if(distance < attackDistance && !cheackAnimationAttack() && !changeCooling) {
            Flip();
            int skillNum = UnityEngine.Random.Range(1, 3);
            changeTarget = pivot.position.x - transform.position.x < 0 ? leftLimit : rightLimit;
            switch (skillNum)
            {
                default:
                    break;
                case 1:
                        Slide();
                        return;
                case 2:
                        AirAttack();
                        return;
            }
        }else if (distance > attackDistance * 3 && !cheackAnimationAttack())
        {
            Flip();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }
        else if (attackDistance < distance && cooling == false)
        {
            AttackRange();
        }
        else if (attackDistance * 4 >= distance && !cheackAnimationAttack())
        {
            Stand();
        }
    }

    private void Skill2()
    {
        AttackMode();
        animator.SetTrigger("Skill2");
    }

    private void Skill1()
    {
        AttackMode();
        animator.SetTrigger("Skill1");
    }

    private void StopSlide()
    {
        animator.SetBool("Slide", false);
        TriggerSkill(1);
    }

    private void SlideRun()
    {
        Vector2 targetPosition = new Vector2(changeTarget.position.x, transform.position.y);
        Vector2 direction = (targetPosition - rigi.position).normalized;
        rigi.velocity = new Vector2(direction.x * moveSpeed * slideSpeed, rigi.velocity.y);
    }

    public void Jump()
    {
        var distance = changeTarget.position - transform.position;
        float jumpHeight = Mathf.Clamp(distance.y * 2, 6, float.MaxValue); ;
        float gravity = Mathf.Abs(Physics2D.gravity.y);
        float initialVelocityY = Mathf.Sqrt(2 * gravity * jumpHeight);
        float time = (initialVelocityY + Mathf.Sqrt(initialVelocityY * initialVelocityY - 2 * gravity * distance.y)) / gravity; ;
        float speed = distance.x / time;
        rigi.velocity = new Vector2(speed, initialVelocityY);
    }

    private void AirAttack()
    {
        AttackMode();
        animator.SetTrigger("Air");
    }

    private void Slide()
    {
        AttackMode();
        animator.SetBool("Slide", true);
    }

    private void AttackRange()
    {
        AttackMode();
        animator.SetTrigger("AttackRange");
    }

    public void shootArrow()
    {
        GameObject shootSpear = Instantiate(AttackMethod[1], rangePosition.position, Quaternion.identity);
        shootSpear.transform.position = rangePosition.position;
        shootSpear.GetComponent<Projectiles>().SetDirection(target);
        shootSpear.GetComponent<Projectiles>().Shoot();
    }

    public void shootRain()
    {
        GameObject shootSpear = Instantiate(AttackMethod[2], new Vector2(target.position.x,pivot.position.y), Quaternion.identity);
        shootSpear.transform.position = new Vector2( target.position.x,pivot.position.y);
        for(int i = 0; i < RainNumber; i++)
        {
            Bounds bounds = AttackRain.bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            GameObject rainArrow = Instantiate(AttackMethod[2], new Vector2(x, pivot.position.y), Quaternion.identity);
            rainArrow.transform.position = new Vector2(x, pivot.position.y);
        }
        
    }

    public void shootBeam()
    {
        GameObject shootSpear = Instantiate(AttackMethod[3], rangePosition.position, Quaternion.identity);
        shootSpear.transform.eulerAngles = transform.eulerAngles;
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

    public override void TriggerSkill(int skill)
    {
        switch (skill)
        {
            case 1:
                changeCooling = true; changeTimer = intChangeTimer; break;
            case 2:
                skill1Cooling = true; skill1Timer = intskill1Timer; break;
            case 3:
                skill2Cooling = true; skill2Timer = intskill2Timer; break;
            default: break;
        }
    }

    protected override void Cooldown(int skill)
    {
        switch (skill)
        {
            case 1:
                changeTimer -= Time.deltaTime;
                if (changeTimer <= 0 && changeCooling)
                {
                    changeCooling = false;
                    changeTimer = 0;
                }
                break;
            case 2:
                skill1Timer -= Time.deltaTime;
                if (skill1Timer <= 0 && skill1Cooling)
                {
                    skill1Cooling = false;
                    skill1Timer = 0;
                }
                break;
            case 3:
                skill2Timer -= Time.deltaTime;
                if (skill2Timer <= 0 && skill2Cooling)
                {
                    skill2Cooling = false;
                    skill2Timer = 0;
                }
                break;
            default:
                break;

        }
    }

}
