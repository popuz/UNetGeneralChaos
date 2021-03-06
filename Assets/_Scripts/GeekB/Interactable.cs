﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class Interactable : NetworkBehaviour
{
    public Transform interactionCenter;
    public float radius = 2f;

    private void OnValidate() => interactionCenter = interactionCenter == null ? this.transform : interactionCenter;
    
    public bool CanInteract { get; protected set; } = true;

    public virtual bool Interact(GameObject user) => false;                

    protected virtual void OnDrawGizmosSelected () 
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionCenter.position, radius);
    }
}