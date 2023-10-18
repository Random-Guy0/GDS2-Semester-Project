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
    [SerializeField] private float deathScale = 1f;

    [SerializeField] private AmmoPickup pickup;
    [SerializeReference] private int ammoDropAmount = 1;

    private Animator animator;

    protected override void Start(){
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        enemySectionManager = GetComponentInParent<SectionEnemyManager>();
        animator = GetComponent<Animator>();
        OnTakeDamage += enemyAttackHandler.InterruptAttack;
        OnTakeDamage += TakeDamage;
    }

    public override void TakeDamage(int amount, Attack attack)
    {
        IEnemyMovement enemyMovement = (IEnemyMovement)detectPlayerComponent;
        enemyMovement.Stun();
        if (attack is MeleeAttack && CurrentHealth - amount <= 0 && ammoDropAmount > 0)
        {
            AmmoPickup newPickup = Instantiate(pickup, transform.position, Quaternion.identity);
            newPickup.AmmoAmount = ammoDropAmount;
        }
        
        base.TakeDamage(amount, attack);
    }

    //bubble function
    protected override void Die(){
        enemySectionManager.EnemyKilled();
        enemyAttackHandler.InterruptAttack();
        StopAllCoroutines();
        _spriteRenderer.color = Color.white;

        if (detectPlayerComponent is GruntDetectPlayer gruntDetectPlayer)
        {
            gruntDetectPlayer.StopMoving();
        }
        else if (detectPlayerComponent is RaptorDetectPlayer raptorDetectPlayer)
        {
            raptorDetectPlayer.StopMoving();
        }
        
        CreateBubble();

        if (animator != null)
        {
            animator.enabled = false;
        }

        Destroy(enemyCollider);
        Destroy(enemyRigidbody);
        Destroy(enemyAttackHandler);
        Destroy(detectPlayerComponent);
        Destroy(GetComponent<EnemyMeleeAttack>());
        Destroy(this);
    }

    private void CreateBubble()
    {
        BubbledEnemy bubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        transform.parent = bubble.transform;
        bubble.transform.localScale = (Vector3)(Mathf.Max(transform.localScale.x, transform.localScale.y) * bubbleScale * Vector2.one) * deathScale + Vector3.forward;
        transform.localScale = (Vector3)(Vector2.one / bubbleScale) + Vector3.forward;
        bubble.PopDamage = Mathf.CeilToInt(maxHealth * 0.25f);
        bubble.enemySprite = _spriteRenderer;
        Vector2 directionToPlayer = transform.position - GameManager.Instance.Player.transform.position;
        directionToPlayer = directionToPlayer.normalized;
        bubble.Bump(directionToPlayer);
    }
    
    private void TakeDamage()
    {
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }
}
