using System.Collections;

public class GruntAttackHandler : EnemyAttackHandler
{
    private GruntDetectPlayer movement;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<GruntDetectPlayer>();
    }

    protected override IEnumerator WaitForAttack(float attackDuration)
    {
        movement.CanMove = false;
        yield return base.WaitForAttack(attackDuration);
        movement.CanMove = true;
    }

    protected override bool CanAttack()
    {
        return base.CanAttack() && movement.CanMove;
    }
}