﻿using UnityEditor;
using UnityEngine;
using UNetGeneralChaos;

public class Character : Unit
{
    [SerializeField] private GameObject _gfx;
    
    public GbInventory Inventory;    

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
      
    public void SetInventory (GbInventory inventory)
    {                
        Inventory = inventory;
        inventory.dropPoint = transform;
    }
    
    public void SetNewFocus (GbInteractable newFocus) 
    {
        if (!_isDead && newFocus.CanInteract) SetFocus(newFocus);        
    }
}