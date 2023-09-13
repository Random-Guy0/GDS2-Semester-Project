using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private SpriteRenderer _spriteRenderer;
    private SectionEnemyManager enemySectionManager;
    [SerializeField] private MonoBehaviour detectPlayerComponent;
    [SerializeField] private EnemyAttackHandler enemyAttackHandler;

    protected override void Start(){
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        enemySectionManager = GetComponentInParent<SectionEnemyManager>();
        enemySectionManager.SetNewEnemy();
    }        

    //bubble function
    protected override void Die(){
        _spriteRenderer.color = Color.blue;
        enemySectionManager.EnemyKilled();
        if (TryGetComponent<BubbledEnemy>(out BubbledEnemy bubbledEnemy))
        {
            bubbledEnemy.enabled = true;
        }

        if (detectPlayerComponent is GruntDetectPlayer gruntDetectPlayer)
        {
            gruntDetectPlayer.StopMoving();
        }
        else if (detectPlayerComponent is RaptorDetectPlayer raptorDetectPlayer)
        {
            raptorDetectPlayer.StopMoving();
        }
        
        Destroy(enemyAttackHandler);
        Destroy(detectPlayerComponent);
        Destroy(this);
    }
    

}
