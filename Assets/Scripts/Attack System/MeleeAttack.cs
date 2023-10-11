using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Melee Attacks/Melee Attack", fileName = "new Melee Attack")]
public class MeleeAttack : Attack
{
    [field: SerializeField] public Vector2 HitSize { get; private set; } = Vector2.one;
    //[SerializeField] private DebugBox hitTestCubePrefab;
    
    public override IEnumerator DoAttack(Vector2 direction, Vector2 attackerSize,
        Vector2 attackerPosition, AttackHandler attacker = null)
    {
        Vector2 origin = GetAttackOrigin(direction, attackerSize, attackerPosition);
        
        /*DebugBox hitTestCube = Instantiate(hitTestCubePrefab, origin, Quaternion.identity);
        hitTestCube.transform.localScale = new Vector3(HitSize.x, HitSize.y, 1f);
        hitTestCube.Duration = Duration;*/
        
        List<Health> allHits = new List<Health>();
        float currentTime = 0f;

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
            }
            
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    protected override Vector2 GetAttackOrigin(Vector2 direction, Vector2 attackerSize, Vector2 attackerPosition)
    {
        Vector2 origin = base.GetAttackOrigin(direction, attackerSize, attackerPosition);

        origin.x += direction.x * HitSize.x * 0.5f;
        origin.y += direction.y * HitSize.y * 0.5f;
        
        return origin;
    }
}
