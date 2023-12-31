using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChargedAttackProjectile : AttackProjectile
{
    [SerializeField] private float explosionRadius = 1.5f;
    [SerializeField] private float dropDistance = 1.2f;
    [SerializeField] private float gravity = 1f;
    [SerializeField] private float bounceFactor = 2.5f;
    [SerializeField] private GameObject explosionPrefab;

    private float startingYPosition;
    private float chargeTime;

    [SerializeField] private FMODUnity.StudioEventEmitter explosionSound;
    
    public void FireProjectile(Vector2 direction, float chargeTime, AttackHandler attacker = null)
    {
        FireProjectile(direction, attacker);
        if (chargeTime != 0f)
        {
            velocity.x *= chargeTime;
        }

        startingYPosition = transform.position.y;
        this.chargeTime = chargeTime;
    }

    protected override void Move()
    {
        velocity.y -= gravity;
        float distanceTravelled = Mathf.Abs(startingXPosition - transform.position.x);
        if (attackStats.Range > 0f && distanceTravelled >= attackStats.Range * chargeTime)
        {
            DoAttack();
        }

        if (transform.position.y < startingYPosition - dropDistance)
        {
            velocity.y = bounceFactor;
            bounceFactor -= 0.1f;
            if (bounceFactor < 2f)
            {
                bounceFactor = 4f;
            }
        }

        transform.position += (Vector3)velocity * Time.deltaTime;
    }

    private void DoAttack()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);

        List<GameObject> alreadyHit = new List<GameObject>();
        
        foreach (RaycastHit2D hit in hits)
        {
            if (!alreadyHit.Contains(hit.transform.gameObject))
            {
                alreadyHit.Add(hit.transform.gameObject);
                if (hit.transform.TryGetComponent<Health>(out Health otherHealth))
                {
                    if (attackStats.CanAttack(otherHealth, attacker))
                    {
                        int damageToDeal = attackStats.Damage;

                        foreach (DamageType type in attackStats.DamageTypes)
                        {
                            if (otherHealth.Weaknesses.Contains(type))
                            {
                                damageToDeal *= (int)attackStats.DamageTypeAttackMultiplier;
                            }
                        }

                        float distanceFromAttack = Vector2.Distance(transform.position, otherHealth.transform.position);
                        damageToDeal *= (int)(explosionRadius / distanceFromAttack);
                        if (damageToDeal <= 0)
                        {
                            damageToDeal = 1;
                        }

                        otherHealth.TakeDamage(damageToDeal, attackStats);
                    }
                }
            }
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionSound.Play();
        
        Destroy(gameObject);
    }

    protected override void DoAttack(Health otherHealth)
    {
        if (attackStats.CanAttack(otherHealth, attacker))
        {
            DoAttack();
        }
    }
}
