using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public void Heal(int amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
    }
    
    protected override void Die()
    {
        //change this to death screen when implemented
        Destroy(this.gameObject);
    }
}
