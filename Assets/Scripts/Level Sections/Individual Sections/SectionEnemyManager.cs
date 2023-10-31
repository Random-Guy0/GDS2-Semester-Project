using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionEnemyManager : MonoBehaviour
{

    public AreaSections areaManager;
    public List<GameObject> enemies;
    private float enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        enemyCount = enemies.Count;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemy Count = " + enemyCount);
        if (enemyCount <= 0)
        {
            Debug.Log(gameObject.name + " completed ");
            areaManager.AreaSectionComplete();
            gameObject.SetActive(false);
        }
    }

    public void EnemyKilled(GameObject enemy)
    {
        Debug.LogWarning(enemy.name + " has been killed");
        enemyCount--;
    }
}
