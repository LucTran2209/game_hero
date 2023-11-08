using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBehavior : EnemyBehavior
{
    protected override void EnemyLogic()
    {
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
    protected override bool cheackAnimationAttack()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
    }
}
