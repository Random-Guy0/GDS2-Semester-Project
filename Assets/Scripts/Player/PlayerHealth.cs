using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override int CurrentHealth
    {
        get => base.CurrentHealth;
        protected set
        {
            base.CurrentHealth = value;
            GameManager.Instance.GameplayUI.UpdateHealthUI(value);
        }
    }

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.GameplayUI.SetInitialHealthUI(CurrentHealth, maxHealth);
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
    }

#if UNITY_EDITOR
    
    private void OnGUI()
    {
        GUI.Label( new Rect(5f, 5f, 300f, 150f), "Health: " + CurrentHealth.ToString());

    }
    
#endif

    protected override void Die()
    {
        //change this to death screen when implemented
        Destroy(this.gameObject);
    }
}
