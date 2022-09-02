using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputHandler inputHandler;
    CameraHandler cameraHandler;
    PlayerLocamotion playerLocamotion;

    InteractableUI interactableUI;
    public GameObject interactableGameObject;
    public GameObject IteminteractableGameObject;
    Animator anim;
    public bool isInteracting;
    public bool isSpringting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDocombo;

    

    void Start()
    {
        inputHandler =GetComponent<InputHandler>();
        anim=GetComponentInChildren<Animator>();
        cameraHandler =CameraHandler.singleton;
        playerLocamotion=GetComponent<PlayerLocamotion>();
        interactableUI=FindObjectOfType<InteractableUI>();
    }

    void Update()
    {   
        float delta=Time.deltaTime;
        isInteracting=anim.GetBool("isInteracting");
        canDocombo=anim.GetBool("canDocombo");
        anim.SetBool("isInAir",isInAir);
        
        inputHandler.TickInput(delta);
        
        playerLocamotion.HandleSprinting(delta);
        playerLocamotion.HandelJumping();
        checkForInteractableObject();
    
    }

    private void FixedUpdate() 
    {
        float delta =Time.fixedDeltaTime;
        playerLocamotion.HandleMovement(delta);
        playerLocamotion.HandleFalling(delta,playerLocamotion.moveDirection);
        
    }

    private void LateUpdate() 
    {   
        inputHandler.rollFlag=false;
        inputHandler.rb_Input=false;
        inputHandler.rt_Input=false;
        inputHandler.dpad_down=false;
        inputHandler.dpad_up=false;
        inputHandler.dpad_left=false;
        inputHandler.dpad_right=false;
        inputHandler.a_Input=false;
        inputHandler.jump_Input=false;
        inputHandler.inventory_Input=false;

        float delta =Time.deltaTime;
        if(cameraHandler !=null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta,inputHandler.mouseX,inputHandler.mouseY);
        }
        if(isInAir)
        {
            playerLocamotion.inAirTimer =playerLocamotion.inAirTimer + Time.deltaTime; 
        }
    }

    public void checkForInteractableObject()
    {
        RaycastHit hit;

        if(Physics.SphereCast(transform.position,0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
        {
            if(hit.collider.tag=="Interactable")
            {
                Interactable interactableObject=hit.collider.GetComponent<Interactable>();
                if(interactableObject!=null)
                {
                    string interactableText = interactableObject.InteractableText;
                    interactableUI.interactableText.text=interactableText;
                    interactableGameObject.SetActive(true);

                    if(inputHandler.a_Input)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if(interactableGameObject !=null)
            {
                interactableGameObject.SetActive(false);
            }

            if(IteminteractableGameObject !=null && inputHandler.a_Input)
            {
                IteminteractableGameObject.SetActive(false);
            }
        }
    }
}
