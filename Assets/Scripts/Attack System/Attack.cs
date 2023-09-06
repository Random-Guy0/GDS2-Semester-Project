using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    [field: SerializeField] public int Damage { get; private set; } = 5;
    [field: SerializeField] public float Cooldown { get; private set; } = 0.5f;
    [field: SerializeField] public AttackTarget Target { get; private set; } = AttackTarget.All;

    public abstract void DoAttack();
}
