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
        yield return WaitToAttack();
        
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
            if (CanMoveVertically)
            {
                origin.y += direction.y * projectileCollider.bounds.extents.y;
            }
        }

        if (!CanMoveVertically)
        {
            direction.y = 0f;
        }

        newProjectile.transform.position = origin;
        
        newProjectile.FireProjectile(direction, attacker);
    }

    protected override Vector2 GetAttackOrigin(Vector2 direction, Vector2 attackerSize, Vector2 attackerPosition)
    {
        Vector2 origin = attackerPosition;
        origin.x += attackerSize.x * 0.5f * direction.x;
        
        if (CanMoveVertically)
        {
            origin.y += attackerSize.y * 0.5f * direction.y;
        }

        origin += direction * 0.01f;
        return origin;
    }
}
