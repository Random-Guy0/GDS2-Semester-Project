using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorDetectPlayer : MonoBehaviour
{
    Rigidbody2D rb;

    float movementSpeed = 2.0f;
    Vector2 moveDirection;
    Transform target;

    void Start()
    {
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
        Vector3 direction = (target.position - transform.position).normalized;
        moveDirection = direction;
        if(enabled){
            rb.velocity = new Vector2(moveDirection.x, 0) * movementSpeed;
        }
        
    }

    
}


