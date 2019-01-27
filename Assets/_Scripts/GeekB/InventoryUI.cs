using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    
    [ SerializeField ] GameObject inventoryUI;
   
    public static InventoryUI instance;
    GbInventory inventory;

    [SerializeField] Transform itemsParent;
    [SerializeField] InventorySlot slotPrefab;
    InventorySlot[] slots;

    private void Awake()
    {        
        inventoryUI.SetActive(false);
        if (instance != null)
        {
            Debug.LogError("More than one instance of InventoryUI found!");
            return;
        }

        instance = this;
    }

    private void Update () {
        if (Input.GetButtonDown( "Inventory" )) {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
    
    public void SetInventory(GbInventory newInventory)
    {
        inventory = newInventory;
        inventory.onItemChanged += ItemChanged;
        InventorySlot[] childs = itemsParent.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < childs.Length; i++) Destroy(childs[i].gameObject);

        slots = new InventorySlot[inventory.space];
        for (int i = 0; i < inventory.space; i++)
        {
            slots[i] = Instantiate(slotPrefab, itemsParent);

            slots[i].inventory = inventory;
            if (i < inventory.items.Count) slots[i].SetItem(inventory.items[i]);
            else slots[i].ClearSlot();
        }
    }

    private void ItemChanged(UnityEngine.Networking.SyncList<GbItem>.Operation op, int itemIndex)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count) slots[i].SetItem(inventory.items[i]);
            else slots[i].ClearSlot();
        }
    }
}