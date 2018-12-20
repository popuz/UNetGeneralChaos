using UnityEngine;
using UnityEngine.AI;

namespace UNetGeneralChaos
{
    public class NavMeshUnitMover : IUnitMovement
    {
        private IPlayerInput _input;
        public bool UseTick => true;

        private LayerMask _movementMask;
        private Camera _cam;

        public NavMeshAgent Agent { get; set; }

        public NavMeshUnitMover(NavMeshAgent agent, LayerMask movementMask, Camera cam)
        {
            Agent = agent;
            _movementMask = movementMask;
            _cam = cam;
        }

        public void Init(IPlayerInput playerInput)
        {
            Debug.Log($"Using {nameof(NavMeshUnitMover)} for player Movement");
            _input = playerInput;
            //_input.FireOnce += NavMeshMoveToPint;        
        }

        public void Tick() => HandleMovement();
        public void Tick(float fixedDeltaTime) => HandleMovement();

        private void HandleMovement()
        {
            if (_input.IsFiring)
                NavMeshMoveToPint();
        }

        private void NavMeshMoveToPint()
        {
            if (Physics.Raycast(_cam.ScreenPointToRay(_input.CursorPosition), out var hit, 100f, _movementMask))
                Agent.SetDestination(hit.point);
        }
    }
}
