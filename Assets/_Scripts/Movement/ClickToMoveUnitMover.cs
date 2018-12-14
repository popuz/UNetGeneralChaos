using UnityEngine;
using UnityEngine.AI;

public class ClickToMoveUnitMover : IUnitMovement
{
    private IPlayerInput _input;

    private NavMeshAgent _agent;
    private Camera _cam;
    private LayerMask _movementMask;

    public void Init(IPlayerInput playerInput, PlayerController playerCtrl)
    {
        _input = playerInput;

        _agent = playerCtrl.GetComponent<NavMeshAgent>();
        _movementMask = playerCtrl.MovementMask;
        _cam = Camera.main;

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
