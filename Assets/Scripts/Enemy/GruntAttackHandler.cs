using System.Collections;
using UnityEngine;

public class GruntAttackHandler : EnemyAttackHandler
{
    private GruntDetectPlayer movement;
    private Animator animator;
    [SerializeField] private AnimationClip meleeAttackAnimation;
    private float meleeAttackSpeed;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<GruntDetectPlayer>();
        animator = GetComponent<Animator>();
        meleeAttackSpeed = meleeAttackAnimation.length / MeleeAttacks[0].Duration;
        animator.SetFloat("AttackSpeed", meleeAttackSpeed);
    }

    protected override IEnumerator WaitForAttack(float attackDuration)
    {
        movement.CanMove = false;
        yield return base.WaitForAttack(attackDuration);
        movement.CanMove = true;
    }

    protected override bool CanAttack()
    {
        return base.CanAttack() && movement.CanMove;
    }

    public override void DoMeleeAttack(int index = 0)
    {
        animator.SetTrigger("DoAttack");
        base.DoMeleeAttack(index);
    }
}