using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private SpriteRenderer _spriteRenderer;
    private SectionEnemyManager enemySectionManager;
    [SerializeField] private MonoBehaviour detectPlayerComponent;

    protected override void Start(){
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        enemySectionManager = GetComponentInParent<SectionEnemyManager>();
        enemySectionManager.SetNewEnemy(gameObject);
    }        

    //bubble function
    protected override void Die(){
        _spriteRenderer.color = Color.blue;
        enemySectionManager.EnemyKilled();
        if (TryGetComponent<BubbledEnemy>(out BubbledEnemy bubbledEnemy))
        {
            bubbledEnemy.enabled = true;
        }
        Destroy(detectPlayerComponent);
        Destroy(this);
    }
    

}
