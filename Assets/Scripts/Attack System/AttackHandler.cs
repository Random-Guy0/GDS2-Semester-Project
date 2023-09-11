using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackHandler : MonoBehaviour
{
    [SerializeField] private MeleeAttack[] availableMeleeAttacks;
    [SerializeField] private RangedAttack[] availableRangedAttacks;

    private Collider2D coll;

    public bool CurrentlyAttacking { get; private set; } = false;

    public virtual void DoMeleeAttack(int index = 0)
    {
        DoAttack(availableMeleeAttacks[index]);
    }

    public virtual void DoRangedAttack(int index = 0)
    {
        DoAttack(availableRangedAttacks[index]);
    }

    protected void DoAttack(Attack attack)
    {
        if (!CurrentlyAttacking)
        {
            float width = GetColliderSize();
            float direction = GetDirection();
            StartCoroutine(attack.DoAttack(direction, width, transform.position));
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
}
