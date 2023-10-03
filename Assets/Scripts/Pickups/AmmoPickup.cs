using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup<AmmoController>
{
    [field: SerializeField] public int AmmoAmount { get; set; } = 3;
    
    protected override void Collect(AmmoController collector)
    {
        collector.PickupAmmo(AmmoAmount);
    }
}
