using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private SpriteRenderer _spriteRenderer;
    private SectionEnemyManager enemySectionManager;
    [SerializeField] private MonoBehaviour detectPlayerComponent;
    [SerializeField] private EnemyAttackHandler enemyAttackHandler;
    [SerializeField] private Rigidbody2D enemyRigidbody;
    [SerializeField] private Collider2D enemyCollider;
    [SerializeField] private BubbledEnemy bubblePrefab;
    [SerializeField] private float bubbleScale = 1f;

    protected override void Start(){
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        enemySectionManager = GetComponentInParent<SectionEnemyManager>();
        enemySectionManager.SetNewEnemy();
    }        

    //bubble function
    protected override void Die(){
        enemySectionManager.EnemyKilled();

        if (detectPlayerComponent is GruntDetectPlayer gruntDetectPlayer)
        {
            gruntDetectPlayer.StopMoving();
        }
        else if (detectPlayerComponent is RaptorDetectPlayer raptorDetectPlayer)
        {
            raptorDetectPlayer.StopMoving();
        }

        BubbledEnemy bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        transform.parent = bubble.transform;
        bubble.transform.localScale = Vector2.one * bubbleScale;
        bubble.PopDamage = Mathf.CeilToInt(maxHealth * 0.25f);
        bubble.enemySprite = _spriteRenderer;
        Vector2 directionToPlayer = transform.position - GameManager.Instance.Player.transform.position;
        directionToPlayer = directionToPlayer.normalized;
        bubble.Bump(directionToPlayer);
        
        Destroy(enemyCollider);
        Destroy(enemyRigidbody);
        Destroy(enemyAttackHandler);
        Destroy(detectPlayerComponent);
        Destroy(this);
    }
    

}
