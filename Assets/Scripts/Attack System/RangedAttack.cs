using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Ranged Attack", fileName = "new Ranged Attack")]
public class RangedAttack : Attack
{
    [field: SerializeField] public AttackProjectile Projectile { get; private set; }
    [field: SerializeField] public float Range { get; private set; } = 5f;
    [field: SerializeField] public float Speed { get; private set; } = 7f;
    [field: SerializeField] public int AmmoCost { get; private set; } = 1;

    public override IEnumerator DoAttack(float direction = 1f, float attackerWidth = 1f,
        Vector2? attackerPosition = null, AttackHandler attacker = null)
    {
        float currentTime = 0f;
        while (currentTime < Duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        AttackProjectile newProjectile = Instantiate(Projectile, Vector3.zero, Quaternion.identity);
        
        Vector2 position = attackerPosition ?? Vector2.zero;
        Vector2 origin = GetAttackOrigin(direction, attackerWidth, position);
        
        if (newProjectile.TryGetComponent<Collider2D>(out Collider2D projectileCollider))
        {
            origin.x += direction * projectileCollider.bounds.extents.x;
        }

        newProjectile.transform.position = origin;
        
        newProjectile.FireProjectile(direction, attacker);
    }
}
