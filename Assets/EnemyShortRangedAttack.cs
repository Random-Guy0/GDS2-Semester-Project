using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyShortRangedAttack : AttackHandler
{
    Transform target;
    Rigidbody2D rb;
    private float stopDistance = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.Player.transform;
    }

    void Update()
    {
        float yPositionDifference = Mathf.Abs(target.position.y - transform.position.y);

        if(Mathf.Abs(yPositionDifference) < 1){
            DoRangedAttack();
            
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
}