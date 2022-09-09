using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{   
    WeaponSlotManager weaponSlotManager;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    public WeaponItem unarmedWeapon;

    public WeaponItem[] weaponInRightHandSlots= new WeaponItem[1];
    public WeaponItem[] weaponInLeftHandSlots= new WeaponItem[1];

    public int currentRightWeaponIndex=-1;
    public int currentLeftWeaponIndex=-1;

    public List<WeaponItem> weaponInventory;

    private void Awake()
    {
        weaponSlotManager=GetComponentInChildren<WeaponSlotManager>();
        currentLeftWeaponIndex=-1;
        currentRightWeaponIndex=-1;
        
    }
    
    private void Start()
    {
        // rightWeapon=weaponInRightHandSlots[0];
        // leftWeapon=weaponInLeftHandSlots[0];
        // weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        // weaponSlotManager.LoadWeaponOnSlot(leftWeapon,true);
    }

    public void ChangeRightWeapon()
    {
        currentRightWeaponIndex = currentRightWeaponIndex + 1;

            if (currentRightWeaponIndex > weaponInRightHandSlots.Length - 1)
            {
                currentRightWeaponIndex = -1;
                rightWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
            }
            else if (weaponInRightHandSlots[currentRightWeaponIndex] != null)
            {
                rightWeapon = weaponInRightHandSlots[currentRightWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlots[currentRightWeaponIndex], false);
            }
            else
            {
                currentRightWeaponIndex = currentRightWeaponIndex + 1;
            }
    }

    public void ChangeLeftWeapon()
    {
        currentLeftWeaponIndex = currentLeftWeaponIndex + 1;

            if (currentLeftWeaponIndex > weaponInLeftHandSlots.Length - 1)
            {
                currentLeftWeaponIndex = -1;
                leftWeapon = unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);
            }
            else if (weaponInLeftHandSlots[currentLeftWeaponIndex] != null)
            {
                leftWeapon = weaponInLeftHandSlots[currentLeftWeaponIndex];
                weaponSlotManager.LoadWeaponOnSlot(weaponInLeftHandSlots[currentLeftWeaponIndex], true);
            }
            else
            {
                currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
            }
    }
}
