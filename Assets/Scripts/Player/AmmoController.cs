using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    [SerializeField] private int maxAmmo = 20;
    [SerializeField] private int startingAmmo = 5;
    private int _ammoCount;

    public int AmmoCount
    {
        get => _ammoCount;
        private set
        {
            _ammoCount = value;
            GameManager.Instance.GameplayUI.UpdateAmmoUI(value);
        }
    }

    private void Start()
    {
        AmmoCount = startingAmmo;
        GameManager.Instance.GameplayUI.SetInitialAmmoUI(AmmoCount, maxAmmo);
    }

    public void PickupAmmo(int amount)
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
}
