﻿using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    #region Singleton

    public static EquipmentUI instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of InventoryUI found!");
            return;
        }

        instance = this;
    }

    #endregion

    [SerializeField] EquipmentSlot headSlot;
    [SerializeField] EquipmentSlot chestSlot;
    [SerializeField] EquipmentSlot legsSlot;
    [SerializeField] EquipmentSlot righHandSlot;
    [SerializeField] EquipmentSlot leftHandSlot;

    EquipmentSlot[] slots;
    [SerializeField] GameObject equipmentUI;

    private void Start()
    {
        slots = new EquipmentSlot[System.Enum.GetValues(typeof(EquipmentSlotType)).Length];

        slots[(int) EquipmentSlotType.Chest] = chestSlot;
        slots[(int) EquipmentSlotType.Head] = headSlot;
        slots[(int) EquipmentSlotType.LeftHand] = leftHandSlot;
        slots[(int) EquipmentSlotType.Legs] = legsSlot;
        slots[(int) EquipmentSlotType.RighHand] = righHandSlot;

        equipmentUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Equipment"))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
    }


    private Equipment equipment;

    public void SetEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;
        equipment.onItemChanged += ItemChanged;
        for (int i = 0; i < slots.Length; i++)
            if (slots[i] != null)
                slots[i].equipment = equipment;

        ItemChanged(0, 0);
    }


    private void ItemChanged(UnityEngine.Networking.SyncList<Item>.Operation op, int itemIndex)
    {

        for (int i = 0; i < slots.Length; i++) slots[i].ClearSlot();

        for (int i = 0; i < equipment.items.Count; i++) {
            int slotIndex = (int) ((EquipmentItem) equipment.items[i]).equipSlot;
            slots[slotIndex].SetItem(equipment.items[i]);
        }
    }
}