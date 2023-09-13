using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GruntAttackHandler : AttackHandler
{
    Rigidbody2D rb;
    Transform target;

    void Start(){
         rb = GetComponent<Rigidbody2D>();
         target = GameObject.Find("Player").transform;
         enabled = false;

    }
    // Start is called before the first frame update
    public void DoMeleeAttack(){
       DoMeleeAttack();
    }
    protected override float GetDirection()
    {
        float direction;
        if(this.transform.position.x - target.position.x < 0){
            direction = -1.0f;
        }
        else{
            direction = 1.0f;
        }
        return direction;
    }
}
