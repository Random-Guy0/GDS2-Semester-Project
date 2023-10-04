using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaPortal : Portal
{
    [SerializeField] private Vector2 teleportToPosition;
    [SerializeField] public SectionsManager secManager;

    protected override void EnterPortal(PlayerMovement player)
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            SceneManager.LoadScene(2);
        }
        base.EnterPortal(player);
        player.transform.position = teleportToPosition;

        secManager.ActivateNewAreaEnemies();
    }
}
