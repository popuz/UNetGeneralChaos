using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveUnitMover : IUnitMovement
{
    private IPlayerInput _input;

    private NavMeshAgent _agent;    
    private LayerMask _movementMask;
    private Camera _cam;

    public ClickToMoveUnitMover(NavMeshAgent agent, LayerMask movementMask, Camera cam)
    {       
        _agent = agent;
        _movementMask = movementMask;
        _cam = cam;
    }
    public void Init(IPlayerInput playerInput)
    {
        Debug.Log($"Using {nameof(ClickToMoveUnitMover)}");
        _input = playerInput;
        _input.FireOnce += NavMeshMoveToPint;        
    }

    void NavMeshMoveToPint()
    {
        Ray ray = _cam.ScreenPointToRay(_input.CursorPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, _movementMask))
            _agent.SetDestination(hit.point);
    }

    public void Tick() { }
}
