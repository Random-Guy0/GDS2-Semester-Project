using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; } = 5;
    [field: SerializeField] public AttackTarget Target { get; private set; } = AttackTarget.All;
    [field: SerializeField] public float Duration { get; private set; } = 0.2f;

    public abstract IEnumerator DoAttack(float direction = 1f, float attackerWidth = 1f, Vector2? attackerPosition = null);

    protected virtual Vector2 GetAttackOrigin(float direction, float attackerWidth, Vector2 attackerPosition)
    {
        Vector2 origin = attackerPosition;
        origin.x += attackerWidth * 0.5f * direction;
        origin.x += direction * 0.01f;
        return origin;
    }

    public void DoDamage(Health health)
    {
        if (!CanAttack(health))
        {
            return;
        }
        
        health.TakeDamage(Damage);
    }

    public bool CanAttack(Health health)
    {
        return !((Target == AttackTarget.Enemies && health is PlayerHealth) /*||
        (Target == AttackTarget.Players && health is EnemyHealth)*/);
    }
}
