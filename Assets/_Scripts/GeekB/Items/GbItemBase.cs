﻿using UnityEngine;

public class GbItemBase : MonoBehaviour
{
    public static ItemCollection collection;
    [SerializeField] ItemCollection collectionLink;

    private void Awake()
    {
        if (collection != null)
        {
            if (collectionLink != collection) Debug.LogError("More than one ItemCollection found!");
            return;
        }

        collection = collectionLink;
    }

    public static int GetItemId(Item item)
    {
        for (int i = 0; i < collection.items.Length; i++)
            if (item == collection.items[i])
                return i;

        if (item != null) Debug.LogError("Items " + item.name + " not found in ItemBase!");
        return -1;
    }

    public static Item GetItem(int id) => id == -1 ? null : collection.items[id];
}