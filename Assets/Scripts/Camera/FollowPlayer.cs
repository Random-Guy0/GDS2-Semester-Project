using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private bool offSwitch;

    private Transform player;
    public float cameraSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        OnOffSwitch(true);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x <= 0.0f)
        {
            OnOffSwitch(true);
        }
        else if (offSwitch == false)
        {
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
        if (player.transform.position.x >= 0.0f)
        {
            StartCoroutine(CamReFollow());
            offSwitch = false;
        }
    }

    IEnumerator CamReFollow()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, 0, -10), cameraSpeed * Time.deltaTime);
        yield return null;
    }
}
