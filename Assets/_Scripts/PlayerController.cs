using UnityEngine;
using UnityEngine.AI;

namespace UNetGeneralChaos
{
    /// <summary>
    /// Main wraper and coupler of logic related to the player character gameObject.
    /// </summary>
    [DisallowMultipleComponent]
    public class PlayerController : MonoBehaviour, ICharacter
    {                
        private IPlayerInput _inputScheme;        
        /// <summary>
        /// Logic that makes unit movement depending on the initialized InputScheme
        /// </summary>
        public IUnitMovement UnitMover { get; private set; }

        [SerializeField, Tooltip("Layer on which player can move via NavAgent")]
        private LayerMask _movementMask;

        /// <summary>
        /// Speed of movement passed in UnitMover on initialization. Works only with WASDInputScheme
        /// </summary>
        public float MoveSpeed { get; set; } = 5f;

        [SerializeField, Tooltip("Target animator which should be coupled with player logic")]
        private Animator _targetAnimator;
        private UnitAnimation _animCtrl;

        [SerializeField, Tooltip("Different visual representation of the player")]
        private Mesh[] _avatarMeshes;
        [SerializeField, Tooltip("Renderer responsibel for visual representation of the character (SkeletalMesh)")]
        private Renderer _characterRenderer;               

        [SerializeField]
        private bool _isPlayer;
        /// <summary>
        /// Whether it's a Player or a Bot/NPC
        /// </summary>
        public bool IsPlayer => _isPlayer;

        /// <summary>
        /// Current Health of the character
        /// </summary>
        public int Health { get; set; }

        void Awake()
        {
            var navAgentTmp = GetComponent<NavMeshAgent>();

            _inputScheme = new ClickToMoveInputScheme();//ClickToMoveInputScheme();//WASDInputScheme();
            UnitMover = new NavMeshUnitMover(navAgentTmp, _movementMask, Camera.main);//new ClickToMoveUnitMover(navAgentTmp, _movementMask, Camera.main);//new WASDUnitMover(transform, MoveSpeed);

            if (navAgentTmp != null)
                _animCtrl = new UnitAnimation(_targetAnimator ?? GetComponentInChildren<Animator>(), navAgentTmp);
        }

        void Start()
        {
            Camera.main.GetComponent<CameraController>().Target = transform;
            UnitMover?.Init(_inputScheme);
        }

        void Update() => _inputScheme?.ReadInput();

        void FixedUpdate()
        {
            if (UnitMover.UseTick)
                UnitMover.Tick(Time.fixedDeltaTime);

            _animCtrl?.Tick();
        }
    }
}
