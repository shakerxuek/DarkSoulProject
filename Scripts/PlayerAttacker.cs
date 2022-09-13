using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{   
    AnimatorHandler animatorHandler;
    InputHandler inputHandler;
    WeaponSlotManager weaponSlotManager;
    public string lastAttack;

    private void Awake() 
    {
        animatorHandler =GetComponentInChildren<AnimatorHandler>();
        weaponSlotManager=GetComponentInChildren<WeaponSlotManager>();
        inputHandler=GetComponent<InputHandler>();
        
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {   
        if(inputHandler.comboFlag)
        {
            animatorHandler.anim.SetBool("canDocombo",false);
            if(lastAttack==weapon.OH_Light_Attack_1)
            {
                animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2,true);
            }
            else if(lastAttack==weapon.th_attack2)
            {   
                animatorHandler.PlayTargetAnimation(weapon.th_attack3,true);
            }
        }
        
    }
    public void HandleLightAttack(WeaponItem weapon)
    {   
        weaponSlotManager.attackingWeapon=weapon;
        if(inputHandler.twohandFlag)
        {
            animatorHandler.PlayTargetAnimation(weapon.th_attack2,true);
            lastAttack=weapon.th_attack2;
        }
        else
        {
            
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1,true);
            lastAttack=weapon.OH_Light_Attack_1;
        }
        
    }

    public void HandleHeavyAttack(WeaponItem weapon)
    {   
        weaponSlotManager.attackingWeapon=weapon;
        if(inputHandler.twohandFlag)
        {
            animatorHandler.PlayTargetAnimation(weapon.th_attack1,true);
            lastAttack=weapon.th_attack1;
        }
        else
        {
            
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1,true);
            lastAttack=weapon.OH_Heavy_Attack_1;
        }
        
    }
}
