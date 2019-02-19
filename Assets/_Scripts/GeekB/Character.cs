using UnityEditor;
using UnityEngine;
using UNetGeneralChaos;

public class Character : Unit
{
    [SerializeField] private GameObject _gfx;

    public new PlayerStats Stats => stats as PlayerStats;
    public Player player;

    void Start()
    {
        _startPos = Vector3.zero;
        _reviveTime = _reviveDelay;
        if (stats.curHealth == 0)
        {
            transform.position = _startPos;
            if (isServer)
            {
                Stats.SetHealthByRate(1);                
                _unitMover.MoveToPoint(_startPos);
            }
        }
    }

    protected override void OnAliveUpdate()
    {
        base.OnAliveUpdate();
        if (_focus == null) return;

        if (!_focus.CanInteract)
        {
            RemoveFocus();
        }
        else
        {
            var distance = Vector3.Distance(_focus.interactionCenter.position, transform.position);
            if (distance <= _focus.radius && !_focus.Interact(gameObject)) RemoveFocus();
        }
    }

    protected override void Die()
    {
        base.Die();
        _gfx.SetActive(false); // hide graphics on die
    }

    protected override void Revive()
    {
        base.Revive();
        transform.position = _startPos;
        _gfx.SetActive(true); // show graphics
        if (isServer) _unitMover.MoveToPoint(_startPos);
    }

    public void SetMovePoint(Vector3 point)
    {
        if (!_isDead)
            _unitMover.MoveToPoint(point);
    }

    public void SetNewFocus(Interactable newFocus)
    {
        if (!_isDead && newFocus.CanInteract) SetFocus(newFocus);
    }
}