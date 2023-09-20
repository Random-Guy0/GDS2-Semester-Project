using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntDetectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    private float speed = 2.0f;
    private float chaseDistance = 15.0f;
    private float stopDistance = 1.0f;
    public GameObject target;
    private float targetDistance;


    void Start(){
         rb = GetComponent<Rigidbody2D>();
         target = GameManager.Instance.Player;
         enabled = false;

    }
    void OnBecameInvisible(){
        enabled = false;
        rb.velocity = Vector2.zero;

    }

    void OnBecameVisible(){
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
       targetDistance = Vector2.Distance(transform.position, target.transform.position);
       if(targetDistance < chaseDistance && targetDistance > stopDistance){
        ChasePlayer();
       }
       else{
        StopMoving();
       }
    }

    private void ChasePlayer(){
        if(transform.position.x < target.transform.position.x){
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else{
            GetComponent<SpriteRenderer>().flipX = false;
        }

    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }
    
}
