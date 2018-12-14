using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    private IPlayerInput _input = new ClickToMoveInputScheme();
    private IUnitMovement _unitMover = new ClickToMoveUnitMover();

    [SerializeField, Tooltip("слой взаимодействия перемещения")]
    private LayerMask _movementMask;
    public LayerMask MovementMask => _movementMask;

    private UnitAnimation _animCtrl = new UnitAnimation();

    void Awake()
    {
        _unitMover.Init(_input, this);
        _animCtrl.Init(GetComponentInChildren<Animator>(), GetComponent<NavMeshAgent>());
    }

    private void Update()
    {
        _input.ReadInput();
    }

    private void FixedUpdate()
    {
        _animCtrl.Tick();
    }
}
