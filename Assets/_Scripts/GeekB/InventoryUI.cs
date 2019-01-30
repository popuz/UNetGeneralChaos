using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;
    
    [ SerializeField ] GameObject inventoryUI;          
    [SerializeField] Transform itemsParent;
    [SerializeField] InventorySlot slotPrefab;

    Inventory inventory;
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
        if (Input.GetButtonDown( "Inventory" ))
            inventoryUI.SetActive(!inventoryUI.activeSelf);        
    }
    
    public void SetInventory(Inventory newInventory)
    {
        inventory = newInventory;
        inventory.onItemChanged += ItemChanged;
        var childes = itemsParent.GetComponentsInChildren<InventorySlot>();

        foreach (var child in childes)
            Destroy(child.gameObject);

        slots = new InventorySlot[inventory.space];
        for (var i = 0; i < inventory.space; i++)
        {
            slots[i] = Instantiate(slotPrefab, itemsParent);

            slots[i].inventory = inventory;
            if (i < inventory.items.Count) slots[i].SetItem(inventory.items[i]);
            else slots[i].ClearSlot();
        }
    }

    private void ItemChanged(UnityEngine.Networking.SyncList<Item>.Operation op, int itemIndex)
    {
        for (var i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count) slots[i].SetItem(inventory.items[i]);
            else slots[i].ClearSlot();
        }
    }
}