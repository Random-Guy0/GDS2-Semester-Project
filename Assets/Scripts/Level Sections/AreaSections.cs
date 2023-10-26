using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaSections : MonoBehaviour
{
    public AreaPortal nextAreaPortal;
    public List<GameObject> section = new List<GameObject>();
    public List<GameObject> areaWalls = new List<GameObject>();
    public List<SectionEnemyManager> areaEnemyManagers = new List<SectionEnemyManager>();
    public List<bool> sectionsCompleted = new List<bool>();
    public SectionsManager sectionManager;
    public int sectionCount = 0;
    private bool areaSectionDisabled;
    void Start()
    {
        Debug.Log("Section List Count = " + section.Count + gameObject.name);
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
                if (sectionManager.currentArea == 4)
                {
                    Debug.Log("Final Area Finished");
                    nextAreaPortal.ableToEnter = true;
                    areaSectionDisabled = true;

                }
                else
                {

                    Debug.Log("Area Section Ended");
                    nextAreaPortal.ableToEnter = true;
                    sectionManager.NewArea();  
                areaSectionDisabled = true;
                    Debug.LogWarning("sectionCount before = " + sectionCount);
                    --sectionCount;
                    Debug.LogWarning("sectionCount after = " + sectionCount);   
                }

            }
            else if (sectionManager.currentArea == 2)
        {
            Debug.Log("Area Section Ended");
            nextAreaPortal.ableToEnter = true;
            sectionManager.NewArea();
            areaSectionDisabled = true;
            Debug.LogWarning("sectionCount before = " + sectionCount);
            --sectionCount;
            Debug.LogWarning("sectionCount after = " + sectionCount);
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
