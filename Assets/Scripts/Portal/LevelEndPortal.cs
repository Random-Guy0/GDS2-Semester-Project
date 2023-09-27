using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPortal : Portal
{
    [SerializeField] private GameObject levelEndScreen;

    protected override void EnterPortal(PlayerMovement player)
    {
        base.EnterPortal(player);
        levelEndScreen.SetActive(true);
    }
}
