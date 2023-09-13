using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntDetectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    float movementSpeed = 1.0f;
    Vector2 moveDirection;
    Transform target;


    void Start(){
         rb = GetComponent<Rigidbody2D>();
         target = GameObject.Find("Player").transform;
         enabled = false;

    }
    void OnBecameInvisible(){
        enabled = false;
        rb.velocity = new Vector2(0, 0);

    }

    void OnBecameVisible(){
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection= (target.position - transform.position).normalized;
        if(enabled){
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed;
        }
        
    }
    
}
