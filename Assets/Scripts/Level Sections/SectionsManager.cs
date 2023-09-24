using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionsManager : MonoBehaviour
{
    public Camera mainCamera;
    public int currentSection = 0;
    public FollowPlayer camFollow;
    public GameObject player;

    public List<GameObject> section = new List<GameObject>();

    private List<bool> sectionComplete = new List<bool>();

    public List<SectionEnemyManager> sectionsManagers = new List<SectionEnemyManager>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < sectionsManagers.Count; i++)
        {
            sectionComplete.Add(false);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSection == 0)
        {
            SectionRunTime();
            Debug.Log("currentSection = " + currentSection);
        }
        else
        {
            Debug.Log("currentSection = " + currentSection);
            int preSection = currentSection - 1;
            if (sectionComplete[currentSection - 1] == false)
            {
                sectionsManagers[currentSection].gameObject.SetActive(true);
                camFollow.ResetCamPosition();
                sectionComplete[currentSection - 1] = true;
            }
            SectionRunTime();
            section[currentSection - 1].gameObject.SetActive(false);
        }

        //New Code
        

        /*
        // Old Code
        else if (currentSection == 2)
        {
            if (section1Complete == false)
            {
                section2Manager.SetActive(true);
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
                section3Manager.SetActive(true);
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
                section4Manager.SetActive(true);
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
                section5Manager.SetActive(true);
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
                section6Manager.SetActive(true);
                camFollow.ResetCamPosition();
                section5Complete = true;
            }
            Section6();
            section5.SetActive(false);
        }
        */
    }

    public void SectionComplete()
    {
        currentSection++;
    }


    
    //New Code
    public void SectionRunTime()
    {
        Vector3 pos = mainCamera.WorldToViewportPoint(section[currentSection].transform.position);
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

    /*
    //Old Code
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
    */
}
