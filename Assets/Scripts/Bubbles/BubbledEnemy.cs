using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbledEnemy : MonoBehaviour
{
    public int PopDamage { get; set; }
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float popRadius = 1.25f;
    [SerializeField] private float timeToDisappear = 2.5f;

    private Coroutine disappearCoroutine;
    private bool popped = false;
    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    public SpriteRenderer enemySprite { private get; set; }

    private PlayerAttackHandler playerGrabbing;
    private bool grabbed;

    public Vector2 velocity
    {
        get
        {
            if (grabbed)
            {
                return playerGrabbing.GetComponent<Rigidbody2D>().velocity;
            }
            else
            {
                return rb.velocity;
            }
        }
    }
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed && !popped && !grabbed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        else if (grabbed)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void Bump(Vector2 force)
    {
        if (!popped && !grabbed)
        {
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth) && enemyHealth.CurrentHealth > 0 && !popped)
        {
            RaycastHit2D[] allHits = Physics2D.CircleCastAll(transform.position, popRadius * transform.localScale.magnitude, Vector2.zero);
            foreach (RaycastHit2D hit in allHits)
            {
                if (hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth otherHealth))
                {
                    otherHealth.TakeDamage(PopDamage);
                }
            }
            Pop();
        }
    }

    private void Pop()
    {
        Destroy(col);
        spriteRenderer.enabled = false;
        enemySprite.color = Color.red;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 2.5f;
        popped = true;
        if (playerGrabbing != null)
        {
            playerGrabbing.HeldBubblePopped();
        }
        Release(0f);
    }

    public void Grab(PlayerAttackHandler player)
    {
        playerGrabbing = player;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = player.transform;
        transform.localPosition = Vector3.right * col.bounds.extents.x;
        grabbed = true;
    }

    public void Release(float directionAndForce)
    {
        float velocityMultiplier = Mathf.Abs(velocity.x);
        if (velocityMultiplier != 0f)
        {
            directionAndForce *= velocityMultiplier;
        }
        playerGrabbing = null;
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        grabbed = false;
        Bump(Vector2.right * directionAndForce);
    }

    private void OnBecameInvisible()
    {
        if (gameObject.activeInHierarchy && !grabbed)
        {
            disappearCoroutine = StartCoroutine(Disappear());
        }
    }

    private void OnBecameVisible()
    {
        if (disappearCoroutine != null)
        {
            StopCoroutine(disappearCoroutine);
        }
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(timeToDisappear);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}