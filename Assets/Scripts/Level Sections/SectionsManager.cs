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
    [SerializeField] public List<Transform> camAreaPosition;

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
            if (currentArea >= areas.Count - 1)
            {
                Debug.Log("Current Area Count Exceeded");
                SectionRunTime();
            }
            else
            {
                //if the current section the player is in is the first section of that area:
                if (areas[currentArea].sectionCount == 0)
                {
                    Debug.Log("Beginning Area in Update SectionsManager");
                    camFollow.OnOffSwitchX(true);
                    SectionRunTime();
                }
                else
                {
                    Debug.Log("No first Area");
                    Debug.Log(areas[currentArea].sectionsCompleted[areas[currentArea].sectionCount - 1] + " and " + endOfArea);
                    // if the previous section in current area's bool list == false & the section for the current area is not the beginning area section
                    if (areas[currentArea].sectionsCompleted[areas[currentArea].sectionCount - 1] == false && endOfArea == false)
                    {

                        Debug.Log("endOfArea = " + endOfArea);
                        if (endOfArea == false)
                        {
                            // set the new next sections manager to active including enemies.
                            areas[currentArea].areaEnemyManagers[areas[currentArea].sectionCount].gameObject.SetActive(true);
                        }
                        if (areas[currentArea].justBeganSecondSection != true)
                        {
                            camFollow.ResetCamPosition();
                        }
                        else
                        {
                            areas[currentArea].justBeganSecondSection = false;
                        }
                        

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

    public void ActivateNewAreaEnemies(GameObject nextArea)
    {
        ++currentArea;
        if (currentArea == 2)
        {
            Debug.Log("Diagonal Bool set to " + camFollow.diagonalArea);
            camFollow.diagonalArea = true;
            camFollow.OnOffSwitchY(true);
        }
        else
        {
            Debug.Log("Diagonal Bool set to " + camFollow.diagonalArea);
            camFollow.diagonalArea = false;
        }
        nextArea.SetActive(true);
        areas[currentArea].areaEnemyManagers[areas[currentArea].sectionCount].gameObject.SetActive(true);
        Debug.Log("Current Area in Activate New Area Enemies = " + currentArea + "  areas.count is = " + areas.Count);
        playerAreaIn = currentArea;
        if (currentArea >= areas.Count - 1)
        {
            Debug.Log("CurrentArea >= areas.count");
            endOfArea = true;
            areas[currentArea].beginningAreaSection = false;
        }
        else
        {
            Debug.Log("CurrentArea is not >= areas.count");
            endOfArea = false;
        }
        
        mainCamera.transform.position = new Vector3(camAreaPosition[currentArea].position.x, camAreaPosition[currentArea].position.y, -10f);
        camFollow.OnOffSwitchX(true);
    }

    public int CurrentArea()
    {
        int area = currentArea + 1;
        return area;
    }

    public void SectionRunTime()
    {

            Debug.Log("Begining Area bool = " + areas[currentArea].beginningAreaSection);
            if (areas[currentArea].beginningAreaSection == false || currentArea == 2)
            {
                Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);

                if (currentArea < 3)
                {
                // For the Next Section
                Vector3 sectionWall = mainCamera.WorldToViewportPoint(areas[currentArea].section[areas[currentArea].sectionCount].transform.position);
                Debug.Log("SectionRunTime Code 1 sectionWall pos.x = " + sectionWall.x);
                Debug.Log("Player Position = " + playerPos.x);
                if (sectionWall.x <= 1.0f && playerPos.x > 0.5f)
                {
                    Debug.Log("SectionRunTime Code 1 == true");
                    camFollow.OnOffSwitchX(true);
                }
                else
                {
                    Debug.Log("SectionRunTime Code 1 == false");
                    camFollow.OnOffSwitchX(false);
                }
                }



                //For the Left Most Wall
                Vector3 leftWall = mainCamera.WorldToViewportPoint(areas[currentArea].areaWalls[0].transform.position);
                Debug.Log("SectionRunTime Code 2 leftWall pos.x = " + leftWall.x);
                if (leftWall.x >= -0.2f)
                {
                    Debug.Log("SectionRunTime Code 2 leftWall pos.x == true");
                    Debug.Log("Player Position = " + playerPos.x);
                    if (leftWall.x >= 0.0f && playerPos.x < 0.5f)
                    {
                        Debug.Log("SectionRunTime Code 2 == true");
                        camFollow.OnOffSwitchX(true);
                    }
                    else
                    {
                        Debug.Log("SectionRunTime Code 2 == false");
                        camFollow.OnOffSwitchX(false);
                    }
                }

                //For the Right Most Wall
                Vector3 rightWall = mainCamera.WorldToViewportPoint(areas[currentArea].areaWalls[1].transform.position);
                Debug.Log("SectionRunTime Code 3 rightWall pos.x = " + rightWall.x);
                if (rightWall.x <= 1.2f)
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
                        camFollow.OnOffSwitchX(true);
                    }
                    else
                    {
                        Debug.Log("SectionRunTime Code 3 == false");
                        camFollow.OnOffSwitchX(false);
                    }
                }




            if (camFollow.diagonalArea == true)
                {
                    Debug.Log("Diagonal Area = true");
                    Vector3 bottomWall = mainCamera.WorldToViewportPoint(areas[currentArea].areaWalls[2].transform.position);
                    Vector3 topWall = mainCamera.WorldToViewportPoint(areas[currentArea].areaWalls[3].transform.position);
                    Vector3 playBottom = mainCamera.WorldToViewportPoint(player.transform.position);
                    if (bottomWall.y >= -0.2f)
                    {
                        if (bottomWall.y >= 0.0f && playBottom.y < 0.5f)
                        {
                            camFollow.OnOffSwitchY(true);
                        }
                        else
                        {
                            camFollow.OnOffSwitchY(false);
                        }
                    }
                    if (topWall.y <= 1.2f)
                    {
                        if (topWall.y <= 1.0f && playerPos.y > 0.5f)
                        {
                            camFollow.OnOffSwitchY(true);
                        }
                        else
                        {
                            camFollow.OnOffSwitchY(false);
                        }
                    }
                }
            }
       /*
        else 
        {
            Debug.Log("Tutorial Level SectionRunTime");
            Vector3 playerPos5 = mainCamera.WorldToViewportPoint(player.transform.position);

            if (playerPos5.x <= 0.5f)
            {
                camFollow.OnOffSwitchX(false);
            }

            Vector3 playerPosTut = mainCamera.WorldToViewportPoint(rightWall.transform.position);

            if (playerPosTut.x <= 1.0f)
            {
                camFollow.OnOffSwitchX(true);
            }
        }
       */

    }
}
