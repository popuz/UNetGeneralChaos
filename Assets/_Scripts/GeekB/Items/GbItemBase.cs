using UnityEngine;

public class GbItemBase : MonoBehaviour
{
    public static GbItemCollection collection;
    [SerializeField] GbItemCollection collectionLink;

    private void Awake()
    {
        if (collection != null)
        {
            if (collectionLink != collection) Debug.LogError("More than one ItemCollection found!");
            return;
        }

        collection = collectionLink;
    }

    public static int GetItemId(GbItem item)
    {
        for (int i = 0; i < collection.items.Length; i++)
            if (item == collection.items[i])
                return i;

        if (item != null) Debug.LogError("Items " + item.name + " not found in ItemBase!");
        return -1;
    }

    public static GbItem GetItem(int id) => id == -1 ? null : collection.items[id];
}