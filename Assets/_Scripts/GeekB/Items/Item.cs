﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public new string name = "New Item";
    public Sprite icon = null;
    public ItemPickup pickupPrefab;

    public virtual void Use(Player player) => Debug.Log("Using " + name);
}