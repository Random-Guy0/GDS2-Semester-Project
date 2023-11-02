using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionEnemyManager : MonoBehaviour
{
    public bool debugConsoleLog;
    public AreaSections areaManager;
    public List<GameObject> enemies;
    private float enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        if (debugConsoleLog == true) { debugConsoleLog = true; }
        else { debugConsoleLog = false; }
        enemyCount = enemies.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (debugConsoleLog == true) { Debug.Log("Enemy Count = " + enemyCount); }
        
        if (enemyCount <= 0)
        {
            if (debugConsoleLog == true) { Debug.Log(gameObject.name + " completed "); }
            
            areaManager.AreaSectionComplete();
            gameObject.SetActive(false);
        }
    }

    public void EnemyKilled(GameObject enemy)
    {
        if (debugConsoleLog == true) { Debug.LogWarning(enemy.name + " has been killed"); }

        enemyCount--;
    }
}
