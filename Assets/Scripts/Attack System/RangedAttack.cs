using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Ranged Attacks/Ranged Attack", fileName = "new Ranged Attack")]
public class RangedAttack : Attack
{
    [field: SerializeField] public AttackProjectile Projectile { get; private set; }
    [field: SerializeField] [Tooltip("Set to 0 for range to be unlimited.")] public float Range { get; private set; } = 5f;
    [field: SerializeField] public float Speed { get; private set; } = 7f;
    [field: SerializeField] public int AmmoCost { get; private set; } = 1;

    public override IEnumerator DoAttack(Vector2 direction, Vector2 attackerSize,
        Vector2 attackerPosition, AttackHandler attacker = null)
    {
        float currentTime = 0f;
        while (currentTime < Duration)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        AttackProjectile newProjectile = Instantiate(Projectile, Vector3.zero, Quaternion.identity);
        
        Vector2 origin = GetAttackOrigin(direction, attackerSize, attackerPosition);
        
        if (newProjectile.TryGetComponent<Collider2D>(out Collider2D projectileCollider))
        {
            origin.x += direction.x * projectileCollider.bounds.extents.x;
            origin.y += direction.y * projectileCollider.bounds.extents.y;
        }

        newProjectile.transform.position = origin;
        
        newProjectile.FireProjectile(direction, attacker);
    }
}
