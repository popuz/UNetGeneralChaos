﻿using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveUnitMover : IUnitMovement
{
    private IPlayerInput _input;
    public bool UseTick => false;
    
    private LayerMask _movementMask;
    private Camera _cam;

    public NavMeshAgent Agent { get; set; }

    public ClickToMoveUnitMover(NavMeshAgent agent, LayerMask movementMask, Camera cam)
    {
        Agent = agent;
        _movementMask = movementMask;
        _cam = cam;
    }

    public void Init(IPlayerInput playerInput)
    {
        Debug.Log($"Using {nameof(ClickToMoveUnitMover)} for player Movement");
        _input = playerInput;
        _input.FireOnce += NavMeshMoveToPint;        
    }

    void NavMeshMoveToPint()
    {
        Ray ray = _cam.ScreenPointToRay(_input.CursorPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, _movementMask))
            Agent.SetDestination(hit.point);
    }

    public void Tick() { }
    public void Tick(float fixedDeltaTime) { }
}
