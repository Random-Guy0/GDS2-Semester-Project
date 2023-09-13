using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    private bool offSwitch;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (offSwitch == false)
        {
            transform.position = new Vector3(player.position.x, 0, -10);
        }
    }

    public void OnOffSwitch(bool value)
    {
        offSwitch = value;
    }
}
