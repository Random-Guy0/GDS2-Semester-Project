using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousAttackProjectile : AttackProjectile
{
    private bool trackPlayer = true;
    
    protected override void Move()
    {
        float distanceTravelled = Mathf.Abs(startingXPosition - transform.position.x);
        if (attackStats.Range > 0f && distanceTravelled >= attackStats.Range)
        {
            Destroy(gameObject);
        }

        Vector3 position = transform.position;
        position += (Vector3)velocity * Time.deltaTime;

        if (attacker is PlayerAttackHandler playerAttackHandler)
        {
            if (!playerAttackHandler.AttackButtonDown)
            {
                trackPlayer = false;
            }
            
            
            if (playerAttackHandler.SelectedWeapon.Attack is ContinuousRangedAttack &&
                playerAttackHandler.GetDirection().x == direction.x &&
                trackPlayer)
            {
                position.y = playerAttackHandler.SelectedWeapon.AttackOrigin.position.y;
            }
        }

        transform.position = position;
    }
}
