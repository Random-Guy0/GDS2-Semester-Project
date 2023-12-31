using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private PlayerAttackHandler attackHandler;

    [SerializeField] private StudioEventEmitter lowHealthSound;
    
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

    public override bool TakeDamage(int amount, Attack attack)
    {
        if (CurrentHealth - amount == 1)
        {
            lowHealthSound.Play();
        }
        
        return base.TakeDamage(amount, attack);
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
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.CanMove = false;
        playerMovement.StopMoving();
        playerMovement.enabled = false;
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }

        GetComponent<PlayerAttackHandler>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        GameManager.Instance.GameOverUI.gameObject.SetActive(true);
        
        this.enabled = false;
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
