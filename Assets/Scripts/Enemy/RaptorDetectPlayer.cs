using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorDetectPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    float patrolDistance = 7.0f;

    float movementSpeed = 4.0f;
    Transform player;
    bool isDiving = false;
    Vector3 initialPosition;
    bool Left = true;

    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        enabled = false;
        initialPosition = transform.position;
        
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

        //checks patrol range
        if(this.transform.position.x > player.position.x + patrolDistance){
            Left = true;
        }
        else if(this.transform.position.x < player.position.x - patrolDistance){
            Left = false;
        }
        

        //check in range
        Debug.Log(this.transform.position - player.position);

        //dive down
        //return to Y pos
        //patrol





        if(!isDiving){
            Debug.Log(Left);
            Patrol(Left);
        }
    }

    

    void Patrol(bool movingLeft){
        if(movingLeft){
            rb.velocity = new Vector2(-1, 0) * movementSpeed;

        }
        else{
            rb.velocity = new Vector2(1, 0) * movementSpeed;     
        }
        

    }

    
}


