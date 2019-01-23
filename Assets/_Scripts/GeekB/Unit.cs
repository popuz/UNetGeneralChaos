using UnityEngine;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour
{
    [SerializeField] protected UnitMovement _unitMover;
    [SerializeField] protected UnitStats _myStats;

    [SerializeField] protected float _reviveDelay = 15f;

    protected bool _isDead;

    protected Vector3 _startPos;
    private float _reviveTime;

    protected virtual void Start()
    {
        _startPos = transform.position;
        _reviveTime = _reviveDelay;
    }

    private void Update() => OnUpdate();

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

        _unitMover.MoveToPoint(transform.position);
        RpcDie();
    }

    [ClientRpc]
    private void RpcDie()
    {
        if (!isServer) Die();
    }

    [ClientCallback]
    protected virtual void Revive()
    {
        _isDead = false;
        if (!isServer) return;

        _myStats.SetHealthRate(1);
        RpcRevive();
    }

    [ClientRpc]
    private void RpcRevive()
    {
        if (!isServer) Revive();
    }
}