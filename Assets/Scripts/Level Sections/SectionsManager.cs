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
                //SectionRunTime();
            }
            else
            {
                //if the current section the player is in is the first section of that area:
                if (areas[currentArea].sectionCount == 0)
                {
                    Debug.Log("Beginning Area in Update SectionsManager");
                    camFollow.OnOffSwitchX(true);
                    //SectionRunTime();
                }
                else if (endOfArea == false)
                {
                    Debug.Log("No first Area");
                    Debug.LogWarning(areas[currentArea].sectionsCompleted[areas[currentArea].sectionCount - 1] + " and " + endOfArea);
                    // if the previous section in current area's bool list == false & the section for the current area is not the beginning area section
                    if (areas[currentArea].sectionsCompleted[areas[currentArea].sectionCount - 1] == false)
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
                    //SectionRunTime();
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


    public void CameraTrackingDecider()
    {
        int measurment = 0;
        Vector3 leftSide = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, player.transform.position.z));
        Vector3 rightSide = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, player.transform.position.z));

        Debug.LogWarning("leftSide = " + leftSide + " and rightSide = " + rightSide);

        float leftGap = leftSide.x - areas[currentArea].areaWalls[0].transform.position.x;
        float rightGap = rightSide.x - areas[currentArea].section[areas[currentArea].sectionCount].transform.position.x;

        Debug.LogWarning("rightSide wall is " + areas[currentArea].section[areas[currentArea].sectionCount].name);
        Debug.LogWarning("rightside.x = " + rightSide.x + " and rightSide Wall is = " + areas[currentArea].section[areas[currentArea].sectionCount].transform.position.x);
        Debug.LogWarning("leftGap = " + leftGap + " and rightGap = " + rightGap);

        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (leftGap <= 0) { measurment = 1; }
        else if (rightGap >= 0) { measurment = 2; }
        
        switch(measurment)
        {
            case 1:
                Debug.Log("Case 1");
                camFollow.OnOffSwitchX(true);
                goto case 3;
            case 2:
                Debug.Log("Case 2");
                camFollow.OnOffSwitchX(true);
                goto case 4;
            case 3:
                Debug.Log("Case 3");
                if (playerPos.x > 0.5f) { camFollow.OnOffSwitchX(false); Debug.Log("Case 3, if = true"); }
                break;
            case 4:
                Debug.Log("Case 4");
                if (playerPos.x < 0.5f) { camFollow.OnOffSwitchX(false); Debug.Log("Case 4, if = true"); }
                break;
            default:
                Debug.Log("Case Default");
                camFollow.OnOffSwitchX(false);
                break;
        }
    }

    public void DiagonalCameraTrackingDecider()
    {
        int measurment = 0;
        Vector3 leftSide = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, player.transform.position.z));
        Vector3 rightSide = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, player.transform.position.z));
        Vector3 topSide = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, player.transform.position.z));
        Vector3 bottomSide = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, player.transform.position.z));

        Debug.LogWarning("leftSide = " + leftSide + ", rightSide = " + rightSide + ", topSide = " + topSide + " and bottomSide = " + bottomSide);

        float leftGap = leftSide.x - areas[currentArea].areaWalls[0].transform.position.x;
        float rightGap = rightSide.x - areas[currentArea].section[areas[currentArea].sectionCount].transform.position.x;
        float topGap = topSide.y - areas[currentArea].areaWalls[2].transform.position.x;
        float bottomGap = bottomSide.y - areas[currentArea].areaWalls[3].transform.position.x;

        Debug.LogWarning("rightSide wall is " + areas[currentArea].section[areas[currentArea].sectionCount].name);
        Debug.LogWarning("rightSide.x = " + rightSide.x + " and rightSide Wall is = " + areas[currentArea].section[areas[currentArea].sectionCount].transform.position.x);
        Debug.LogWarning("topSide.x = " + topSide.x + " and topSide Wall is = " + areas[currentArea].areaWalls[2].transform.position.x);
        Debug.LogWarning("rightside.x = " + bottomSide.x + " and bottomSide Wall is = " + areas[currentArea].areaWalls[3].transform.position.x);
        Debug.LogWarning("leftGap = " + leftGap + ", rightGap = " + rightGap + ", topGap = " + topGap + " and bottomGap = " + bottomGap);

        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (leftGap <= 0) { measurment = 1; }
        else if (rightGap >= 0) { measurment = 2; }
        else if (topGap >= 0) { measurment = 3; }
        else if (bottomGap <= 0) { measurment = 4; }

        switch (measurment)
        {
            case 1:
                Debug.Log("Case 1");
                camFollow.OnOffSwitchX(true);
                goto case 3;
            case 2:
                Debug.Log("Case 2");
                camFollow.OnOffSwitchX(true);
                goto case 4;
            case 3:
                Debug.Log("Case 3");
                camFollow.OnOffSwitchY(true);
                goto case 7;
            case 4:
                Debug.Log("Case 4");
                camFollow.OnOffSwitchY(true);
                goto case 8;
            case 5:
                Debug.Log("Case 5");
                if (playerPos.x > 0.5f) { camFollow.OnOffSwitchX(false); Debug.Log("Case 5, if = true"); }
                break;
            case 6:
                Debug.Log("Case 6");
                if (playerPos.x < 0.5f) { camFollow.OnOffSwitchX(false); Debug.Log("Case 6, if = true"); }
                break;
            case 7:
                Debug.Log("Case 7");
                if (playerPos.y < 0.5f) { camFollow.OnOffSwitchY(false); Debug.Log("Case 7, if = true"); }
                goto case 5;
            case 8:
                Debug.Log("Case 8");
                if (playerPos.y > 0.5f) { camFollow.OnOffSwitchY(false); Debug.Log("Case 8, if = true"); }
                goto case 6;
            default:
                Debug.Log("Case Default");
                camFollow.OnOffSwitchX(false);
                camFollow.OnOffSwitchY(false);
                break;
        }
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
