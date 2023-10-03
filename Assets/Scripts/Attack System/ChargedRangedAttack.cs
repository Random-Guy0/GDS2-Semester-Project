using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Ranged Attacks/Charged Ranged Attack", fileName = "new Charged Ranged Attack")]
public class ChargedRangedAttack : RangedAttack
{
    public override IEnumerator DoAttack(float direction = 1, float attackerWidth = 1, Vector2? attackerPosition = null,
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
        
        Vector2 position = attackerPosition ?? Vector2.zero;
        Vector2 origin = GetAttackOrigin(direction, attackerWidth, position);
        
        if (newProjectile.TryGetComponent<Collider2D>(out Collider2D projectileCollider))
        {
            origin.x += direction * projectileCollider.bounds.extents.x;
        }

        newProjectile.transform.position = origin;
        
        newProjectile.FireProjectile(direction, chargeTime, attacker);
    }
}