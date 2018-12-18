using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, ICharacter
{
    //private IPlayerInput _input;
    //private IUnitMovement _unitMover;
    public IPlayerInput InputScheme { get; set; }
    public IUnitMovement UnitMover { get; set; }

    [SerializeField, Tooltip("слой взаимодействия перемещения")]
    private LayerMask _movementMask;
    public LayerMask MovementMask => _movementMask;

    public UnitAnimation AnimCtrl { get; set; }

    #region For Unit-tests
    [SerializeField]
    private bool _isPlayer;
    public bool IsPlayer => _isPlayer;

    public int Health { get; set; }
    public float Speed { get; set; }
    #endregion

    void Awake()
    {
        var navAgentTmp = GetComponent<NavMeshAgent>();

        InputScheme = new WASDInputScheme();//ClickToMoveInputScheme();//WASDInputScheme();
        UnitMover = new WASDUnitMover(transform, Speed);//new ClickToMoveUnitMover(navAgentTmp, _movementMask, Camera.main);//WASDUnitMover(transform, 5f);

        if (navAgentTmp != null)
            AnimCtrl = new UnitAnimation(GetComponentInChildren<Animator>(), navAgentTmp);        
    }

    void Start()
    {
        UnitMover?.Init(InputScheme);
    }

    private void Update()
    {
        InputScheme?.ReadInput();
    }

    private void FixedUpdate()
    {
        UnitMover?.Tick();
        AnimCtrl?.Tick();
    }
}
