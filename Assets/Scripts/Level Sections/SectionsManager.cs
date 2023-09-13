using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionsManager : MonoBehaviour
{
    public Camera mainCamera;
    public float currentSection = 1;
    public GameObject section0;
    public GameObject section1;
    public GameObject section2;
    public GameObject section3;
    public GameObject section4;
    public GameObject section5;
    public GameObject section6;
    public FollowPlayer camFollow;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        Section0();
        if (currentSection == 1)
        {
            Section1();
        }
        else if (currentSection == 2)
        {
            Section2();
            section1.SetActive(false);
            mainCamera.transform.position = new Vector3(player.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        else if (currentSection == 3)
        {
            Section3();
            section2.SetActive(false);
            mainCamera.transform.position = new Vector3(player.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        else if (currentSection == 4)
        {
            Section4();
            section3.SetActive(false);
            mainCamera.transform.position = new Vector3(player.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        else if (currentSection == 5)
        {
            Section5();
            section4.SetActive(false);
            mainCamera.transform.position = new Vector3(player.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        else if (currentSection == 6)
        {
            Section6();
            section5.SetActive(false);
            mainCamera.transform.position = new Vector3(player.transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
    }

    public void Section0()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(section0.transform.position);
        if (pos.x <= 0.0f)
        {
            camFollow.OnOffSwitch(true);
        }

        if (player.transform.position.x >= 0.5f)
        {
            camFollow.OnOffSwitch(false);
        }
    }

    public void Section1()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(section1.transform.position);
        if(pos.x <= 1.0f)
        {
            camFollow.OnOffSwitch(true);
        }
        
        if (player.transform.position.x <= 0.5f)
        {
            camFollow.OnOffSwitch(false);
        }
    }
    public void Section2()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(section2.transform.position);
        if (pos.x <= 1.0f)
        {
            camFollow.OnOffSwitch(true);
        }

        if (player.transform.position.x <= 0.5f)
        {
            camFollow.OnOffSwitch(false);
        }
    }

    public void Section3()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(section3.transform.position);
        if (pos.x <= 1.0f)
        {
            camFollow.OnOffSwitch(true);
        }

        if (player.transform.position.x <= 0.5f)
        {
            camFollow.OnOffSwitch(false);
        }
    }

    public void Section4()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(section4.transform.position);
        if (pos.x <= 1.0f)
        {
            camFollow.OnOffSwitch(true);
        }

        if (player.transform.position.x <= 0.5f)
        {
            camFollow.OnOffSwitch(false);
        }
    }

    public void Section5()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(section5.transform.position);
        if (pos.x <= 1.0f)
        {
            camFollow.OnOffSwitch(true);
        }

        if (player.transform.position.x <= 0.5f)
        {
            camFollow.OnOffSwitch(false);
        }
    }

    public void Section6()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(section6.transform.position);
        if (pos.x <= 1.0f)
        {
            camFollow.OnOffSwitch(true);
        }

        if (player.transform.position.x <= 0.5f)
        {
            camFollow.OnOffSwitch(false);
        }
    }
}
