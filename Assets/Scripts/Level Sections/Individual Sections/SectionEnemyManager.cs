using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionEnemyManager : MonoBehaviour
{

    public AreaSections sectionManager;
    private float enemyCount;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount <= 0)
        {
            sectionManager.AreaSectionComplete();
            gameObject.SetActive(false);
        }
    }

    public void SetNewEnemy()
    {
        ++enemyCount;
    }

    public void EnemyKilled()
    {
        enemyCount--;
    }
}
