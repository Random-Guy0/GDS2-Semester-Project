using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Melee Attack", fileName = "new Melee Attack")]
public class MeleeAttack : Attack
{
    [field: SerializeField] public Vector2 HitSize { get; private set; } = Vector2.one;
    [SerializeField] private DebugBox hitTestCubePrefab;
    
    public override IEnumerator DoAttack(float direction = 1f, float attackerWidth = 1f,
        Vector2? attackerPosition = null, AttackHandler attacker = null)
    {
        Vector2 position = attackerPosition ?? Vector2.zero;
        Vector2 origin = GetAttackOrigin(direction, attackerWidth, position);
        
        DebugBox hitTestCube = Instantiate(hitTestCubePrefab, origin, Quaternion.identity);
        hitTestCube.transform.localScale = new Vector3(HitSize.x, HitSize.y, 1f);
        hitTestCube.Duration = Duration;
        
        List<Health> allHits = new List<Health>();
        float currentTime = 0f;

        bool grabbedBubble = false;

        while (currentTime < Duration)
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, HitSize, 0f, Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.gameObject.TryGetComponent<Health>(out Health currentHealth))
                {
                    if (!allHits.Contains(currentHealth))
                    {
                        DoDamage(currentHealth, attacker);
                        allHits.Add(currentHealth);
                    }
                }

                if (attacker is PlayerAttackHandler playerAttackHandler && !grabbedBubble)
                {
                    if (hit.transform.TryGetComponent<BubbledEnemy>(out BubbledEnemy bubbledEnemy))
                    {
                        playerAttackHandler.GrabBubble(bubbledEnemy);
                        grabbedBubble = true;
                    }
                    else if (playerAttackHandler.CarryingBubble)
                    {
                        playerAttackHandler.ReleaseBubble();
                        grabbedBubble = true;
                    }
                }
            }
            
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    protected override Vector2 GetAttackOrigin(float direction, float attackerWidth, Vector2 attackerPosition)
    {
        Vector2 origin = base.GetAttackOrigin(direction, attackerWidth, attackerPosition);

        origin.x += direction * HitSize.x * 0.5f;
        
        return origin;
    }
}
