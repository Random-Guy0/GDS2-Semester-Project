using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionEnemyManager : MonoBehaviour
{

    private List<GameObject> enemy;
    private SectionsManager sectionManager;
    private float enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        enemy = new List<GameObject>();
        sectionManager = GameObject.FindGameObjectWithTag("LevelSectionManager").GetComponent<SectionsManager>();
        enemyCount = enemy.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            Debug.Log("All Enemies Defeated");
            sectionManager.SectionComplete();
        }
    }

    public void SetNewEnemy(GameObject newEnemy)
    {
        enemy.Add(newEnemy);
        Debug.Log("Enemy Added to Section Manager of " + gameObject.name + " Total Enemies now: " + enemyCount);
    }

    public void EnemyKilled()
    {
        Debug.Log("EnemyKilled");
        enemyCount--;
        Debug.Log("Enemy Count for Section " + gameObject.name + "is now: " + enemyCount);
    }
}
