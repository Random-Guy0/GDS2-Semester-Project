using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Ranged Attacks/Charged Ranged Attack", fileName = "new Charged Ranged Attack")]
public class ChargedRangedAttack : RangedAttack
{
    public override IEnumerator DoAttack(Vector2 direction, Vector2 attackerSize, Vector2 attackerPosition,
        AttackHandler attacker = null)
    {
        PlayerAttackHandler playerAttackHandler = (PlayerAttackHandler)attacker;
        
        float chargeTime = 0f;
        while (chargeTime < Duration && playerAttackHandler.AttackButtonDown)
        {
            chargeTime += Time.deltaTime;
            yield return null;
        }

        if (chargeTime > Duration)
        {
            chargeTime = Duration;
        }
        
        ChargedAttackProjectile newProjectile = (ChargedAttackProjectile)Instantiate(Projectile, Vector3.zero, Quaternion.identity);
        
        Vector2 origin = GetAttackOrigin(direction, attackerSize, attackerPosition);
        
        if (newProjectile.TryGetComponent<Collider2D>(out Collider2D projectileCollider))
        {
            origin.x += direction.x * projectileCollider.bounds.extents.x;
            origin.y += direction.y * projectileCollider.bounds.extents.y;
        }

        newProjectile.transform.position = origin;
        
        newProjectile.FireProjectile(direction, chargeTime, attacker);
    }
}