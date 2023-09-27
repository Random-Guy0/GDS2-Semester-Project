using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private Image healthIcon;
    private List<Image> healthIcons = new List<Image>();
    
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private Slider ammoSlider;

    public void SetInitialHealthUI(int health, int maxHealth)
    {
        healthIcons.Add(healthIcon);
        
        for (int i = 2; i <= maxHealth; i++)
        {
            Image newHealthIcon = Instantiate(healthIcon, healthIcon.transform.parent);
            newHealthIcon.transform.localPosition = new Vector3((i - 1) * 125f, 0f);
            if (i > health)
            {
                newHealthIcon.gameObject.SetActive(false);
            }
            healthIcons.Add(newHealthIcon);
        }
    }

    public void SetInitialAmmoUI(int ammo, int maxAmmo)
    {
        ammoSlider.maxValue = maxAmmo;
        UpdateAmmoUI(ammo);
    }

    public void UpdateHealthUI(int health)
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            if (i < health)
            {
                healthIcons[i].gameObject.SetActive(true);
            }
            else
            {
                healthIcons[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateAmmoUI(int ammo)
    {
        ammoText.SetText("Soap: {0}", ammo);
        ammoSlider.value = ammo;
    }
}
