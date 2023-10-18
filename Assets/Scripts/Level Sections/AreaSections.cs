using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSections : MonoBehaviour
{

    public List<GameObject> section = new List<GameObject>();
    public List<GameObject> areaWalls = new List<GameObject>();
    public List<SectionEnemyManager> areaEnemyManagers = new List<SectionEnemyManager>();
    public List<bool> sectionsCompleted = new List<bool>();
    public SectionsManager sectionManager;
    public int sectionCount = 0;
    public bool beginningAreaSection = true;
    private bool areaSectionDisabled = false;
    void Start()
    {
        for (int i = 0; i < section.Count - 1; i++)
        {
            sectionsCompleted.Add(false);
        }
    }
    void Update()
    {
        if (sectionCount == sectionsCompleted.Count && areaSectionDisabled == false) 
        {
            Debug.Log("Area Section Ended");
            sectionManager.NewArea();
            areaSectionDisabled = true;
            sectionManager.endOfArea = true;
        }
    }

    public void AreaSectionComplete()
    {
        section[sectionCount].SetActive(false);

        if (sectionCount == 0)
        {
            beginningAreaSection = false;
        }
        ++sectionCount;
    }
}
