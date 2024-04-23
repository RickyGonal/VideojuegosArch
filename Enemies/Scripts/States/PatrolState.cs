using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : EnemyBaseState
{
    public PatrolState(EnemyMain enemy, string animationName) : base(enemy, animationName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (enemy.CheckforPlayer())
            enemy.SwitchState(enemy.playerDetectedState);
        if (enemy.CheckForObstacles())
            Rotate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.rb.velocity = new Vector2(enemy.stats.speed * enemy.facingDirection, enemy.rb.velocity.y);
    }
    void Rotate()
    {
        enemy.transform.Rotate(0, 180, 0);
        enemy.facingDirection = -enemy.facingDirection;
    }
}
