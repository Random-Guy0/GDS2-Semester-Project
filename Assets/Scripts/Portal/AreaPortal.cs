using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaPortal : Portal
{
    public bool debugConsoleLog = false;
    [SerializeField] private Vector2 teleportToPosition;
    public SectionsManager secManager;
    public bool ableToEnter = false;
    public GameObject areaGameObject;
    public bool alreadyActivated = false;
    
    //remove after Sprint 3
   // [SerializeField] private AudioSource EgyptLevel1Audio;
   // [SerializeField] private AudioSource EgyptLevel2Audio;

    protected override void EnterPortal(PlayerMovement player)
    {
        if (ableToEnter == true)
        {
            //End Portal In Tutorial
            if (SceneManager.GetActiveScene().name == "Tutorial New")
            {
                //Which scene to load once entered portal
                SceneManager.LoadScene("1-Tutorial-to-Egypt");
            }
            //End Portal In Egyptian Level
            if (SceneManager.GetActiveScene().name == "Egyptian Level" && secManager.currentArea == 3)
            {
                if (secManager.areas[secManager.currentArea].areaSectionDisabled)
                {
                    //Which scene to load once entered portal
                    SceneManager.LoadScene("2-Egypt-to-Babylon");
                }

            }
            //End Portal In Babylon Level
            else if (SceneManager.GetActiveScene().name == "Babylon Level" && secManager.currentArea == 3)
            {
                if (secManager.areas[secManager.currentArea].areaSectionDisabled)
                {
                    //Which scene to load once entered portal
                    SceneManager.LoadScene("Victory Scene");
                }

            }
            else if ( secManager.currentArea < 3 && alreadyActivated == false)
            {
                if (debugConsoleLog == true) { Debug.Log("Activating New Area, Current Area = " + secManager.currentArea); }
                secManager.ActivateNewAreaEnemies(areaGameObject);
                alreadyActivated = true;
            }
            base.EnterPortal(player);
            player.transform.position = teleportToPosition;
            
            if (debugConsoleLog == true) { Debug.Log("New Area Entered"); }
        }
    }
}
