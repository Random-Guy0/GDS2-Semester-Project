using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class GruntAttackHandler : EnemyAttackHandler
{
    private GruntDetectPlayer movement;
    private Animator animator;
    [FormerlySerializedAs("meleeAttackAnimation")] [SerializeField] private AnimationClip attackAnimation;
    [SerializeField] private Transform attackOrigin;
    private float attackSpeed;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<GruntDetectPlayer>();
        animator = GetComponent<Animator>();
        float attackDuration = 1f;
        if (MeleeAttacks.Length > 0)
        {
            attackDuration = MeleeAttacks[0].Duration;
        }
        else if (RangedAttacks.Length > 0)
        {
            attackDuration = RangedAttacks[0].Duration;
        }
        attackSpeed = attackAnimation.length / attackDuration;
        animator.SetFloat("AttackSpeed", attackSpeed);
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

    protected override void DoAttack()
    {
        animator.SetTrigger("DoAttack");
        base.DoAttack();
    }

    protected override Vector2 GetAttackOrigin()
    {
        return attackOrigin.position;
    }
}