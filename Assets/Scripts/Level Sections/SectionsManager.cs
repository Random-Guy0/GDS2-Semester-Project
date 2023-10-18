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
    [SerializeField] public bool endOfArea = false;
    [SerializeField] private bool tutorialLevel = false;
    public GameObject rightWall;
    [SerializeField] private bool camAlreadyTracking = false;

    public int numberOfAreas;

    public List<AreaSections> areas = new List<AreaSections>();

    void Start()
    {

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            Debug.Log("Tutorial Level");
            tutorialLevel = true;
        }
        else
        {
            Debug.Log("Main Level");
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
            //When the currentArea is greater than the total number of areas (After the final area has been beaten):
            if (currentArea >= areas.Count)
            {
                SectionRunTime();
            }
            else
            {
                //if the current section the player is in is the first section of that area:
                if (areas[currentArea].sectionCount == 0)
                {
                    SectionRunTime();
                }
                else
                {
                    // if the previous section in current area's bool list == false & the section for the current area is not the beginning area section
                    if (areas[currentArea].sectionsCompleted[areas[currentArea].sectionCount - 1] == false && endOfArea == false)
                    {
                        Debug.Log("endOfArea = " + endOfArea);
                        if (endOfArea == false)
                        {
                            // set the new next sections manager to active including enemies.
                            areas[currentArea].areaEnemyManagers[areas[currentArea].sectionCount].gameObject.SetActive(true);
                        }
                        camFollow.ResetCamPosition();

                        // set the previous sections bool completed list object to true
                        areas[currentArea].sectionsCompleted[areas[currentArea].sectionCount - 1] = true;
                    }
                    SectionRunTime();
                    if (endOfArea == false)
                    {
                        // set the previous (Was current but just completed the previous section so current is one forward) wall to false
                        areas[currentArea].section[areas[currentArea].sectionCount - 1].SetActive(false);
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
        ++currentArea;
        areas[currentArea].areaEnemyManagers[areas[currentArea].sectionCount].gameObject.SetActive(true);
        playerAreaIn = currentArea;
        endOfArea = false;
        Vector3 newCameraPosition = new Vector3(72.13f, 0f, -10f);
        mainCamera.transform.position = newCameraPosition;
        camFollow.OnOffSwitch(true);
    }

    public void SectionRunTime()
    {
        if (tutorialLevel == false)
        {
            Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);


            // For the Next Section
            Vector3 sectionWall = mainCamera.WorldToViewportPoint(areas[currentArea].section[areas[currentArea].sectionCount].transform.position);
            Debug.Log("SectionRunTime Code 1 sectionWall pos.x = " + sectionWall.x);
            Debug.Log("Player Position = " + playerPos.x);
            if (sectionWall.x <= 1.0f && playerPos.x > 0.5f)
            {
                Debug.Log("SectionRunTime Code 1 == true");
                camFollow.OnOffSwitch(true);
            }
            else
            {
                Debug.Log("SectionRunTime Code 1 == false");
                camFollow.OnOffSwitch(false);
            }


            //For the Left Most Wall

            Vector3 leftWall = mainCamera.WorldToViewportPoint(areas[currentArea].areaWalls[0].transform.position);
            Debug.Log("SectionRunTime Code 2 leftWall pos.x = " + leftWall.x);
            if (leftWall.x >= -0.5f)
            {
                Debug.Log("SectionRunTime Code 2 leftWall pos.x == true");
                Debug.Log("Player Position = " + playerPos.x);
                if (leftWall.x >= 0.0f && playerPos.x < 0.5f)
                {
                    Debug.Log("SectionRunTime Code 2 == true");
                    camFollow.OnOffSwitch(true);
                }
                else
                {
                    Debug.Log("SectionRunTime Code 2 == false");
                    camFollow.OnOffSwitch(false);
                }
            }

            //For the Right Most Wall

            Vector3 rightWall = mainCamera.WorldToViewportPoint(areas[currentArea].areaWalls[1].transform.position);
            Debug.Log("SectionRunTime Code 3 rightWall pos.x = " + rightWall.x);
            if (rightWall.x <= 1.5f)
            {
                if (endOfArea == false)
                {
                    Debug.Log("End Of Area = true");
                    endOfArea = true;
                }

                Debug.Log("SectionRunTime Code 3 rightWall pos.x == true");
                Debug.Log("Player Position = " + playerPos.x);
                if (rightWall.x <= 1.0f && playerPos.x > 0.5f)
                {
                    Debug.Log("SectionRunTime Code 3 == true");
                    camFollow.OnOffSwitch(true);
                }
                else
                {
                    Debug.Log("SectionRunTime Code 3 == false");
                    camFollow.OnOffSwitch(false);
                }
            }





            /*
            // for the position of the wall in the list using the current section count || For the Current Next Section
            Vector3 pos = mainCamera.WorldToViewportPoint(areas[currentArea].section[areas[currentArea].sectionCount].transform.position);
            if (pos.x <= 1.0f)
            {
                Debug.Log("SectionRunTime Code 1 == true");
                camFollow.OnOffSwitch(true);
            }



            // For the Right Most Wall of the Area (Currently Not Following Player Again Afterwards
            Vector3 playerPos1;
            if (playerAreaIn == currentArea - 1)
            {
                Debug.Log("SectionRunTime Code 2 if statement == true ");
                playerPos1 = mainCamera.WorldToViewportPoint(areas[currentArea].areaWalls[1].transform.position);
                Debug.Log("playerPos1.x == " + playerPos1.x);
                if (playerPos1.x <= 1.0f)
                {
                    Debug.Log("SectionRunTime Code 2 == true");
                    camFollow.OnOffSwitch(true);
                }

            }
            else
            {
                Vector3 playerPos8 = mainCamera.WorldToViewportPoint(areas[currentArea].areaWalls[1].transform.position);
                if (playerPos8.x <= 1.0f)
                {
                    Debug.Log("SectionRunTime Code 3 == true");
                    camFollow.OnOffSwitch(true);
                }
            }

            // For if the cam is going to cross over the left wall of the area
            Vector3 playerPos = mainCamera.WorldToViewportPoint(areas[currentArea].areaWalls[0].transform.position);
            if (playerPos.x <= 0.5f && playerPos.x >= -1.0f)
            {
                Debug.Log("SectionRunTime Code 4 == true");
                camFollow.OnOffSwitch(false);
            }

            */
        }
        else {
            Debug.Log("Tutorial Level SectionRunTime");
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
