using UnityEngine;
using UnityEngine.Networking;

public class Unit : GbInteractable
{
    [SerializeField] protected UnitMovement _unitMover;
    [SerializeField] protected UnitStats _myStats;

    [SerializeField] protected float _reviveDelay = 15f;

    protected GbInteractable _focus;

    protected bool _isDead;

    protected Vector3 _startPos;
    private float _reviveTime;

    public delegate void UnitDenegate();
    [SyncEvent] public event UnitDenegate EventOnDamage;
    [SyncEvent] public event UnitDenegate EventOnDie;
    [SyncEvent] public event UnitDenegate EventOnRevive;

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
            if (combat.Attack(_myStats)) EventOnDamage?.Invoke();            
            return true;
        }                        
        return base.Interact(user);
    }

    private void OnUpdate()
    {
        if (!isServer) return;

        if (!_isDead)
        {
            if (_myStats.CurHealth == 0) Die();
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
        if (!isServer) return;
               
        CanInteract = false;
        RemoveFocus();
        _unitMover.MoveToPoint(transform.position);
        EventOnDie?.Invoke();
        RpcDie();
    }

    [ClientCallback]
    protected virtual void Revive()
    {
        _isDead = false;
        if (!isServer) return;
        
        CanInteract = true;
        _myStats.SetHealthRate(1);
        EventOnRevive?.Invoke();
        RpcRevive();
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

    protected virtual void SetFocus(GbInteractable newFocus)
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