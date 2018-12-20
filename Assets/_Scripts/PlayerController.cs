using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour, ICharacter
{
    public IPlayerInput InputScheme { get; set; }//private IPlayerInput _input;
    public IUnitMovement UnitMover { get; set; }//private IUnitMovement _unitMover;

    [SerializeField, Tooltip("слой взаимодействия перемещения")]
    private LayerMask _movementMask;
    public LayerMask MovementMask => _movementMask;
    public float MoveSpeed { get; set; } = 5f;

    [SerializeField, Tooltip("аниматор модели")]
    private Animator _targetAnimator;
    public UnitAnimation AnimCtrl { get; set; }

    [SerializeField]
    private bool _isPlayer;
    public bool IsPlayer => _isPlayer;

    public int Health { get; set; }
    
    void Awake()
    {
        var navAgentTmp = GetComponent<NavMeshAgent>();
            
        InputScheme = new ClickToMoveInputScheme();//ClickToMoveInputScheme();//WASDInputScheme();
        UnitMover = new NavMeshUnitMover(navAgentTmp, _movementMask, Camera.main);//new ClickToMoveUnitMover(navAgentTmp, _movementMask, Camera.main);//new WASDUnitMover(transform, MoveSpeed);
        
        if (navAgentTmp != null)
            AnimCtrl = new UnitAnimation(_targetAnimator ?? GetComponentInChildren<Animator>(), navAgentTmp);        
    }

    void Start()
    {        
        Camera.main.GetComponent<CameraController>().Target = transform;
        UnitMover?.Init(InputScheme);
    }
    
    void Update() => InputScheme?.ReadInput();    

    void FixedUpdate()
    {
        if(UnitMover.UseTick)
            UnitMover.Tick(Time.fixedDeltaTime);

        AnimCtrl?.Tick();
    }
}
