using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionEnemyManager : MonoBehaviour
{

    public AreaSections sectionManager;
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
        if (enemyCount <= 0)
        {
            Debug.Log(gameObject.name + " completed ");
            sectionManager.AreaSectionComplete();
            gameObject.SetActive(false);
        }
    }

    public void EnemyKilled()
    {
        enemyCount--;
    }
}
