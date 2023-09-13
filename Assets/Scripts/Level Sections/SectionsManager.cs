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

    private bool section1Complete;
    private bool section2Complete;
    private bool section3Complete;
    private bool section4Complete;
    private bool section5Complete;
    private bool section6Complete;
    // Start is called before the first frame update
    void Start()
    {
        section1Complete = false;
        section2Complete = false;
        section3Complete = false;
        section4Complete = false;
        section5Complete = false;
        section6Complete = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSection == 1)
        {
            Section1();
        }
        else if (currentSection == 2)
        {
            if (section1Complete == false)
            {
                camFollow.ResetCamPosition();
                section1Complete = true;
            }
            Section2();
            section1.SetActive(false);
            
        }
        else if (currentSection == 3)
        {
            if (section2Complete == false)
            {
                camFollow.ResetCamPosition();
                section2Complete = true;
            }
            Section3();
            section2.SetActive(false);

        }
        else if (currentSection == 4)
        {
            if (section3Complete == false)
            {
                camFollow.ResetCamPosition();
                section3Complete = true;
            }
            Section4();
            section3.SetActive(false);
        }
        else if (currentSection == 5)
        {
            if (section4Complete == false)
            {
                camFollow.ResetCamPosition();
                section4Complete = true;
            }
            Section5();
            section4.SetActive(false);
        }
        else if (currentSection == 6)
        {
            if (section5Complete == false)
            {
                camFollow.ResetCamPosition();
                section5Complete = true;
            }
            Section6();
            section5.SetActive(false);
        }
    }

    public void SectionComplete()
    {
        currentSection = currentSection + 1.0f;
    }

    public void Section1()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(section1.transform.position);
        if(pos.x <= 1.0f)
        {
            camFollow.OnOffSwitch(true);
        }

        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (playerPos.x <= 0.5f)
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

        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (playerPos.x <= 0.5f)
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

        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (playerPos.x <= 0.5f)
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

        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (playerPos.x <= 0.5f)
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

        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (playerPos.x <= 0.5f)
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

        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (playerPos.x <= 0.5f)
        {
            camFollow.OnOffSwitch(false);
        }
    }
}
