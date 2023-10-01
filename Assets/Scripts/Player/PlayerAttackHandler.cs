using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackHandler : AttackHandler
{
    [SerializeField] private Animator animator;
    private PlayerMovement playerMovement;
    private AmmoController ammoController;

    private bool bufferAttack = false;

    [SerializeField] private AnimationClip meleeAttack1Animation;
    [SerializeField] private AnimationClip meleeAttack2Animation;
    private float meleeAttack1Speed;
    private float meleeAttack2Speed;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        ammoController = GetComponent<AmmoController>();

        meleeAttack1Speed = meleeAttack1Animation.length / MeleeAttacks[0].Duration;
        meleeAttack2Speed = meleeAttack2Animation.length / MeleeAttacks[0].Duration;
        animator.SetFloat("MeleeAttack1Speed", meleeAttack1Speed);
        animator.SetFloat("MeleeAttack2Speed", meleeAttack2Speed);
    }

    public void DoMeleeAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bufferAttack = CurrentlyAttacking;
            DoMeleeAttack();
        }
    }

    public override void DoMeleeAttack(int index = 0)
    {
        animator.SetTrigger("DoMeleeAttack");
        base.DoMeleeAttack(index);
    }

    public void DoRangedAttack(InputAction.CallbackContext context)
    {
        if (context.performed && ammoController.CanUseAmmo(RangedAttacks[0].AmmoCost))
        {
            ammoController.UseAmmo(RangedAttacks[0].AmmoCost);
            DoRangedAttack();
        }
    }

    protected override IEnumerator WaitForAttack(float attackDuration)
    {
        playerMovement.CanMove = false;
        yield return base.WaitForAttack(attackDuration);
        playerMovement.CanMove = true;
        if (bufferAttack)
        {
            bufferAttack = false;
            DoMeleeAttack();
        }
        else
        {
            InterruptAttack();
        }
        
    }

    protected override float GetDirection()
    {
        float direction = playerMovement.Direction.x;
        if (direction == 0f)
        {
            direction = 1f;
        }
        return direction;
    }

    public override void InterruptAttack()
    {
        base.InterruptAttack();
        animator.SetTrigger("InterruptAttack");
    }
}
