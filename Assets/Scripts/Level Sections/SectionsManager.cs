using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectionsManager : MonoBehaviour
{
    [SerializeField] public bool debugConsoleLog;
    public Camera mainCamera;
    public int currentArea = 0;
    public int playerAreaIn = 0;
    public FollowPlayer camFollow;
    public GameObject player;


    [SerializeField] public List<Transform> camAreaPosition;
    [SerializeField] public List<AreaSections> areas = new List<AreaSections>();

    void Start()
    {
        if (debugConsoleLog == true) { debugConsoleLog = true; }
        else { debugConsoleLog = false; }

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            Debug.Log("Tutorial Level");
        }
        else
        {
            Debug.Log("Main Level");
        }
        Debug.LogError("Current Area is " + currentArea);
    }

    void Update()
    {
        if (debugConsoleLog == true)    {   Debug.LogError("Current section count = " + areas[currentArea].sectionCount + " and the current section.count = " + areas[currentArea].section.Count);  }
    }

    public void NextSection(AreaPortal nextAreaPortal)
    {
        if (debugConsoleLog == true)
        {
            Debug.LogError("Current section count = " + areas[currentArea].sectionCount + " and the current section.count = " + areas[currentArea].section.Count);
            Debug.LogError("Current Area = " + currentArea);
        }
        if (areas[currentArea].sectionCount != areas[currentArea].section.Count - 1 && currentArea != 2)
        {
            //check if player was in begining section
            bool beginingSection;
            if (areas[currentArea].sectionCount == 0) { beginingSection = true; }
            else { beginingSection = false; }

            //new section player currently in
            areas[currentArea].section[areas[currentArea].sectionCount].SetActive(false);
            areas[currentArea].sectionsCompleted[areas[currentArea].sectionCount] = true;
            ++areas[currentArea].sectionCount;

            //Set previous section completed bool in list to true
            areas[currentArea].areaEnemyManagers[areas[currentArea].sectionCount].gameObject.SetActive(true);

            //Smooth the cam position to play position if cam was limited
            bool diagonalArea;
            if (currentArea == 2) { diagonalArea = true; }
            else { diagonalArea = false; }
            camFollow.ResetCamPosition(beginingSection, diagonalArea);
        }
        else if (currentArea == 2)
        {
            if (debugConsoleLog == true)     {    Debug.LogWarning("Area 3 only section completed");  }
            areas[currentArea].section[areas[currentArea].sectionCount].SetActive(false);
            areas[currentArea].sectionsCompleted[areas[currentArea].sectionCount] = true;
            ++areas[currentArea].sectionCount;
            camFollow.ResetCamPosition(false, true);
        }
        else
        {
            nextAreaPortal.ableToEnter = true;
        }
    }

    public void NewArea()
    {
       
    }

    public void ActivateNewAreaEnemies(GameObject nextArea)
    {
        if (currentArea < 4)
        {
            ++currentArea;
            if (debugConsoleLog == true)    {   Debug.LogError("Current Area is " + currentArea);   }
            if (currentArea == 2)
            {
                if (debugConsoleLog == true)    {   Debug.Log("Diagonal Bool set to " + camFollow.diagonalArea);    }
                camFollow.diagonalArea = true;
                camFollow.OnOffSwitchY(true);
                camFollow.OnOffSwitchX(true);
            }
            else
            {
                camFollow.diagonalArea = false;
                camFollow.OnOffSwitchX(true);
            }
            if (debugConsoleLog == true)    {   Debug.LogError("Cam Current Position = " + mainCamera.transform.position);  }
            mainCamera.transform.position = new Vector3(camAreaPosition[currentArea].position.x, camAreaPosition[currentArea].position.y, -10f);
            if (debugConsoleLog == true)    {   Debug.LogError("Cam New Position = " + mainCamera.transform.position);  }
            nextArea.SetActive(true);
            //Set First Area New Section Enemies to active
            areas[currentArea].areaEnemyManagers[areas[currentArea].sectionCount].gameObject.SetActive(true);
        }       
    }


    public void CameraTrackingDecider()
    {
        if (debugConsoleLog == true)    {   Debug.LogWarning("Begining CameraTrackingDecider()");   }
        int measurment = 0;
        Vector3 leftSide = mainCamera.ViewportToWorldPoint(new Vector3(0f, 0.5f, player.transform.position.z));
        Vector3 rightSide = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0.5f, player.transform.position.z));
        if (debugConsoleLog == true)    {   Debug.LogWarning("leftSide = " + leftSide + " and rightSide = " + rightSide);   }
        float leftGap = leftSide.x - areas[currentArea].areaWalls[0].transform.position.x;
        float rightGap = rightSide.x - areas[currentArea].section[areas[currentArea].sectionCount].transform.position.x;
        if (debugConsoleLog == true)
        {
            Debug.LogWarning("rightSide wall is " + areas[currentArea].section[areas[currentArea].sectionCount].name);
            Debug.LogWarning("rightside.x = " + rightSide.x + " and rightSide Wall is = " + areas[currentArea].section[areas[currentArea].sectionCount].transform.position.x);
            Debug.LogWarning("leftGap = " + leftGap + " and rightGap = " + rightGap);
        }
        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (debugConsoleLog == true)    {   Debug.LogWarning("Player Pos for CameraTrackingDecider = " + playerPos.x);  }
        if (leftGap <= 0) { measurment = 1; }
        else if (rightGap >= 0) { measurment = 2; }
        switch(measurment)
        {
            case 1:
                if (debugConsoleLog == true)    {   Debug.Log("Case 1");    }
                camFollow.OnOffSwitchX(true);
                camFollow.camRecenter = false;
                goto case 3;
            case 2:
                if (debugConsoleLog == true)    {   Debug.Log("Case 2");    }
                camFollow.OnOffSwitchX(true);
                camFollow.camRecenter = false;
                goto case 4;
            case 3:
                if (debugConsoleLog == true)    {   Debug.Log("Case 3");    }
                if (playerPos.x > 0.5f) { camFollow.OnOffSwitchX(false); if (debugConsoleLog == true) { Debug.Log("Case 3, if = true"); }   }
                break;
            case 4:
                if (debugConsoleLog == true)    {   Debug.Log("Case 4");    }
                if (playerPos.x < 0.5f) { camFollow.OnOffSwitchX(false); if (debugConsoleLog == true) {  Debug.Log("Case 4, if = true"); }  }
                break;
            default:
                if (debugConsoleLog == true)    {   Debug.Log("Case Default");  }  
                camFollow.OnOffSwitchX(false);
                break;
        }
    }

    public void DiagonalCameraTrackingDecider()
    {
        if (debugConsoleLog == true)    {   Debug.LogWarning("Begining DiagonalCameraTrackingDecider()");   }
        int measurment = 0;
        Vector3 topSide = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, player.transform.position.z));
        Vector3 bottomSide = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0f, player.transform.position.z));
        if (debugConsoleLog == true)     {   Debug.LogWarning("topSide = " + topSide + " and bottomSide = " + bottomSide);   }
        float topGap = topSide.y - areas[currentArea].areaWalls[2].transform.position.y;
        float bottomGap = bottomSide.y - areas[currentArea].areaWalls[3].transform.position.y;
        if (debugConsoleLog == true)
        {
            Debug.LogWarning("topSide.y = " + topSide.y + ", topSide Wall.y is = " + areas[currentArea].areaWalls[2].transform.position.y + ", bottomSide.y = " + bottomSide.y + " and bottomSide Wall.y is " + areas[currentArea].areaWalls[3].transform.position.y);
            Debug.LogWarning("topGap = " + topGap + " and bottomGap = " + bottomGap);
        }
        Vector3 playerPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (debugConsoleLog == true)    {   Debug.LogWarning("Player Pos for CameraTrackingDecider = " + playerPos.y);  }
        if (topGap >= 0) { measurment = 1; }
        else if (bottomGap <= 0) { measurment = 2; }
        switch (measurment)
        {
            case 1:
                if (debugConsoleLog == true)    {   Debug.Log("Case 3");    }
                camFollow.OnOffSwitchY(true);
                camFollow.camRecenter = false;
                goto case 3;
            case 2:
                if (debugConsoleLog == true)    {   Debug.Log("Case 4");    }
                camFollow.OnOffSwitchY(true);
                camFollow.camRecenter = false;
                goto case 4;
            case 3:
                if (debugConsoleLog == true)    {   Debug.Log("Case 7");    }
                if (playerPos.y < 0.5f) { camFollow.OnOffSwitchY(false); if (debugConsoleLog == true)   {   Debug.Log("Case 7, if = true"); }   }
                break;
            case 4:
                    if (debugConsoleLog == true)    {   Debug.Log("Case 8");    }
                if (playerPos.y > 0.5f) { camFollow.OnOffSwitchY(false); if (debugConsoleLog == true)   {   Debug.Log("Case 8, if = true"); }   }
                break;
            default:
                if (debugConsoleLog == true)    {   Debug.Log("Case Default");  }
                camFollow.OnOffSwitchY(false);
                break;
        }
    }
}
