using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorDetectPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    float patrolDistance = 7.0f;
    float attackRange = 7.0f;
    float initialY;

    float movementSpeed = 4.0f;
    Transform player;
    Vector2 initialPosition;
    bool Left = true;

    void Start()
    {
        initialY = this.transform.position.y;
        rb = GetComponent<Rigidbody2D>();
        player = GameManager.Instance.Player.transform;
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
        if(Left){
            rb.velocity = new Vector2(-1, 0) * movementSpeed;

        }
        else{
            rb.velocity = new Vector2(1, 0) * movementSpeed;     
        }

        //checks patrol range
        if(this.transform.position.x > player.position.x + patrolDistance){
            Left = true;
        }
        else if(this.transform.position.x < player.position.x - patrolDistance){
            Left = false;
        }
        

        //check in range
        float currentRange = this.transform.position.x - player.position.x;
        if(Mathf.Abs(currentRange) < attackRange && Left){
            Dive();
        }
        
    

    void Dive()
    {
       // Calculate the dive destination
        Vector2 diveDestination = new Vector2(player.position.x + 4.0f, player.position.y);
        
        // Move down to the dive destination Y
        Vector2 moveDirection = (diveDestination - (Vector2)transform.position).normalized;
        rb.velocity = new Vector2(rb.velocity.x, moveDirection.y * movementSpeed);

        if(this.transform.position.x < player.position.x - 2.0f){
            Vector2 returnPosition = new Vector2(transform.position.x, initialY);
            rb.velocity = new Vector2(rb.velocity.x, 0.7f* movementSpeed);
        }

        // Check if the Raptor has reached the dive destination Y
        
    }
    
}
}

