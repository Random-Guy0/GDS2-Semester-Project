using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedEnemyHealth : EnemyHealth
{
    [SerializeField] private Animator shieldAnimator;
    [SerializeField] private AnimationClip shieldBreakAnimation;
    [SerializeField] private Collider2D shieldCollider;
    private bool shielded = true;
    
    public override bool TakeDamage(int amount, Attack attack)
    {
        if (!shielded)
        {
            return base.TakeDamage(amount, attack);
        }
        
        if ((attack is MeleeAttack || attack is ChargedRangedAttack) && shielded)
        {
            shieldAnimator.SetTrigger("ShieldBreak");
            Destroy(shieldCollider);
            StartCoroutine(DestroyShield());
            shielded = false;
            return true;
        }

        return true;
    }

    private IEnumerator DestroyShield()
    {
        yield return new WaitForSeconds(shieldBreakAnimation.length);
        Destroy(shieldAnimator.gameObject);
    }
}
