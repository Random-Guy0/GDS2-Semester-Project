using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : AttackHandler
{
    Transform target;
    Rigidbody2D rb;
    [SerializeField] private float stopDistance = 2.0f;
    private float yPositionTolerance = 0.5f; // Tolerance for Y position check

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.Player.transform;
    }

    void Update()
    {
        

        // Check if the Grunt is in the same Y position as the player within the tolerance
        if (CanAttack())
        {
           DoAttack();
        }
    }

    public override Vector2 GetDirection()
    {
        float direction;
        if (this.transform.position.x - target.position.x < 0)
        {
            direction = 1.0f;
        }
        else
        {
            direction = -1.0f;
        }
        return Vector2.right * direction;
    }

    protected virtual bool CanAttack()
    {
        float yPositionDifference = Mathf.Abs(target.position.y - transform.position.y);
        return yPositionDifference <= yPositionTolerance &&
               Mathf.Abs(target.position.x - transform.position.x) < stopDistance
               && !CurrentlyAttacking;
    }

    public override void InterruptAttack()
    {
        if (!CurrentlyAttacking)
        {
            base.InterruptAttack();
        }
    }
    
    protected virtual void DoAttack()
    {
        if (MeleeAttacks.Length > 0)
        {
            base.DoMeleeAttack();
        }
        else if(RangedAttacks.Length > 0)
        {
            base.DoRangedAttack();
        }
    }
}