using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacks/Melee Attacks/Player Melee Attack", fileName = "new Player Melee Attack")]
public class PlayerMeleeAttack : MeleeAttack
{
    public override IEnumerator DoAttack(Vector2 direction, Vector2 attackerSize,
        Vector2 attackerPosition, AttackHandler attacker = null)
    {
        if (!CanMoveVertically)
        {
            direction.y = 0f;
        }
        
        Vector2 origin = GetAttackOrigin(direction, attackerSize, attackerPosition);
        
#if UNITY_EDITOR
        Debug.DrawLine(new Vector3(origin.x + HitSize.x * 0.5f, origin.y + HitSize.y * 0.5f), new Vector3(origin.x - HitSize.x * 0.5f, origin.y + HitSize.y * 0.5f), Color.green, Duration - AttackStartDelay);
        Debug.DrawLine(new Vector3(origin.x + HitSize.x * 0.5f, origin.y + HitSize.y * 0.5f), new Vector3(origin.x + HitSize.x * 0.5f, origin.y - HitSize.y * 0.5f), Color.green, Duration - AttackStartDelay);
        Debug.DrawLine(new Vector3(origin.x + HitSize.x * 0.5f, origin.y - HitSize.y * 0.5f), new Vector3(origin.x - HitSize.x * 0.5f, origin.y - HitSize.y * 0.5f), Color.green, Duration - AttackStartDelay);
        Debug.DrawLine(new Vector3(origin.x - HitSize.x * 0.5f, origin.y + HitSize.y * 0.5f), new Vector3(origin.x - HitSize.x * 0.5f, origin.y - HitSize.y * 0.5f), Color.green, Duration - AttackStartDelay);  
#endif
        
        List<Health> allHits = new List<Health>();
        float currentTime = AttackStartDelay;

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
}
