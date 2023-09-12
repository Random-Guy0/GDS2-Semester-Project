using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text ammoText;

    public void UpdateHealthUI(int health)
    {
        healthText.SetText("Health: {0}", health);
    }

    public void UpdateAmmoUI(int ammo)
    {
        ammoText.SetText("Ammo: {0}", ammo);
    }
}
