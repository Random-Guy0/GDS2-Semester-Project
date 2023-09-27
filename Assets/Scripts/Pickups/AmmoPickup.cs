using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup<AmmoController>
{
    [SerializeField] private int ammoAmount = 3;
    
    protected override void Collect(AmmoController collector)
    {
        collector.PickupAmmo(ammoAmount);
    }
}
