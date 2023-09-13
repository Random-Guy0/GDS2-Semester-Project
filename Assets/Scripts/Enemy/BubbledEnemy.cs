using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbledEnemy : MonoBehaviour
{
    [SerializeField] private int bumpDamage = 10;
    [SerializeField] private float maxSpeed = 15f;
    
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.mass = 0.5f;
        rb.drag = 2f;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    public void Bump(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (enabled && other.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth otherHealth))
        {
            otherHealth.TakeDamage(bumpDamage);
        }
    }
}
