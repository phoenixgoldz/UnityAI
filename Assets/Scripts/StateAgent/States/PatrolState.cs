using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolState : State
{
    private float timer;
    public PatrolState(StateAgent owner) : base(owner)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("Patrol Update");
        owner.movement.Resume();
        owner.navigation.targetNode = owner.navigation.GetNearestNode();
        timer = Random.Range(5,10);
    }

    public override void OnExit()
    {
        Debug.Log("Patrol Update");
    }

    public override void OnUpdate()
    {
        if (owner.perceived.Length > 0)
        {
            owner.stateMachine.StartState(nameof(ChaseState));
        }
        Debug.Log("Patrol Update");

        timer -= Time.deltaTime;
        if(timer >= 0) 
            {
            owner.stateMachine.StartState(nameof(WanderState));
            }
    }
}

