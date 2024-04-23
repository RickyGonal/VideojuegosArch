using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetected : EnemyBaseState
{
    public PlayerDetected(EnemyMain enemy, string animationName) : base(enemy, animationName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        if (!enemy.CheckForObstacles())
        {
            enemy.rb.velocity = Vector2.zero;
            enemy.alert.SetActive(true);
        }
    }
    public override void Exit()
    {
        base.Exit();
        enemy.alert.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!enemy.CheckforPlayer())
            enemy.SwitchState(enemy.patrolState);
        else
        {
            if(Time.time >= enemy.stateTime + enemy.stats.playerDetectedWaitTime)
            {
                enemy.SwitchState(enemy.chargeState);
                //enemy.SwitchState(enemy.shootingState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
