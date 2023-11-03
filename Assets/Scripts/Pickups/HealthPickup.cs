using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup<PlayerHealth>
{
    [field: SerializeField] public int HealthAmount { get; set; } = 1;
    
    protected override void Collect(PlayerHealth collector)
    {
        collector.Heal(HealthAmount);
    }
}
