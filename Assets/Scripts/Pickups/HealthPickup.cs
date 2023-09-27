using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup<PlayerHealth>
{
    [SerializeField] private int healthAmount = 1;
    
    protected override void Collect(PlayerHealth collector)
    {
        collector.Heal(healthAmount);
    }
}
