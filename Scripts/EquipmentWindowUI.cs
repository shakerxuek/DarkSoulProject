using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindowUI : MonoBehaviour
{
    public bool right01selected;
    public bool right02selected;
    public bool right03selected;
    public bool right04selected;
    public bool left01selected;
    public bool left02selected;
    public bool left03selected;
    public bool left04selected;

    HandEquipmentSlotUI[] handEquipmentSlotUI;

    private void Awake()
    {
        handEquipmentSlotUI=GetComponentsInChildren<HandEquipmentSlotUI>();
    }

    public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
    {
        for (int i = 0; i < handEquipmentSlotUI.Length; i++)
        {
            if(handEquipmentSlotUI[i].rightHandSlot01)
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlots[0]);
            }
            else if(handEquipmentSlotUI[i].rightHandSlot02)
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlots[1]);
            }
            else if(handEquipmentSlotUI[i].rightHandSlot03)
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlots[2]);
            }
            else if(handEquipmentSlotUI[i].rightHandSlot04)
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlots[3]);
            }
            else if(handEquipmentSlotUI[i].leftHandSlot01)
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInLeftHandSlots[0]);
            }
            else if(handEquipmentSlotUI[i].leftHandSlot02)
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInLeftHandSlots[1]);
            }
            else if(handEquipmentSlotUI[i].leftHandSlot03)
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInLeftHandSlots[2]);
            }
            else
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInLeftHandSlots[3]);
            }
        }
    }

    public void Right01Selected()
    {
        right01selected=true;
    }
    public void Right02Selected()
    {
        right02selected=true;
    }
    public void Right03Selected()
    {
        right03selected=true;
    }
    public void Right04Selected()
    {
        right04selected=true;
    }
    public void Left01Selected()
    {
        left01selected=true;
    }
    public void Left02Selected()
    {
        left02selected=true;
    }
    public void Left03Selected()
    {
        left03selected=true;
    }
    public void Left04Selected()
    {
        left04selected=true;
    }
}
