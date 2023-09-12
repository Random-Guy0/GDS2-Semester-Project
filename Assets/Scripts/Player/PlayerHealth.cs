using System;
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

    private void OnGUI()
    {
        GUI.Label( new Rect(5f, 5f, 300f, 150f), "Health: " + CurrentHealth.ToString());

    }

    protected override void Die()
    {
        //change this to death screen when implemented
        Destroy(this.gameObject);
    }
}
