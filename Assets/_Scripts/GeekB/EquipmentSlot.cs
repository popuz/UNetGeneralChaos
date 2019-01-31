using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;
    public Button unequipButton;
    public Equipment equipment;
    private Item item;

    private void Awake()
    {
        unequipButton.onClick.AddListener(Unequip);
    }
    
    public void SetItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        unequipButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        unequipButton.interactable = false;
    }

    public void Unequip() => equipment.UnequipItem(item);
}