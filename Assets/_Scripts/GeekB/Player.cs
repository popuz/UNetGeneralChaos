using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(StatsManager), typeof(NetworkIdentity))]
public class Player : MonoBehaviour
{
    [SerializeField] StatsManager _statsManager;

    [SerializeField] Character _character;
    [SerializeField] Inventory _inventory;
    [SerializeField] Equipment _equipment;

    public Character character => _character;
    public Inventory inventory => _inventory;
    public Equipment equipment => _equipment;

    public void Setup(Character character, Inventory inventory, Equipment equipment,
        bool isLocalPlayer)
    {
        _statsManager = GetComponent<StatsManager>();
        _character = character;
        _inventory = inventory;
        _equipment = equipment;
        _character.player = this;
        _inventory.player = this;
        _equipment.player = this;
        
        if (GetComponent<NetworkIdentity>().isServer)        
            _character.Stats.manager = _statsManager;     
        
        if (isLocalPlayer)
        {
            InventoryUI.instance.SetInventory(_inventory);
            EquipmentUI.instance.SetEquipment(_equipment);
            StatsUI.instance.SetManager(_statsManager);
        }          
    }

}