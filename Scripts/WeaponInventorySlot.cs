using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{   
    PlayerInventory playerInventory;
    WeaponSlotManager weaponSlotManager;
    UImanager uImanager;
    public Image icon;
    WeaponItem item;

    private void Awake() 
    {
        playerInventory=FindObjectOfType<PlayerInventory>();
        uImanager=FindObjectOfType<UImanager>();
        weaponSlotManager=FindObjectOfType<WeaponSlotManager>();
    }

    public void AddItem(WeaponItem newItem)
    {
        item=newItem;
        icon.sprite=item.itemIcon;
        icon.enabled=true;
        gameObject.SetActive(true);
    }

    public void ClearInventorySlot()
    {
        item=null;
        icon.sprite=null;
        icon.enabled=false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem()
    {   
        if(uImanager.rightHandSlot01Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponInRightHandSlots[0]);
            playerInventory.weaponInRightHandSlots[0]=item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if(uImanager.rightHandSlot02Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponInRightHandSlots[1]);
            playerInventory.weaponInRightHandSlots[1]=item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if(uImanager.rightHandSlot03Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponInRightHandSlots[2]);
            playerInventory.weaponInRightHandSlots[2]=item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if(uImanager.rightHandSlot04Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponInRightHandSlots[3]);
            playerInventory.weaponInRightHandSlots[3]=item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if(uImanager.leftHandSlot01Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponInLeftHandSlots[0]);
            playerInventory.weaponInLeftHandSlots[0]=item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if(uImanager.leftHandSlot02Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponInLeftHandSlots[1]);
            playerInventory.weaponInLeftHandSlots[1]=item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if(uImanager.leftHandSlot03Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponInLeftHandSlots[2]);
            playerInventory.weaponInLeftHandSlots[2]=item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if(uImanager.leftHandSlot04Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponInLeftHandSlots[3]);
            playerInventory.weaponInLeftHandSlots[3]=item;
            playerInventory.weaponInventory.Remove(item);
        }
        else
        {
            return;
        }

        if(playerInventory.currentRightWeaponIndex !=-1 && playerInventory.currentLeftWeaponIndex != -1)
        {
            playerInventory.rightWeapon=playerInventory.weaponInRightHandSlots[playerInventory.currentRightWeaponIndex];
            playerInventory.leftWeapon=playerInventory.weaponInLeftHandSlots[playerInventory.currentLeftWeaponIndex];

            weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
            weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon,true);
        }
        uImanager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        uImanager.UpdateUI();
        uImanager.ResetAllSelectedSlots();
       
        
    }
}
