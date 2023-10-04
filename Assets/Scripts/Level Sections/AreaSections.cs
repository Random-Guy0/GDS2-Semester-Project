using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSections : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> section = new List<GameObject>();
    public List<GameObject> areaWalls = new List<GameObject>();
    public List<SectionEnemyManager> areaEnemyManagers = new List<SectionEnemyManager>();
    public List<bool> sectionsCompleted = new List<bool>();
    public SectionsManager sectionManager;
    public int sectionComplelted = 0;
    public bool beginningAreaSection = true;
    private bool areaSectionDisabled = false;
    void Start()
    {
        for (int i = 0; i < section.Count - 1; i++)
        {
            sectionsCompleted.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sectionComplelted >= sectionsCompleted.Count && areaSectionDisabled == false) 
        {
            Debug.Log("AreaSection completed");
            sectionManager.NewArea();
            ++sectionManager.currentArea;
            //gameObject.SetActive(false);
            areaSectionDisabled = true;
        }
    }

    public void AreaSectionComplete()
    {
        //sectionsCompleted[sectionComplelted] = true;
        Debug.Log("Area Complete");
        section[sectionComplelted].SetActive(false);

        if (sectionComplelted == 0)
        {
            beginningAreaSection = false;
        }
        ++sectionComplelted;
    }
}
