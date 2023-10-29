using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : DestructibleObject
{
    [SerializeField] private bool rangedAttacksHit = false;
    
    public override bool TakeDamage(int amount, Attack attack)
    {
        if (attack is not RangedAttack)
        {
            return base.TakeDamage(amount, attack);
        }

        return rangedAttacksHit;
    }
}
