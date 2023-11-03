using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public bool debugConsoleLog;
    public Camera thisCamera;
    public float cameraSpeed = 1;
    public bool camRecenter;
    public SectionsManager sectionManager;
    public bool diagonalArea = false;

    [SerializeField] private bool offSwitchX;
    [SerializeField] private bool offSwitchY;
    [SerializeField] private bool beginRecentering = false;
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if (debugConsoleLog == true) { debugConsoleLog = true; }
        else { debugConsoleLog = false; }
        camRecenter = false;
        OnOffSwitchX(true);
        OnOffSwitchY(true);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        if (debugConsoleLog == true) { Debug.LogWarning("Diagonal Area bool = " + diagonalArea); }

        if (diagonalArea == true)
        {
            Vector2 playPosition = new Vector2(player.position.x, player.position.y);
            Vector2 camPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

            if (debugConsoleLog == true) { Debug.Log("MainCamera begining camera tracking determination"); }

            sectionManager.DiagonalCameraTrackingDecider();
            sectionManager.CameraTrackingDecider();

            if (debugConsoleLog == true) { Debug.Log("Cam Position = " + gameObject.transform.position); }

            if (playPosition == camPosition)
            {

                if (debugConsoleLog == true) { Debug.Log("CamRecenter reset to false"); }

                camRecenter = false;
                if (beginRecentering)
                {
                    beginRecentering = false;
                }
            }

            if (debugConsoleLog == true) { Debug.Log("CamRecenter = " + camRecenter); }

            if (camRecenter == true && playPosition != camPosition)
            {
                if (beginRecentering)
                {

                    if (debugConsoleLog == true) { Debug.Log("CamRecenter beginning offswitch = true"); }

                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, -10), cameraSpeed * Time.deltaTime);
                    offSwitchX = true;
                    offSwitchY = true;
                }
            }

            if (debugConsoleLog == true) { Debug.Log("OnOffSwitchX = " + offSwitchX); }

            if (offSwitchX == false)
            {

                if (debugConsoleLog == true) { Debug.Log("Cam Follows Player, new Position equal to player position || X"); }

                transform.position = new Vector3(player.position.x, gameObject.transform.position.y, -10);
            }
            if (debugConsoleLog == true) { Debug.Log("OnOffSwitchY = " + offSwitchY); }

            if (offSwitchY == false)
            {

                if (debugConsoleLog == true) { Debug.Log("Cam Follows Player, new Position equal to player position || Y"); }

                transform.position = new Vector3(gameObject.transform.position.x, player.position.y, -10);
            }
        }
        else
        {

            if (debugConsoleLog == true) { Debug.Log("MainCamera begining camera tracking determination"); }

            sectionManager.CameraTrackingDecider();

            if (debugConsoleLog == true) { Debug.Log("Cam Position = " + gameObject.transform.position + " and Player Position = " + player.position.x); }

            if (gameObject.transform.position.x == player.position.x)
            {

                if (debugConsoleLog == true) { Debug.Log("CamRecenter reset to false"); }

                camRecenter = false;
                if (beginRecentering)
                {
                    beginRecentering = false;
                }
            }

            if (debugConsoleLog == true) { Debug.Log("CamRecenter = " + camRecenter); }

            if (camRecenter == true && gameObject.transform.position.x != player.position.x)
            {
                if (beginRecentering)
                {

                    if (debugConsoleLog == true) { Debug.Log("CamRecenter beginning offswitch = true"); }

                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, sectionManager.areas[sectionManager.currentArea].cameraLocationSpawn.position.y, -10), cameraSpeed * Time.deltaTime);
                    offSwitchX = true;
                }
            }
            if (debugConsoleLog == true) { Debug.Log("OnOffSwitch = " + offSwitchX); }

            if (offSwitchX == false)
            {

                if (debugConsoleLog == true) { Debug.Log("Cam Follows Player, new Position equal to player position"); }

                transform.position = new Vector3(player.position.x, gameObject.transform.position.y, -10);
            }
        }


        /*
        
        if (gameObject.transform.position.x == player.position.x)
            {
                camRecenter = false;
            }
            if (camRecenter == true && gameObject.transform.position.x != player.position.x)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, 0, -10), cameraSpeed * Time.deltaTime);
                offSwitch = true;
            }
            if (offSwitch == false)
            {
                transform.position = new Vector3(player.position.x, 0, -10);
            }
            OnOffSwitch(false);
         
        
        */
    }

    public void OnOffSwitchX(bool value)
    {
        offSwitchX = value;
    }

    public void OnOffSwitchY(bool value)
    {
        offSwitchY = value;
    }

    

    public void ResetCamPosition(bool firstSection, bool diagonalArea)
    {
        camRecenter = true;
        if (diagonalArea) { 
            sectionManager.DiagonalCameraTrackingDecider();
            sectionManager.CameraTrackingDecider();
            beginRecentering = true; }
        else { 
            sectionManager.CameraTrackingDecider(); 
            beginRecentering = true; }
    }
}
