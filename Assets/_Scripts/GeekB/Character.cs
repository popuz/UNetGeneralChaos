using UnityEditor;
using UnityEngine;
using UNetGeneralChaos;

public class Character : Unit
{
    [SerializeField] private GameObject _gfx;

    protected override void OnAliveUpdate()
    {
        base.OnAliveUpdate();
        if (_focus != null)
        {
            if (!_focus.CanInteract)            
                RemoveFocus();            
            else if ( Vector3.Distance(_focus.interactionCenter.position, transform.position) <= _focus.radius) 
                _focus.Interact(gameObject);                            
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
    
    public void SetNewFocus (GbInteractable newFocus) 
    {
        if (!_isDead && newFocus.CanInteract) SetFocus(newFocus);        
    }
}