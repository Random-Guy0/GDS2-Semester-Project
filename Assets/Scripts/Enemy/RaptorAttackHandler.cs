using UnityEngine;

public class RaptorAttackHandler : EnemyAttackHandler
{
    [SerializeField] private float playerAttackDistance = 2f;
    
    public override Vector2 GetDirection()
    {
        Vector2 direction = (GameManager.Instance.Player.transform.position - transform.position).normalized;
        Debug.Log(direction);
        return direction;
    }

    protected override bool CanAttack()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) <= playerAttackDistance && !CurrentlyAttacking;
    }
}