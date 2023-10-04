using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPortal : Portal
{
    [SerializeField] private Vector2 teleportToPosition;
    [SerializeField] public SectionsManager secManager;

    protected override void EnterPortal(PlayerMovement player)
    {
        base.EnterPortal(player);
        player.transform.position = teleportToPosition;

        secManager.ActivateNewAreaEnemies();
    }
}
