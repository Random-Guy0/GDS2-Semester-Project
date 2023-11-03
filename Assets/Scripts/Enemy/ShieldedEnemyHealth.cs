using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedEnemyHealth : EnemyHealth
{
    [SerializeField] private Animator shieldAnimator;
    [SerializeField] private AnimationClip shieldBreakAnimation;
    [SerializeField] private Collider2D shieldCollider;
    private bool shielded = true;

    [SerializeField] private FMODUnity.StudioEventEmitter shieldHitSound;
    [SerializeField] private FMODUnity.StudioEventEmitter shieldBreakSound;
    
    public override bool TakeDamage(int amount, Attack attack)
    {
        if (!shielded)
        {
            return base.TakeDamage(amount, attack);
        }
        
        if (attack is MeleeAttack or ChargedRangedAttack && shielded)
        {
            shieldAnimator.SetTrigger("ShieldBreak");
            Destroy(shieldCollider);
            StartCoroutine(DestroyShield());
            shielded = false;
            shieldBreakSound.Play();
            return true;
        }

        shieldHitSound.Play();
        return true;
    }

    private IEnumerator DestroyShield()
    {
        yield return new WaitForSeconds(shieldBreakAnimation.length);
        Destroy(shieldAnimator.gameObject);
    }
}
