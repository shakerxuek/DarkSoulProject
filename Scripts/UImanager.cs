using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{   
    public PlayerInventory playerInventory;
    public EquipmentWindowUI equipmentWindowUI;
    
    public GameObject selectWindow;
    public GameObject Hudwindow;
    public GameObject equipmentScreenWindow;
    public GameObject weaponInventoryWindow;

    public bool rightHandSlot01Selected;
    public bool rightHandSlot02Selected;
    public bool rightHandSlot03Selected;
    public bool rightHandSlot04Selected;
    public bool leftHandSlot01Selected;
    public bool leftHandSlot02Selected;
    public bool leftHandSlot03Selected;
    public bool leftHandSlot04Selected;

    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab;
    public Transform weaponInventorySlotsParent;
    WeaponInventorySlot[] weaponInventorySlots;

    private void Awake() 
    {
    }
    private void Start() 
    {
        weaponInventorySlots=weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
    }
    public void UpdateUI()
    {   
        Debug.Log("updateUI");
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
        ResetAllSelectedSlots();
        weaponInventoryWindow.SetActive(false);
        equipmentScreenWindow.SetActive(false);
    }

    public void ResetAllSelectedSlots()
    {
        rightHandSlot01Selected=false;
        rightHandSlot02Selected=false;
        rightHandSlot03Selected=false;
        rightHandSlot04Selected=false;
        leftHandSlot01Selected=false;
        leftHandSlot02Selected=false;
        leftHandSlot03Selected=false;
        leftHandSlot04Selected=false;
    }

}
