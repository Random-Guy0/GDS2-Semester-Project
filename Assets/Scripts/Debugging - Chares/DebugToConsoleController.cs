using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugToConsoleController : MonoBehaviour
{
    public bool debuggingSwitch;
    public List<SectionEnemyManager> sectionEnemyManager;
    public List<AreaSections> areaSections;
    public List<AreaPortal> areaPortal;
    public SectionsManager sectionManager;
    public FollowPlayer followPlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (debuggingSwitch == true) { debuggingSwitch = true; }
        else { debuggingSwitch = false; }
        SectionEnemyBoolSelector();
        AreaSectionBoolSelector();
        AreaPortalBoolSelector();
        SectionManagerBoolSelector();
        FollowPlayerBoolSelector();
    }

    private void SectionEnemyBoolSelector()
    {
        for (int i = 0; i < sectionEnemyManager.Count; i++)
        {
            sectionEnemyManager[i].debugConsoleLog = debuggingSwitch;
        }
    }

    private void AreaSectionBoolSelector()
    {
        for (int i = 0; i < areaSections.Count; i++)
        {
            areaSections[i].debugConsoleLog = debuggingSwitch;
        }
    }

    private void AreaPortalBoolSelector()
    {
        for (int i = 0; i < areaPortal.Count; i++)
        {
            areaPortal[i].debugConsoleLog = debuggingSwitch;
        }
    }

    private void SectionManagerBoolSelector()
    {
        sectionManager.debugConsoleLog = debuggingSwitch;
    }

    private void FollowPlayerBoolSelector()
    {
        followPlayer.debugConsoleLog = debuggingSwitch;
    }
}
