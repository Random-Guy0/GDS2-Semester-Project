using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    [SerializeField] private RangedAttack attackStats;

    private float direction = 0f;
    private float startingXPosition;

    private Vector2 velocity = Vector2.zero;

    public void FireProjectile(float direction)
    {
        this.direction = direction;
        startingXPosition = transform.position.x;
        velocity.x = this.direction * attackStats.Speed;
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
}
