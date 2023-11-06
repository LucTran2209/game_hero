using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LancerBehavior : EnemyBehavior
{
    #region Public Variables
    [SerializeField] protected Transform rangePosition;
    #endregion

    #region Private Variables

    #endregion

    protected override void Awake()
    {
        base.Awake();
        AttackMethod[0].GetComponent<HitBoxCustomeTrigger>().onCustomTriggerEnter += CustomOnTriggerEnter2D;
    }

    private void CustomOnTriggerEnter2D(Collider2D d)
    {
        if (d.tag == "Player")
        {
            animator.SetBool("Combo", true);
        }
    }

    protected override void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance && !cheackAnimationAttack())
        {
            Flip();
            StopAttack();
            animator.SetBool("Combo", false);

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

    public void throwLance()
    {
        GameObject shootSpear = Instantiate(AttackMethod[1], rangePosition.position, Quaternion.identity);
        shootSpear.transform.position = rangePosition.position;
        shootSpear.GetComponent<Projectiles>().SetDirection(target);
        shootSpear.GetComponent<Projectiles>().Shoot();
    }

    protected override bool cheackAnimationAttack()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3");
    }
}
