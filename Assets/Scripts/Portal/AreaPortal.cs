using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaPortal : Portal
{
    [SerializeField] private Vector2 teleportToPosition;
    [SerializeField] public SectionsManager secManager;
    [SerializeField] public bool ableToEnter = false;
    [SerializeField] public GameObject areaGameObject;
    
    //remove after Sprint 3
   // [SerializeField] private AudioSource EgyptLevel1Audio;
   // [SerializeField] private AudioSource EgyptLevel2Audio;

    protected override void EnterPortal(PlayerMovement player)
    {
        if (ableToEnter == true)
        {
            if (SceneManager.GetActiveScene().name == "Tutorial")
            {
                SceneManager.LoadScene("Egyptian Level");
            }
            base.EnterPortal(player);
            player.transform.position = teleportToPosition;
            if (SceneManager.GetActiveScene().name != "Tutorial" && secManager.currentArea < 3)
            {
                secManager.ActivateNewAreaEnemies(areaGameObject);
            }
            else if (SceneManager.GetActiveScene().name != "Tutorial")
            {
                SceneManager.LoadScene("MainMenuStart");
            }
            

        //    EgyptLevel1Audio.Stop();
       //     EgyptLevel2Audio.Play();
            Debug.Log("New Area Entered");
        }
        
    }
}
