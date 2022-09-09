using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;



    public float rollInputTimer;
    public bool sprintFlag;
    public bool comboFlag;
    public bool rollFlag;
    public bool twohandFlag;
    public bool lockonFlag;
    public bool inventory_Flag;
    public bool a_Input;
    public bool b_Input;
    public bool y_input;
    public bool rb_Input;
    public bool rt_Input;
    public bool lockon_Input;
    public bool lockonleft_Input;
    public bool lockonright_Input;
    public bool inventory_Input;
    public bool dpad_up;
    public bool dpad_down;
    public bool dpad_left;
    public bool dpad_right;
    public bool jump_Input;


    Playercontrol inputActions;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    UImanager uImanager;
    CameraHandler cameraHandler;
    WeaponSlotManager weaponSlotManager;

    Vector2 movementInput;
    Vector2 cameraInput;


    void Awake()
    {
        playerAttacker=GetComponent<PlayerAttacker>();
        playerInventory=GetComponent<PlayerInventory>();
        playerManager=GetComponent<PlayerManager>();
        uImanager=FindObjectOfType<UImanager>();
        cameraHandler=FindObjectOfType<CameraHandler>();
        weaponSlotManager=GetComponentInChildren<WeaponSlotManager>();
    }

    
    public void  OnEnable() 
    {
        if(inputActions ==null)
        {
            inputActions = new Playercontrol();
            inputActions.Playermove.Movement.performed += inputActions => movementInput =inputActions.ReadValue<Vector2>();
            inputActions.Playermove.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.Playeraction.RB.performed += i =>rb_Input=true;
            inputActions.Playeraction.RT.performed += i =>rt_Input=true;
            inputActions.PlayerQuickSlots.Dpadright.performed += i =>dpad_right=true;
            inputActions.PlayerQuickSlots.Dpadleft.performed += i =>dpad_left=true;
            inputActions.Playeraction.A.performed += i =>a_Input=true;
            inputActions.Playeraction.Jump.performed +=i => jump_Input=true;
            inputActions.Playeraction.Inventory.performed +=i => inventory_Input=true;
            inputActions.Playeraction.Lockon.performed +=i=> lockon_Input=true;
            inputActions.Playermove.LockonLeft.performed +=i=>lockonleft_Input=true;
            inputActions.Playermove.LockonRight.performed+=i=>lockonright_Input=true;
            inputActions.Playeraction.Y.performed +=i=> y_input=true;
        }
        inputActions.Enable();
    }

    private void OnDisable() 
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        moveInput(delta);
        HandleSPInput(delta);
        HandleAttackInput(delta);
        HandleQuickSlotsInput();
        HandleInventoryInput();
        HandleLockonInput();
        HandleTwoHandInput();
    }

    private void moveInput(float delta)
    {
        horizontal =movementInput.x;
        vertical=movementInput.y;
        moveAmount=Mathf.Clamp01(Mathf.Abs(horizontal)+ Mathf.Abs(vertical));
        mouseX=cameraInput.x;
        mouseY=cameraInput.y;
    }

    private void HandleSPInput(float delta)
    {
        b_Input=inputActions.Playeraction.Sprint.phase==UnityEngine.InputSystem.InputActionPhase.Performed;
        sprintFlag=b_Input;
        if(b_Input)
        {
            rollInputTimer += delta;
        }
        else
        {
            if(rollInputTimer>0 && rollInputTimer<0.5f)
            {
                sprintFlag=false;
                rollFlag=true;
            }

            rollInputTimer=0;
        }
    }

    private void HandleAttackInput(float delta)
    {
        if(rb_Input)
        {   
            if(playerManager.canDocombo)
            {   
                comboFlag=true;
                playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                comboFlag=false;
            }
            else
            {
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }
            
        }
        if(rt_Input)
        {
            playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
        }
    }

    private void HandleQuickSlotsInput()
    {   

        if(dpad_right)
        {
            playerInventory.ChangeRightWeapon();
        }
        else if(dpad_left)
        {
            playerInventory.ChangeLeftWeapon();
        }
    }

    private void HandleInventoryInput()
    {
        if(inventory_Input)
        {
            inventory_Flag= !inventory_Flag;
            
            if(inventory_Flag)
            {
                uImanager.OpenSelectWindow();
                uImanager.UpdateUI();
                uImanager.Hudwindow.SetActive(false);
            }
            else
            {
                uImanager.CloseSelectWindow();
                uImanager.CloseAllInventoryWindows();
                uImanager.Hudwindow.SetActive(true);
            }
        }
    }
    private void HandleLockonInput()
    {
        if(lockon_Input && lockonFlag == false)
        {   
            lockon_Input=false;
            cameraHandler.HandleLockon();
            if(cameraHandler.nearestLockonTarget !=null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.nearestLockonTarget;
                lockonFlag=true;
            }
            
        }
        else if(lockon_Input && lockonFlag)
        {
            lockon_Input=false;
            lockonFlag=false;
            cameraHandler.ClearLockOnTargets();
        }

        if(lockonFlag && lockonright_Input)
        {
            lockonright_Input=false;
            cameraHandler.HandleLockon();
            if(cameraHandler.rightLockTarget !=null)
            {
                cameraHandler.currentLockOnTarget=cameraHandler.rightLockTarget;
            }
        }

        if(lockonFlag && lockonleft_Input)
        {
            lockonleft_Input=false;
            cameraHandler.HandleLockon();
            if(cameraHandler.leftLockTarget !=null)
            {
                cameraHandler.currentLockOnTarget=cameraHandler.leftLockTarget;
            }
        }
    }

    private void HandleTwoHandInput()
    {
        if(y_input)
        {   
            y_input=false;
            twohandFlag=!twohandFlag;
            if(twohandFlag && playerInventory.rightWeapon!=null)
            {   
                playerInventory.leftWeapon=playerInventory.unarmedWeapon;
                weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon,true);
                weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
            }
            else
            {   
                if(playerInventory.rightWeapon!=null)
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon,true);
                }
                
            }
        }
    }
}
