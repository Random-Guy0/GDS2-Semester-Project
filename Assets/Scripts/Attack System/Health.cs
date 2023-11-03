using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    [field: SerializeField] public DamageType[] Weaknesses { get; private set; }

    public virtual int CurrentHealth { get; protected set; }

    public delegate void TakeDamageHandler();

    public event TakeDamageHandler OnTakeDamage;

    protected virtual void Start()
    {
        CurrentHealth = maxHealth;
    }

    public virtual bool TakeDamage(int amount, Attack attack)
    {
        TakeDamage(amount);
        return true;
    }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        
        OnTakeDamage?.Invoke();
    }

    protected abstract void Die();
}
