using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionsManager : MonoBehaviour
{
    public Camera mainCamera;
    //public int currentSection = 0;
    public int currentArea = 0;
    public int playerAreaIn = 0;
    public FollowPlayer camFollow;
    public GameObject player;
    [SerializeField] private bool endOfArea;

    public int numberOfAreas;
   // public List<SectionEnemyManager> sectionsManagers = new List<SectionEnemyManager>();

    //public List<GameObject> section = new List<GameObject>();


    //private List<bool> sectionComplete = new List<bool>();

    public List<AreaSections> levelAreaSection = new List<AreaSections>();

    // Start is called before the first frame update
    void Start()
    {
        /*
        for (int i = 0; i < sectionsManagers.Count; i++) {
            sectionComplete.Add(false);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (currentSection == 0) {
            SectionRunTime();
            Debug.Log("currentSection = " + currentSection);
        }
        else {
            Debug.Log("currentSection = " + currentSection);
            if (sectionComplete[currentSection - 1] == false) {
                section[currentSection].gameObject.SetActive(true);
                camFollow.ResetCamPosition();
                sectionComplete[currentSection - 1] = true;
            }
            SectionRunTime();
            //sectionsManagers[currentSection - 1].gameObject.SetActive(false);
        }
        */
        if (currentArea >= levelAreaSection.Count)
        {
            SectionRunTime();
        }
        else
        {
            Debug.Log(levelAreaSection[currentArea].sectionComplelted);
            Debug.Log("Beggining Update Sections Manager");
            if (levelAreaSection[currentArea].sectionComplelted == 0)
            {
                Debug.Log("if Update Section Sections Manager");
                SectionRunTime();
                Debug.Log("Current Area = " + currentArea + " ; Current Section In Area = " + levelAreaSection[currentArea].sectionComplelted);
            }
            else if (endOfArea == false)
            {
                Debug.Log("Else Update Section Sections Manager");
                Debug.Log("Current Area = " + currentArea + " ; Current Section In Area = " + levelAreaSection[currentArea].sectionComplelted);
                if (levelAreaSection[currentArea].sectionsCompleted[levelAreaSection[currentArea].sectionComplelted - 1] == false && levelAreaSection[currentArea].beginningAreaSection == false)
                {
                    Debug.Log("Previous Area == false and Begining area section == false");
                    levelAreaSection[currentArea].areaEnemyManagers[levelAreaSection[currentArea].sectionComplelted].gameObject.SetActive(true);
                    camFollow.ResetCamPosition();
                    levelAreaSection[currentArea].sectionsCompleted[levelAreaSection[currentArea].sectionComplelted - 1] = true;
                }
                SectionRunTime();
                //levelAreaSection[currentArea].areaEnemyManagers[levelAreaSection[currentArea].sectionComplelted - 1].gameObject.SetActive(false);
                if (endOfArea == false)
                {
                    levelAreaSection[currentArea].section[levelAreaSection[currentArea].sectionComplelted - 1].SetActive(false);
                }
            }
        }

        
        
    }

    public void NewArea()
    {
        //currentSection++;
        endOfArea = true;
    }

    public void ActivateNewAreaEnemies()
    {
        levelAreaSection[currentArea].areaEnemyManagers[levelAreaSection[currentArea].sectionComplelted].gameObject.SetActive(true);
        playerAreaIn = currentArea;
        endOfArea = false;
    }

    public void SectionRunTime()
    {
        Debug.Log("SectionRunTime");
        //Section Barrier to Right of Player (Next Section to Progress to)
        Vector3 pos = mainCamera.WorldToViewportPoint(levelAreaSection[currentArea].section[levelAreaSection[currentArea].sectionComplelted].transform.position);
        if (pos.x <= 1.0f) {
            camFollow.OnOffSwitch(true);
        }

            Vector3 playerPos1 = new Vector3();
            if (playerAreaIn == currentArea - 1)
            {
                playerPos1 = mainCamera.WorldToViewportPoint(levelAreaSection[currentArea - 1].areaWalls[1].transform.position);

            }
            else
            {
                playerPos1 = mainCamera.WorldToViewportPoint(levelAreaSection[currentArea].areaWalls[1].transform.position);
            }

            if (playerPos1.x <= 1.0f)
            {
                camFollow.OnOffSwitch(true);
            }

        

        //Section Barrier to Left (Beginning) of player
        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);

        if (playerPos.x <= 0.5f) {
            camFollow.OnOffSwitch(false);
        }
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
