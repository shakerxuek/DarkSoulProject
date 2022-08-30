using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEquipmentSlotUI : MonoBehaviour
{
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

    public void AddItem(WeaponItem newWeapon)
    {
        weapon=newWeapon;
        if(newWeapon)
        {
            icon.sprite=weapon.itemIcon;
            icon.enabled=true;
            gameObject.SetActive(true);
        }
        Debug.Log("123");
        
    }

    public void ClearItem()
    {
        weapon=null;
        icon.sprite=null;
        icon.enabled=false;
        gameObject.SetActive(false);
    }
}
