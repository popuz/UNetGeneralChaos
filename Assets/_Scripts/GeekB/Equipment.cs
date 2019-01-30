﻿using UnityEngine.Networking;

public class Equipment : NetworkBehaviour
{
    public event SyncList<Item>.SyncListChanged onItemChanged;
    public SyncListItem items = new SyncListItem();
    public Player player;

    public override void OnStartLocalPlayer()
    {
        items.Callback += ItemChanged;
    }

    private void ItemChanged(SyncList<Item>.Operation op, int itemIndex)
    {
        onItemChanged(op, itemIndex);
    }

    public EquipmentItem EquipItem(EquipmentItem item)
    {
        EquipmentItem oldItem = null;
        for (int i = 0; i < items.Count; i++)
        {
            if (((EquipmentItem) items[i]).equipSlot == item.equipSlot)
            {
                oldItem = (EquipmentItem) items[i];
                items.RemoveAt(i);
                break;
            }
        }

        items.Add(item);
        return oldItem;
    }

    public void UnequipItem(Item item)
    {
        CmdUnequipItem(items.IndexOf(item));
    }

    [Command]
    void CmdUnequipItem(int index)
    {
        if (items[index] != null && player.inventory.AddItem(items[index]))
        {
            items.RemoveAt(index);
        }
    }
}