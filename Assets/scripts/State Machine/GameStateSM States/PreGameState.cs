using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreGameState : State
{
    public PreGameState(StateMachine stateMachine, GameManager gameManager) : base(stateMachine, gameManager)
    {
    }
    public override void Enter()
    {
        base.Enter();        
    }
    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.ballScripts[0].FirstBallPush();
            gameManager.gameStartText.gameObject.SetActive(false);
            //gameStarted = true;
            stateMachine.ChangeState(gameManager.gameOn);
        }
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        gameManager.ballGameObjects[0].transform.position = gameManager.platformGameObject.transform.position + gameManager.ballOffsetFromPlatform;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
