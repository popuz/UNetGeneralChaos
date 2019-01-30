using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Character _character;
    [SerializeField] Inventory _inventory;
    [SerializeField] Equipment _equipment;

    public Character character => _character;
    public Inventory inventory => _inventory;
    public Equipment equipment => _equipment;

    public void Setup(Character character, Inventory inventory, Equipment equipment,
        bool isLocalPlayer)
    {
        _character = character;
        _inventory = inventory;
        _equipment = equipment;
        _character.player = this;
        _inventory.player = this;
        _equipment.player = this;
        if (isLocalPlayer)
        {
            InventoryUI.instance.SetInventory(_inventory);
        }
    }
}