using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaSections : MonoBehaviour
{
    [SerializeField] public bool debugConsoleLog;
    public AreaPortal nextAreaPortal;
    public List<GameObject> section = new List<GameObject>();
    public List<GameObject> areaWalls = new List<GameObject>();
    public List<SectionEnemyManager> areaEnemyManagers = new List<SectionEnemyManager>();
    public List<bool> sectionsCompleted = new List<bool>();
    public SectionsManager sectionManager;
    public int sectionCount = 0;
    private bool areaSectionDisabled;
    public bool diagonalArea = false;
    void Start()
    {
        if (debugConsoleLog == true) { debugConsoleLog = true; }
        else { debugConsoleLog = false; }

        if (debugConsoleLog == true) { Debug.Log("Section List Count = " + section.Count + gameObject.name); }

        for (int i = 0; i <= section.Count; i++)
        {
            sectionsCompleted.Add(false);
        }
        areaSectionDisabled = false;
        section.Add(areaWalls[1]);
    }
    void Update()
    {
        if (sectionCount == sectionsCompleted.Count && areaSectionDisabled == false)
        {
                if (debugConsoleLog == true) { Debug.Log("Area Section Ended"); }

                nextAreaPortal.ableToEnter = true;
                sectionManager.NewArea();
                areaSectionDisabled = true;
                if (debugConsoleLog == true) { Debug.LogWarning("sectionCount before = " + sectionCount); }

                --sectionCount;
                if (debugConsoleLog == true) { Debug.LogWarning("sectionCount after = " + sectionCount); }

        }
        else if (diagonalArea == true && sectionsCompleted[0] == true)
        {
            if (areaSectionDisabled == false)
            {
                if (debugConsoleLog == true) { Debug.Log("Area Section Ended - Section 3"); }

                nextAreaPortal.ableToEnter = true;
                sectionManager.NewArea();
                areaSectionDisabled = true;
                if (debugConsoleLog == true) { Debug.LogWarning("sectionCount before = " + sectionCount); }

                //--sectionCount;
                if (debugConsoleLog == true) { Debug.LogWarning("sectionCount after = " + sectionCount); }

            }
        }
        else
        {
            if (debugConsoleLog == true) { Debug.Log("Final Area Finished"); }

            nextAreaPortal.ableToEnter = true;
            areaSectionDisabled = true;
        }

    }

    public bool FinalArea()
    {
        if (sectionCount == section.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AreaSectionComplete()
    {
        sectionManager.NextSection(nextAreaPortal);
    }
}
