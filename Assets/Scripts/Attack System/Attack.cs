using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; } = 5;
    [field: SerializeField] public AttackTarget Target { get; private set; } = AttackTarget.All;
    [field: SerializeField] public float Duration { get; private set; } = 0.2f;
    [field: SerializeField] public DamageType[] DamageTypes { get; private set; }
    [field: SerializeField] public float DamageTypeAttackMultiplier { get; private set; } = 2f;

    public abstract IEnumerator DoAttack(float direction = 1f, float attackerWidth = 1f,
        Vector2? attackerPosition = null, AttackHandler attacker = null);

    protected virtual Vector2 GetAttackOrigin(float direction, float attackerWidth, Vector2 attackerPosition)
    {
        Vector2 origin = attackerPosition;
        origin.x += attackerWidth * 0.5f * direction;
        origin.x += direction * 0.01f;
        return origin;
    }

    public void DoDamage(Health otherHealth, AttackHandler attacker = null)
    {
        if (!CanAttack(otherHealth, attacker))
        {
            return;
        }

        int damageToDeal = Damage;

        foreach (DamageType type in DamageTypes)
        {
            if (otherHealth.Weaknesses.Contains(type))
            {
                damageToDeal *= (int)DamageTypeAttackMultiplier;
            }
        }
        
        otherHealth.TakeDamage(damageToDeal, this);
    }

    public bool CanAttack(Health otherHealth, AttackHandler attacker = null)
    {
        bool notAttackingSelf = false;
        if (attacker != null)
        {
            notAttackingSelf = otherHealth.gameObject != attacker.gameObject;
        }
        
        return !((Target == AttackTarget.Enemies && otherHealth is PlayerHealth) ||
            (Target == AttackTarget.Players && otherHealth is EnemyHealth)) &&
               notAttackingSelf;
    }
}
