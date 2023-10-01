using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private PlayerAttackHandler attackHandler;
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
        attackHandler = GetComponent<PlayerAttackHandler>();
        OnTakeDamage += attackHandler.InterruptAttack;
        OnTakeDamage += TakeDamage;
    }

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

    private void TakeDamage()
    {
        StartCoroutine(FlashRed());
    }
    
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
