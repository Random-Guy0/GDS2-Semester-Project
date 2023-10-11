using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Ranged Attacks/Continuous Ranged Attack", fileName = "new Ranged Attack")]
public class ContinuousRangedAttack : RangedAttack
{
    [field: SerializeField] public float ProjectileTimer { get; private set; } = 0.02f;
    
    public override IEnumerator DoAttack(Vector2 direction, Vector2 attackerSize, Vector2 attackerPosition,
        AttackHandler attacker = null)
    {
        PlayerAttackHandler playerAttackHandler = (PlayerAttackHandler)attacker;
        
        float ammoTimer = 0f;
        float projectileTimer = 0f;
        while (playerAttackHandler.AttackButtonDown)
        {
            ammoTimer += Time.deltaTime;
            projectileTimer += Time.deltaTime;

            if (ammoTimer >= Duration)
            {
                if (!playerAttackHandler.UseAmmo(AmmoCost))
                {
                    break;
                }
                ammoTimer = 0f;
            }

            if (projectileTimer >= ProjectileTimer)
            {
                projectileTimer = 0f;

                AttackProjectile newProjectile = Instantiate(Projectile, Vector3.zero, Quaternion.identity);

                direction = attacker.GetDirection();

                Vector2 position = playerAttackHandler.SelectedWeapon.AttackOrigin.position;
                Vector2 origin = GetAttackOrigin(direction, attackerSize, position);

                if (newProjectile.TryGetComponent<Collider2D>(out Collider2D projectileCollider))
                {
                    origin.x += direction.x * projectileCollider.bounds.extents.x;
                    origin.y += direction.y * projectileCollider.bounds.extents.y;
                }

                newProjectile.transform.position = origin;

                newProjectile.FireProjectile(direction, attacker);
            }

            yield return null;
        }
    }
}
