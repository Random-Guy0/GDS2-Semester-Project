using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private bool offSwitchX;
    private bool offSwitchY;
    public Camera thisCamera;
    private Transform player;
    public float cameraSpeed = 1;
    public bool camRecenter;
    public SectionsManager sectionManager;
    public bool diagonalArea = false;
    // Start is called before the first frame update
    void Start()
    {
        camRecenter = false;
        OnOffSwitchX(true);
        OnOffSwitchY(true);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (diagonalArea == true)
        {
            Vector2 playPosition = new Vector2(player.position.x, player.position.y);
            Vector2 camPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Debug.Log("MainCamera begining camera tracking determination");
            sectionManager.DiagonalCameraTrackingDecider();
            Debug.Log("Cam Position = " + gameObject.transform.position);
            if (playPosition == camPosition)
            {
                Debug.Log("CamRecenter reset to false");
                camRecenter = false;
            }
            Debug.Log("CamRecenter = " + camRecenter);
            if (camRecenter == true && playPosition != camPosition)
            {
                Debug.Log("CamRecenter beginning offswitch = true");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, player.position.y, -10), cameraSpeed * Time.deltaTime);
                offSwitchX = true;
                offSwitchY = true;
            }
            Debug.Log("OnOffSwitchX = " + offSwitchX);
            if (offSwitchX == false)
            {
                Debug.Log("Cam Follows Player, new Position equal to player position || X");
                transform.position = new Vector3(player.position.x, gameObject.transform.position.y, -10);
            }
            Debug.Log("OnOffSwitchY = " + offSwitchY);
            if (offSwitchY == false)
            {
                Debug.Log("Cam Follows Player, new Position equal to player position || Y");
                transform.position = new Vector3(gameObject.transform.position.x, player.position.y, -10);
            }
        }
        else
        {
            Debug.Log("MainCamera begining camera tracking determination");
            sectionManager.CameraTrackingDecider();
            Debug.Log("Cam Position = " + gameObject.transform.position + " and Player Position = " + player.position.x);
            if (gameObject.transform.position.x == player.position.x)
            {
                Debug.Log("CamRecenter reset to false");
                camRecenter = false;
            }
            Debug.Log("CamRecenter = " + camRecenter);
            if (camRecenter == true && gameObject.transform.position.x != player.position.x)
            {
                Debug.Log("CamRecenter beginning offswitch = true");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, 0, -10), cameraSpeed * Time.deltaTime);
                offSwitchX = true;
            }
            Debug.Log("OnOffSwitch = " + offSwitchX);
            if (offSwitchX == false)
            {
                Debug.Log("Cam Follows Player, new Position equal to player position");
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

    

    public void ResetCamPosition()
    {

        Vector3 begPlayerPos = thisCamera.WorldToViewportPoint(player.position);
        if (begPlayerPos.x >= 0.5f)
        {
            StartCoroutine(CamReFollow());
            offSwitchX = false;
        }
    }

    IEnumerator CamReFollow()
    {

        camRecenter = true;
        yield return null;
    }
}
