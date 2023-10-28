using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateAmmo : Crate<AmmoPickup>
{
    [SerializeField] private int ammoAmount = 3;

    protected override void Die()
    {
        AmmoPickup newPickup = SpawnDrop();
        if (newPickup != null)
        {
            newPickup.AmmoAmount = ammoAmount;
        }
        Destroy(gameObject);
    }
}
