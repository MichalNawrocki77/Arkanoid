using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOnState : State
{    public GameOnState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine, gameManager)
    {
    }
    //public override void Enter()
    //{
    //    base.Enter();
    //}
    //public override void HandleInput()
    //{
    //    base.HandleInput();
    //}
    //public override void LogicUpdate()
    //{
    //    base.LogicUpdate();
    //}
    //public override void PhysicsUpdate()
    //{
    //    base.PhysicsUpdate();
    //}
    public override void Exit()
    {
        base.Exit();
        gameManager.GameReset();
    }
}
