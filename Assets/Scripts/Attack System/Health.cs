using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [field: SerializeField] public DamageType[] Weaknesses { get; private set; }

    public virtual int CurrentHealth { get; protected set; }

    public delegate void TakeDamageHandler();

    public event TakeDamageHandler OnTakeDamage;

    [SerializeField] private FMODUnity.StudioEventEmitter hurtSound;
    [SerializeField] private FMODUnity.StudioEventEmitter deathSound;
    [SerializeField] private FMODUnity.StudioEventEmitter impactSound;

    private bool deathSoundPlayed = false;

    protected virtual void Start()
    {
        CurrentHealth = maxHealth;
    }

    public virtual bool TakeDamage(int amount, Attack attack)
    {
        if (attack is PlayerMeleeAttack meleeAttack && !meleeAttack.ImpactSound.IsNull && impactSound != null)
        {
            impactSound.EventReference = meleeAttack.ImpactSound;
            impactSound.Play();
        }
        
        TakeDamage(amount);
        return true;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            if (deathSound != null && !deathSoundPlayed)
            {
                deathSound.Play();
                deathSoundPlayed = true;
            }
            
            CurrentHealth = 0;
            Die();
        }
        else
        {
            if (hurtSound != null)
            {
                hurtSound.Play();
            }
        }
        
        OnTakeDamage?.Invoke();
    }

    protected abstract void Die();
}
