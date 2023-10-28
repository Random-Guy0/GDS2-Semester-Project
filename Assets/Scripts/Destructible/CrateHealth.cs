using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateHealth : Crate<HealthPickup>
{
    [SerializeField] private int healthAmount = 1;

    protected override void Die()
    {
        HealthPickup newPickup = SpawnDrop();
        if (newPickup != null)
        {
            newPickup.HealthAmount = healthAmount;
        }
        Destroy(gameObject);
    }
}
