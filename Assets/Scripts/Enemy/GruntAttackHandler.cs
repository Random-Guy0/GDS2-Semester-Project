using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GruntAttackHandler : AttackHandler
{
    Rigidbody2D rb;
    Transform target;

    void Start(){
         rb = GetComponent<Rigidbody2D>();
         target = GameManager.Instance.Player.transform;
    }

    void Update(){
        Vector2 moveDirection = (target.position - transform.position).normalized;
       
        if(Mathf.Abs(target.position.x - transform.position.x) < 1.5){
            DoMeleeAttack();
        }
        
        

    }
    // Start is called before the first frame update

    protected override float GetDirection()
    {
        float direction;
        if(this.transform.position.x - target.position.x < 0){
            direction = 1.0f;
        }
        else{
            direction = -1.0f;
        }
        return direction;
    }
}
