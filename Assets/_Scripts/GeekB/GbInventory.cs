using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GbInventory : NetworkBehaviour
{
    public Transform dropPoint;
    public int space = 20;
    public event SyncList<GbItem>.SyncListChanged onItemChanged;
    
    public SyncListItem items = new SyncListItem();
    
    public override void OnStartLocalPlayer() => items.Callback += ItemChanged;

    private void ItemChanged(SyncList<GbItem>.Operation op, int itemIndex) => onItemChanged?.Invoke(op, itemIndex);

    public bool Add(GbItem item)
    {
        if (items.Count < space)
        {
            items.Add(item);
            return true;
        }
        else return false;
    }

    public void Remove(GbItem item)
    {
        CmdRemoveItem(items.IndexOf(item));
    }

    [Command]
    private void CmdRemoveItem(int index)
    {
        if (items[index] != null)
        {
            Drop(items[index]);
            items.RemoveAt(index);
        }
    }
        
    private void Drop(GbItem item)
    {
        GbItemPickup pickupItem = Instantiate(item.pickupPrefab, dropPoint.position,
            Quaternion.Euler(0, Random.Range(0, 360f), 0));
        pickupItem.item = item;
        NetworkServer.Spawn(pickupItem.gameObject);
    }
}