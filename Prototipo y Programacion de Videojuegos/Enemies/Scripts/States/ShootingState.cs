using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : EnemyBaseState
{
    public ShootingState(EnemyMain enemy, string animationName) : base(enemy, animationName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        //Ponerse en posicion de disparo
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
