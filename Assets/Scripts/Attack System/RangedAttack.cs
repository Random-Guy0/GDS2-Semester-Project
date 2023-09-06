using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Ranged Attack", fileName = "new Ranged Attack")]
public class RangedAttack : Attack
{
    [field: SerializeField] public AttackProjectile Projectile { get; private set; }
    [field: SerializeField] public float Range { get; private set; } = 5f;
    [field: SerializeField] public float Speed { get; private set; } = 7f;

    public override void DoAttack()
    {
        
    }
}
