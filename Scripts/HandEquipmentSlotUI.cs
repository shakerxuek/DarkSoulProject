using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEquipmentSlotUI : MonoBehaviour
{   
    UImanager uImanager;
    public Image icon;
    WeaponItem weapon;
    public bool rightHandSlot01;
    public bool rightHandSlot02;
    public bool rightHandSlot03;
    public bool rightHandSlot04;
    public bool leftHandSlot01;
    public bool leftHandSlot02;
    public bool leftHandSlot03;
    public bool leftHandSlot04;

    private void Awake() 
    {
        uImanager=FindObjectOfType<UImanager>();
    }
    public void AddItem(WeaponItem newWeapon)
    {
        weapon=newWeapon;
        if(newWeapon)
        {
            icon.sprite=weapon.itemIcon;
            icon.enabled=true;
            gameObject.SetActive(true);
        }
        
    }

    public void ClearItem()
    {
        weapon=null;
        icon.sprite=null;
        icon.enabled=false;
        gameObject.SetActive(false);
    }

    public void SelectThisSlot()
    {
        if(rightHandSlot01)
        {
            uImanager.rightHandSlot01Selected=true;
        }
        else if(rightHandSlot02)
        {
            uImanager.rightHandSlot02Selected=true;
        }
        else if(rightHandSlot03)
        {
            uImanager.rightHandSlot03Selected=true;
        }
        else if(rightHandSlot04)
        {
            uImanager.rightHandSlot04Selected=true;
        }
        else if(leftHandSlot01)
        {
            uImanager.leftHandSlot01Selected=true;
        }
        else if(leftHandSlot02)
        {
            uImanager.leftHandSlot02Selected=true;
        }
        else if(leftHandSlot03)
        {
            uImanager.leftHandSlot03Selected=true;
        }
        else if(leftHandSlot04)
        {
            uImanager.leftHandSlot04Selected=true;
        }
    }
}
