using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackHandler : MonoBehaviour
{
    [field: SerializeField] public MeleeAttack[] MeleeAttacks { get; private set; }
    [field: SerializeField] public RangedAttack[] RangedAttacks { get; private set; }

    private Collider2D coll;

    public bool CurrentlyAttacking { get; protected set; } = false;
    protected Attack CurrentAttack { get; set; }
    protected Coroutine attackCoroutine;

    public virtual void DoMeleeAttack(int index = 0)
    {
        DoAttack(MeleeAttacks[index]);
    }

    public virtual void DoRangedAttack(int index = 0)
    {
        DoAttack(RangedAttacks[index]);
    }

    private void DoAttack(Attack attack)
    {
        if (!CurrentlyAttacking && enabled)
        {
            CurrentAttack = attack;
            float width = GetColliderSize();
            float direction = GetDirection();
            attackCoroutine = StartCoroutine(attack.DoAttack(direction, width, transform.position, this));
            StartCoroutine(WaitForAttack(attack.Duration));
        }
    }

    protected virtual IEnumerator WaitForAttack(float attackDuration)
    {
        CurrentlyAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        CurrentlyAttacking = false;

    }

    protected virtual float GetColliderSize()
    {
        float width = 1f;        
        
        if ((coll == null && TryGetComponent<Collider2D>(out coll)) || coll != null)
        {
            width = coll.bounds.size.x;
        }

        return width;
    }

    protected abstract float GetDirection();

    public virtual void InterruptAttack()
    {
        if (attackCoroutine != null)
        {
            Debug.Log("Attack Interrupted");
            //StopCoroutine(attackCoroutine);
        }
        CurrentAttack = null;
    }
}
