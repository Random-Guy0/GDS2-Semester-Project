using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackHandler : AttackHandler
{
    private PlayerMovement playerMovement;
    private AmmoController ammoController;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        ammoController = GetComponent<AmmoController>();
    }

    public void DoMeleeAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DoMeleeAttack();
        }
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
}
