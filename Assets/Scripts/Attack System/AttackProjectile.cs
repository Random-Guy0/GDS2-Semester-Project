using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    [SerializeField] private RangedAttack attackStats;

    private float direction = 1f;
    private float startingXPosition;

    private Vector2 velocity = Vector2.zero;

    private GameObject attacker;

    public void FireProjectile(float direction, GameObject attacker = null)
    {
        this.direction = direction;
        startingXPosition = transform.position.x;
        velocity.x = this.direction * attackStats.Speed;
        this.attacker = attacker;
    }

    private void Update()
    {
        float distanceTravelled = Mathf.Abs(startingXPosition - transform.position.x);
        if (distanceTravelled >= attackStats.Range)
        {
            Destroy(gameObject);
        }

        transform.position += (Vector3)velocity * Time.deltaTime;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health otherHealth) && attackStats.CanAttack(otherHealth, attacker))
        {
            attackStats.DoDamage(otherHealth, attacker);
            Destroy(gameObject);
        }
    }
}
