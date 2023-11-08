using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehavior : EnemyBehavior
{

    #region Public Variables
    [SerializeField] protected float rangeDistance; // Minium Distance for range attack
    [SerializeField] protected Transform rangePosition;
    #endregion

    #region Private Variables
    #endregion

    

    #region Enemy Action

    protected override void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (rangeDistance >= distance && attackDistance < distance && !cooling && !cheackAnimationAttack())
        {
           AttackRange();
        }
        else if (distance > 4.5f && !cheackAnimationAttack())
        {
            Flip();
            StopAttack();
        }
        else if (distance <= 4.5f && !cheackAnimationAttack())
        {
            Stand();
        }
    }
    private void AttackRange()
    {
        AttackMode();
        Stand();
        animator.SetTrigger("Range");
    }

    public void shootArrow()
    {
        GameObject shootSpear = Instantiate(AttackMethod[1], rangePosition.position, Quaternion.identity);
        shootSpear.transform.position = rangePosition.position;
        shootSpear.GetComponent<Projectiles>().SetDirection(target);
        shootSpear.GetComponent<Projectiles>().Shoot();
    }

    protected override void Move()
    {
        Flip();
        animator.SetBool("Walk", true);
        if (!cheackAnimationAttack())
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            Vector2 direction = (targetPosition - rigi.position).normalized;
            rigi.velocity = new Vector2(direction.x * moveSpeed, rigi.velocity.y);
        }
    }


    #endregion

    #region Cooldown
    #endregion

    protected override bool cheackAnimationAttack()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");    }
}
