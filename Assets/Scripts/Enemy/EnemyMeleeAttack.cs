using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMeleeAttack : AttackHandler
{
    Rigidbody2D rb;
    Transform target;
    private float stopDistance = 2.0f;

    private float yPositionTolerance = 0.5f; // Tolerance for Y position check

    void Start(){
         rb = GetComponent<Rigidbody2D>();
         target = GameManager.Instance.Player.transform;
    }

    void Update(){
        Vector2 moveDirection = (target.position - transform.position).normalized;
        float yPositionDifference = Mathf.Abs(target.position.y - transform.position.y);

        if (yPositionDifference <= yPositionTolerance && Mathf.Abs(target.position.x - transform.position.x) < stopDistance){
            
            DoMeleeAttack();

        }
    }
    // Start is called before the first frame update

    public override Vector2 GetDirection()
    {
        float direction;
        if(this.transform.position.x - target.position.x < 0){
            direction = 1.0f;
        }
        else{
            direction = -1.0f;
        }
        return Vector2.right * direction;
    }
}
