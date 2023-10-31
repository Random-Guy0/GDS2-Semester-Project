using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntDetectPlayer : MonoBehaviour, IEnemyMovement
{
    Rigidbody2D rb;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float chaseDistance = 15.0f;
    [SerializeField] private float stopDistance = 1.0f;
    [SerializeField] private float lookAheadDistance = 2.5f;
    [SerializeField] private LayerMask avoidLayers;
    private float horizontalDistance = 1.0f; // Distance to move horizontally away from the player
    public GameObject target;
    private float targetDistance;
    private EnemyAttackHandler attackHandler;

    [field: SerializeField] public float StunDuration { get; protected set; } = 0.25f;

    private Vector2 boxcastSize;

    public bool CanMove { get; set; } = true;

    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.Player;
        attackHandler = GetComponent<EnemyAttackHandler>();
        enabled = false;
        boxcastSize = GetComponent<BoxCollider2D>().size;
        boxcastSize.y *= transform.localScale.x * 1.1f;
        animator = GetComponent<Animator>();
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
        if (CanMove)
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
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero;
    }

    private void ChasePlayer()
    {
        bool facingRight = transform.position.x < target.transform.position.x;
        Flip(facingRight);
        
        Vector2 moveDir = target.transform.position - transform.position;
        moveDir.Normalize();
        RaycastHit2D[] checkInFront = Physics2D.BoxCastAll(transform.position, boxcastSize, 0f, moveDir, lookAheadDistance, avoidLayers);
        if (!ValidRaycast(checkInFront))
        {
            Vector2 lookUp = Quaternion.AngleAxis(90f, Vector3.forward) * moveDir;
            lookUp.Normalize();
            RaycastHit2D[] checkUp = Physics2D.BoxCastAll(transform.position, boxcastSize, 90f, moveDir, lookAheadDistance, avoidLayers);
            if (!ValidRaycast(checkUp))
            {
                Vector2 lookDown = Quaternion.AngleAxis(-90f, Vector3.forward) * moveDir;
                lookDown.Normalize();
                moveDir = lookDown;
            }
            else
            {
                moveDir = lookUp;
            }
        }
        
        Debug.DrawRay(transform.position, moveDir * lookAheadDistance);

        Vector2 position = transform.position;
        position += speed * Time.deltaTime * moveDir;
        transform.position = position;

        //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private bool ValidRaycast(RaycastHit2D[] hits)
    {
        bool result = true;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform != transform && hit.transform != target.transform && hit.collider != null)
            {
                result = false;
                break;
            }
        }

        return result;
    }

    private void MoveHorizontallyAwayFromPlayer()
    {
        // Calculate the new position to move horizontally away from the player
        Vector2 newPosition = new Vector2(transform.position.x + horizontalDistance, transform.position.y);

        if (transform.position.x < target.transform.position.x)
        {
            Flip(true);
        }
        else
        {
            Flip(false);
        }

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }

    private void MoveDownTowardsPlayer()
    {
        // Calculate the new position to move down towards the player
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);

        if (transform.position.x < target.transform.position.x)
        {
            Flip(true);
        }
        else
        {
            Flip(false);
        }

        transform.position = newPosition;
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    public void Stun()
    {
        StartCoroutine(WaitForStun());
    }

    private IEnumerator WaitForStun()
    {
        CanMove = false;
        yield return new WaitForSeconds(StunDuration);
        CanMove = true;
    }

    private void Flip(bool flipped)
    {
        Vector3 scale = transform.localScale;
        if (flipped)
        {
            scale.x = Mathf.Abs(scale.x) * -1f;
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}