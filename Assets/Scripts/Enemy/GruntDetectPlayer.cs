using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntDetectPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    private float speed = 1.5f;
    private float chaseDistance = 15.0f;
    private float stopDistance = 1.0f;
    private float horizontalDistance = 1.0f; // Distance to move horizontally away from the player
    public GameObject target;
    private float targetDistance;
    private EnemyAttackHandler attackHandler;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.Player;
        attackHandler = GetComponent<EnemyAttackHandler>();
        enabled = false;
    }

    void OnBecameInvisible()
    {
        enabled = false;
        rb.velocity = Vector2.zero;
    }

    void OnBecameVisible()
    {
        enabled = true;
    }

    void Update()
    {
        targetDistance = Vector2.Distance(transform.position, target.transform.position);
        if (!attackHandler.CurrentlyAttacking)
        {
            if (targetDistance < chaseDistance)
            {
                if (targetDistance > stopDistance)
                {
                    ChasePlayer();
                }
                else
                {
                    // Check if the player is above the Grunt
                    if (target.transform.position.y > transform.position.y)
                    {
                        MoveHorizontallyAwayFromPlayer();
                    }
                    else
                    {
                        MoveDownTowardsPlayer();
                    }
                }
            }
            else
            {
                StopMoving();
            }
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void MoveHorizontallyAwayFromPlayer()
    {
        // Calculate the new position to move horizontally away from the player
        Vector2 newPosition = new Vector2(transform.position.x + horizontalDistance, transform.position.y);

        if (transform.position.x < target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }

    private void MoveDownTowardsPlayer()
    {
        // Calculate the new position to move down towards the player
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);

        if (transform.position.x < target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        transform.position = newPosition;
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }
}