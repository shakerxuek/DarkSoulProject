using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    WeaponHolderSlot leftHandSlot;
    WeaponHolderSlot rightHandSlot;

    DamageCollider leftHandDamageCollider;
    DamageCollider rightHandDamageCollider;

    Animator animator;
    QuickSlotsUI quickSlotsUI;
    PlayerStats playerStats;
    InputHandler inputHandler;
    public WeaponItem attackingWeapon;
    private void Awake()
    {   
        animator=GetComponent<Animator>();
        quickSlotsUI=FindObjectOfType<QuickSlotsUI>();
        playerStats=GetComponentInParent<PlayerStats>();
        inputHandler=GetComponentInParent<InputHandler>();
        WeaponHolderSlot[] weaponHolderSlots= GetComponentsInChildren<WeaponHolderSlot>();
        foreach(WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if(weaponSlot.isLeftHandSlot)
            {
                leftHandSlot=weaponSlot;
            }
            else if(weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if(isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();
            quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);

            if(weaponItem!=null)
            {
                animator.CrossFade(weaponItem.Left_hand_idle, 0.2f);
            }
            else
            {
                animator.CrossFade("Left Arm Empty",0.2f);
            }
        }
        else
        {   
            animator.CrossFade("both Arms Empty",0.2f);
            if(inputHandler.twohandFlag)
            {
                animator.CrossFade(weaponItem.twohand_idle,0.2f);
            }
            else
            {
                
                if(weaponItem!=null)
                {
                    animator.CrossFade(weaponItem.Right_hand_idle, 0.2f);
                }
                else
                {
                    animator.CrossFade("Right Arm Empty",0.2f);
                }
            }
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();
            quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);

            
        }
    }

    private void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider=leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    private void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider=rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
    }

    public void OpenRightDamageCollider()
    {
        rightHandDamageCollider.EnableDamageCollider();
    }

    public void OpenLeftDamageCollider()
    {
        leftHandDamageCollider.EnableDamageCollider();
    }

    public void CloseRightHandDamageCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
    }

    public void CloseLeftHandDamageCollider()
    {
        leftHandDamageCollider.DisableDamageCollider();
    }

    public void DrainStaminaLightAttack()
    {
        playerStats.TakStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina*attackingWeapon.lightAttackMultiplier));
    }
    public void DrainStaminaHeavyAttack()
    {
        playerStats.TakStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina*attackingWeapon.heavyAttackMultiplier));
    }
}
