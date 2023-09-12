using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 20;
    private int ammo;

    private void Start()
    {
        ammo = maxAmmo;
    }

    private void PickupAmmo(int amount)
    {
        ammo += amount;
        if (ammo > maxAmmo)
        {
            ammo = maxAmmo;
        }
    }

    private bool CanPickupAmmo(int amount)
    {
        return ammo + amount <= maxAmmo;
    }

    public void UseAmmo(int amount)
    {
        if (CanUseAmmo(amount))
        {
            ammo -= amount;
        }
    }

    public bool CanUseAmmo(int amount)
    {
        return amount <= ammo;
    }

    private void OnGUI()
    {
        GUI.Label( new Rect(5f, 20f, 300f, 150f), "Ammo: " + ammo.ToString());
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
