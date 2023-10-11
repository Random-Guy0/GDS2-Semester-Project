using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    [SerializeField] protected RangedAttack attackStats;

    protected Vector2 direction;
    protected float startingXPosition;

    protected Vector2 velocity = Vector2.zero;

    protected AttackHandler attacker;

    public void FireProjectile(Vector2 direction, AttackHandler attacker = null)
    {
        this.direction = direction;
        startingXPosition = transform.position.x;
        velocity = this.direction.normalized * attackStats.Speed;
        this.attacker = attacker;
    }

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        float distanceTravelled = Mathf.Abs(startingXPosition - transform.position.x);
        if (distanceTravelled >= attackStats.Range)
        {
            Destroy(gameObject);
        }

        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    protected virtual void DoAttack(Health otherHealth)
    {
        if (attackStats.CanAttack(otherHealth, attacker))
        {
            attackStats.DoDamage(otherHealth, attacker);
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Health>(out Health otherHealth))
        {
            DoAttack(otherHealth);
        }
    }
}
