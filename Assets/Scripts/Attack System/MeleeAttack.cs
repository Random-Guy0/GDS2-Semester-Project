using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Melee Attack", fileName = "new Melee Attack")]
public class MeleeAttack : Attack
{
    [field: SerializeField] public Vector2 HitSize { get; private set; }
    
    public override void DoAttack()
    {
        
    }
}
