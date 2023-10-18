using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private bool offSwitch;
    public Camera thisCamera;
    private Transform player;
    public float cameraSpeed = 1;
    public bool camRecenter;
    public SectionsManager sectionManager;
    // Start is called before the first frame update
    void Start()
    {
        camRecenter = false;
        OnOffSwitch(true);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Cam Position = " + gameObject.transform.position);
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
            offSwitch = true;
        }
        Debug.Log("OnOffSwitch = " + offSwitch);
        if (offSwitch == false)
        {
            Debug.Log("Cam Follows Player, new Position equal to player position");
            transform.position = new Vector3(player.position.x, 0, -10);
        }
        OnOffSwitch(false);
    }

    public void OnOffSwitch(bool value)
    {
        offSwitch = value;
    }

    public void ResetCamPosition()
    {

        Vector3 begPlayerPos = thisCamera.WorldToViewportPoint(player.position);
        if (begPlayerPos.x >= 0.5f)
        {
            StartCoroutine(CamReFollow());
            offSwitch = false;
        }
    }

    IEnumerator CamReFollow()
    {

        camRecenter = true;
        yield return null;
    }
}
