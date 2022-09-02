using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{   
    public PlayerInventory playerInventory;
    EquipmentWindowUI equipmentWindowUI;
    
    public GameObject selectWindow;
    public GameObject Hudwindow;
    public GameObject weaponInventoryWindow;

    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab;
    public Transform weaponInventorySlotsParent;
    WeaponInventorySlot[] weaponInventorySlots;

    private void Awake() 
    {
        equipmentWindowUI=FindObjectOfType<EquipmentWindowUI>();
    }
    private void Start() 
    {
        weaponInventorySlots=weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
    }
    public void UpdateUI()
    {
        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            if(i<playerInventory.weaponInventory.Count)
            {
                if(weaponInventorySlots.Length<playerInventory.weaponInventory.Count)
                {
                    Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                    weaponInventorySlots=weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                }
                weaponInventorySlots[i].AddItem(playerInventory.weaponInventory[i]);
            }
            else
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }
        
    }

    public void OpenSelectWindow()
    {
        selectWindow.SetActive(true);
    }

    public void CloseSelectWindow()
    {
        selectWindow.SetActive(false);
    }

    public void CloseAllInventoryWindows()
    {
        weaponInventoryWindow.SetActive(false);
    }

}