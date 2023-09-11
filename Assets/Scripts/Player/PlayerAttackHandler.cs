using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackHandler : AttackHandler
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
        if (context.performed)
        {
            DoRangedAttack();
        }
    }

    protected override IEnumerator WaitForAttack(float attackDuration)
    {
        playerMovement.enabled = false;
        yield return base.WaitForAttack(attackDuration);
        playerMovement.enabled = true;
    }

    protected override float GetDirection()
    {
        float direction = playerMovement.LookDirection;
        direction = Mathf.Round(direction);
        if (direction == 0f)
        {
            direction = 1f;
        }
        return direction;
    }
}
