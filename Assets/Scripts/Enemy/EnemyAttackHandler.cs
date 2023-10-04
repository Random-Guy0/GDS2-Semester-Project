using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackHandler : AttackHandler
{
    Transform target;
    Rigidbody2D rb;
    private float stopDistance = 2.0f;
    private float yPositionTolerance = 0.5f; // Tolerance for Y position check

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.Player.transform;
    }

    void Update()
    {
        float yPositionDifference = Mathf.Abs(target.position.y - transform.position.y);

        // Check if the Grunt is in the same Y position as the player within the tolerance
        if (yPositionDifference <= yPositionTolerance && Mathf.Abs(target.position.x - transform.position.x) < stopDistance)
        {
           // DoMeleeAttack();
           // WaitForAttack(2);
        }
    }

    public override float GetDirection()
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
        return direction;
    }

}