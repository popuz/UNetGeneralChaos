using UnityEngine;
using UnityEngine.Networking;

public class Unit : Interactable
{
    public UnitStats stats => _stats;

    [SerializeField] protected UnitMovement _unitMover;
    [SerializeField] protected UnitStats _stats;
    [SerializeField] protected float _reviveDelay = 15f;

    protected Interactable _focus;

    protected bool _isDead;

    protected Vector3 _startPos;
    private float _reviveTime;

    public delegate void UnitDenegate();

    public event UnitDenegate EventOnDamage;
    public event UnitDenegate EventOnDie;
    public event UnitDenegate EventOnRevive;


    public override void OnStartServer()
    {
        if (_unitMover == null) return;
        _unitMover.SetMoveSpeed(_stats.moveSpeed.GetValue());
        _stats.moveSpeed.onStatChanged += _unitMover.SetMoveSpeed;
    }

    protected virtual void Start()
    {
        _startPos = transform.position;
        _reviveTime = _reviveDelay;
    }

    private void Update() => OnUpdate();

    public override bool Interact(GameObject user)
    {
        Debug.Log(user.name + " interacts with" + name);
        var combat = user.GetComponent<GbCombat>();

        if (combat != null)
        {
            if (combat.Attack(_stats)) EventOnDamage?.Invoke();
            return true;
        }

        return base.Interact(user);
    }

    private void OnUpdate()
    {
        if (!isServer) return;

        if (!_isDead)
        {
            if (_stats != null && _stats.CurHealth == 0) Die();
            else OnAliveUpdate();
        }
        else
        {
            OnDeadUpdate();
        }
    }

    protected virtual void OnAliveUpdate()
    {
    }

    private void OnDeadUpdate()
    {
        if (_reviveTime > 0)
        {
            _reviveTime -= Time.deltaTime;
        }
        else
        {
            _reviveTime = _reviveDelay;
            Revive();
        }
    }

    [ClientCallback]
    protected virtual void Die()
    {
        _isDead = true;
        GetComponent<Collider>().enabled = false;
        EventOnDie?.Invoke();

        if (isServer)
        {
            CanInteract = false;
            RemoveFocus();
            _unitMover.MoveToPoint(transform.position);
            RpcDie();
        }
    }

    [ClientCallback]
    protected virtual void Revive()
    {
        _isDead = false;                
        GetComponent<Collider>().enabled = true;
        EventOnRevive?.Invoke();
        if (isServer)
        {
            CanInteract = true;
            _stats.SetHealthRate(1);
            RpcRevive();
        }
    }

    [ClientRpc]
    private void RpcRevive()
    {
        if (!isServer) Revive();
    }

    [ClientRpc]
    private void RpcDie()
    {
        if (!isServer) Die();
    }

    protected virtual void SetFocus(Interactable newFocus)
    {
        if (newFocus == _focus) return;

        _focus = newFocus;
        _unitMover.FollowTarget(newFocus);
    }

    protected virtual void RemoveFocus()
    {
        _focus = null;
        _unitMover.StopFollowingTarget();
    }
}