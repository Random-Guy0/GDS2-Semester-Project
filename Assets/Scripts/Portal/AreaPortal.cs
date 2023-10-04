using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPortal : Portal
{
    [SerializeField] private Vector2 teleportToPosition;
    [SerializeField] public SectionsManager secManager;
    
    //remove after Sprint 3
    [SerializeField] private AudioSource EgyptLevel1Audio;
    [SerializeField] private AudioSource EgyptLevel2Audio;

    protected override void EnterPortal(PlayerMovement player)
    {
        base.EnterPortal(player);
        player.transform.position = teleportToPosition;

        secManager.ActivateNewAreaEnemies();
        
        EgyptLevel1Audio.Stop();
        EgyptLevel2Audio.Play();
    }
}
