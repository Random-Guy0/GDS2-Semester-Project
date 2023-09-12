using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 20;
    public int AmmoCount { get; private set; }

    private void Start()
    {
        AmmoCount = maxAmmo;
    }

    private void PickupAmmo(int amount)
    {
        AmmoCount += amount;
        if (AmmoCount > maxAmmo)
        {
            AmmoCount = maxAmmo;
        }
    }

    private bool CanPickupAmmo(int amount)
    {
        return AmmoCount + amount <= maxAmmo;
    }

    public void UseAmmo(int amount)
    {
        if (CanUseAmmo(amount))
        {
            AmmoCount -= amount;
        }
    }

    public bool CanUseAmmo(int amount)
    {
        return amount <= AmmoCount;
    }

    private void OnGUI()
    {
        GUI.Label( new Rect(5f, 20f, 300f, 150f), "Ammo: " + AmmoCount.ToString());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<AmmoPickup>(out AmmoPickup ammoPickup) && CanPickupAmmo(ammoPickup.AmmoAmount))
        {
            PickupAmmo(ammoPickup.AmmoAmount);
            Destroy(ammoPickup.gameObject);
        }
    }
}
