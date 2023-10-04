using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectionsManager : MonoBehaviour
{
    public Camera mainCamera;
    public int currentArea = 0;
    public int playerAreaIn = 0;
    public FollowPlayer camFollow;
    public GameObject player;
    [SerializeField] private bool endOfArea;
    [SerializeField] private bool tutorialLevel = false;
    public GameObject rightWall;

    public int numberOfAreas;

    public List<AreaSections> levelAreaSection = new List<AreaSections>();

    void Start()
    {

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            Debug.Log("true");
            tutorialLevel = true;
        }
    }

    void Update()
    {
        if (tutorialLevel == true)
        {
            SectionRunTime();
        }
        else
        {
            if (currentArea >= levelAreaSection.Count)
            {
                SectionRunTime();
            }
            else
            {
                if (levelAreaSection[currentArea].sectionComplelted == 0)
                {
                    SectionRunTime();
                }
                else if (endOfArea == false)
                {
                    if (levelAreaSection[currentArea].sectionsCompleted[levelAreaSection[currentArea].sectionComplelted - 1] == false && levelAreaSection[currentArea].beginningAreaSection == false)
                    {
                        Debug.Log("Yeet");
                        if (endOfArea == false)
                        {
                            levelAreaSection[currentArea].areaEnemyManagers[levelAreaSection[currentArea].sectionComplelted].gameObject.SetActive(true);
                        }
                        camFollow.ResetCamPosition();
                        levelAreaSection[currentArea].sectionsCompleted[levelAreaSection[currentArea].sectionComplelted - 1] = true;
                    }
                    SectionRunTime();
                    if (endOfArea == false)
                    {
                        levelAreaSection[currentArea].section[levelAreaSection[currentArea].sectionComplelted - 1].SetActive(false);
                    }
                }
            }
        }
    }

    public void NewArea()
    {
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
        if (tutorialLevel == false)
        {
            Vector3 pos = mainCamera.WorldToViewportPoint(levelAreaSection[currentArea].section[levelAreaSection[currentArea].sectionComplelted].transform.position);
            if (pos.x <= 1.0f)
            {
                camFollow.OnOffSwitch(true);
            }


            Vector3 playerPos1;
            if (playerAreaIn == currentArea - 1)
            {

                playerPos1 = mainCamera.WorldToViewportPoint(levelAreaSection[currentArea - 1].areaWalls[1].transform.position);

                if (playerPos1.x <= 1.0f)
                {
                    camFollow.OnOffSwitch(true);
                }

            }
            else
            {
                Vector3 playerPos8 = mainCamera.WorldToViewportPoint(levelAreaSection[currentArea].areaWalls[1].transform.position);
                if (playerPos8.x <= 1.0f)
                {
                    camFollow.OnOffSwitch(true);
                }
            }
            Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
            if (playerPos.x <= 0.5f)
            {
                camFollow.OnOffSwitch(false);
            }
        }
        else {

            Vector3 playerPos5 = mainCamera.WorldToViewportPoint(player.transform.position);

            if (playerPos5.x <= 0.5f)
            {
                camFollow.OnOffSwitch(false);
            }

            Vector3 playerPosTut = mainCamera.WorldToViewportPoint(rightWall.transform.position);

            if (playerPosTut.x <= 1.0f)
            {
                camFollow.OnOffSwitch(true);
            }
        }

    }
}
