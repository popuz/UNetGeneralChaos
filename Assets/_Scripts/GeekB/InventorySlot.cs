using UnityEngine;
using UnityEngine.UI; 

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton; 
    public GbInventory inventory; 

    GbItem item;

    private void Awake()
    {
        removeButton.onClick.AddListener(OnRemoveButton);
        this.GetComponent<Button>().onClick.AddListener(UseItem);
    }

    public void SetItem(GbItem newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        inventory.Remove(item);
    }

    public void UseItem()
    {
        if (item != null) item.Use();
    }
}