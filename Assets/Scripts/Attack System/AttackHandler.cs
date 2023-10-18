using System;
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
            Vector2 size = GetColliderSize();
            Vector2 direction = GetDirection();
            attackCoroutine = StartCoroutine(attack.DoAttack(direction, size, GetAttackOrigin(), this));
            StartCoroutine(WaitForAttack(attack.Duration));
        }
    }

    protected virtual IEnumerator WaitForAttack(float attackDuration)
    {
        CurrentlyAttacking = true;
        yield return new WaitForSeconds(attackDuration);
        CurrentlyAttacking = false;

    }

    protected virtual Vector2 GetColliderSize()
    {
        Vector2 size = Vector2.one;
        
        if ((coll == null && TryGetComponent<Collider2D>(out coll)) || coll != null)
        {
            size = coll.bounds.size;
        }

        return size;
    }

    public abstract Vector2 GetDirection();

    public virtual void InterruptAttack()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
        CurrentAttack = null;
    }

    protected virtual Vector2 GetAttackOrigin()
    {
        return transform.position;
    }
}
