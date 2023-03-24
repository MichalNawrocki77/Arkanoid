using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;
    protected GameManager gameManager;
    protected State(StateMachine stateMachine, GameManager gameManager)
    {
        this.stateMachine = stateMachine;
        this.gameManager = gameManager;
    }
    public virtual void Enter()
    {

    }
    public virtual void HandleInput()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void Exit()
    {

    }
}
