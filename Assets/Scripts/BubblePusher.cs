using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePusher : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float pushMultiplier = 2.5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<BubbledEnemy>(out BubbledEnemy bubbledEnemy) && bubbledEnemy.enabled)
        {
            Vector2 direction = -rb.velocity.normalized;
            Vector2 origin = transform.position;
            List<Collider2D> colliders = new List<Collider2D>();
            rb.GetAttachedColliders(colliders);
            origin += colliders[0].bounds.extents * direction;
            RaycastHit2D raycast = Physics2D.Raycast(origin, direction);

            if (raycast.transform != null && raycast.transform.gameObject != bubbledEnemy.gameObject)
            {
                bubbledEnemy.Bump(rb.velocity * pushMultiplier);
            }
        }
    }
}
