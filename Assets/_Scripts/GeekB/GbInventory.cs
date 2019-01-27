using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GbInventory : NetworkBehaviour
{
    public int space = 20;
    public SyncListItem items = new SyncListItem();
    public event SyncList<GbItem>.SyncListChanged onItemChanged;

    public Transform dropPoint;
    
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
        Drop(item);
        CmdRemoveItem(items.IndexOf(item));
    }
    
    private void Drop(GbItem item)
    {
        GbItemPickup pickupItem = Instantiate(item.pickupPrefab, dropPoint.position,
            Quaternion.Euler(0, Random.Range(0, 360f), 0));
        pickupItem.item = item;
        NetworkServer.Spawn(pickupItem.gameObject);
    }

    [Command]
    private void CmdRemoveItem(int index)
    {
        if (items[index] != null)
            items.RemoveAt(index);
    }

    public override void OnStartLocalPlayer()
    {
        items.Callback += ItemChanged;
    }

    private void ItemChanged(SyncList<GbItem>.Operation op, int itemIndex)
    {
        onItemChanged(op, itemIndex);
    }
}