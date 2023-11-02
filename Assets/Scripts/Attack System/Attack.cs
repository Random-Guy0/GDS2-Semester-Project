using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; } = 5;
    [field: SerializeField] public AttackTarget Target { get; private set; } = AttackTarget.All;
    [field: SerializeField] public float AttackStartDelay { get; private set; } = 0f;
    [field: SerializeField] public float Duration { get; private set; } = 0.2f;
    [field: SerializeField] public DamageType[] DamageTypes { get; private set; }
    [field: SerializeField] public float DamageTypeAttackMultiplier { get; private set; } = 2f;
    [field: SerializeField] public bool CanMoveVertically { get; private set; } = false;

    public abstract IEnumerator DoAttack(Vector2 direction, Vector2 attackerSize,
        Vector2 attackerPosition, AttackHandler attacker = null);

    protected virtual Vector2 GetAttackOrigin(Vector2 direction, Vector2 attackerSize, Vector2 attackerPosition)
    {
        Vector2 origin = attackerPosition;
        origin.x += attackerSize.x * 0.5f * direction.x;
        origin.y += attackerSize.y * 0.5f * direction.y;
        origin += direction * 0.01f;
        return origin;
    }

    public bool DoDamage(Health otherHealth, AttackHandler attacker = null)
    {
        if (!CanAttack(otherHealth, attacker))
        {
            return false;
        }

        int damageToDeal = Damage;

        foreach (DamageType type in DamageTypes)
        {
            if (otherHealth.Weaknesses.Contains(type))
            {
                damageToDeal *= (int)DamageTypeAttackMultiplier;
            }
        }
        
        return otherHealth.TakeDamage(damageToDeal, this);
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

    protected IEnumerator WaitToAttack()
    {
        yield return new WaitForSeconds(AttackStartDelay);
    }
}
