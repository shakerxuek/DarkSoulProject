using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputHandler inputHandler;
    CameraHandler cameraHandler;
    PlayerLocamotion playerLocamotion;
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
    }

    void Update()
    {   
        float delta=Time.deltaTime;
        isInteracting=anim.GetBool("isInteracting");
        canDocombo=anim.GetBool("canDocombo");
        
        inputHandler.TickInput(delta);
        playerLocamotion.HandleMovement(delta);
        playerLocamotion.HandleSprinting(delta);
        playerLocamotion.HandleFalling(delta,playerLocamotion.moveDirection);
        checkForInteractableObject();
    
    }

    private void FixedUpdate() 
    {
        float delta =Time.fixedDeltaTime;

        if(cameraHandler !=null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta,inputHandler.mouseX,inputHandler.mouseY);
        }
    }

    private void LateUpdate() 
    {   
        inputHandler.rollFlag=false;
        inputHandler.sprintFlag=false;
        inputHandler.rb_Input=false;
        inputHandler.rt_Input=false;
        inputHandler.dpad_down=false;
        inputHandler.dpad_up=false;
        inputHandler.dpad_left=false;
        inputHandler.dpad_right=false;
        inputHandler.a_Input=false;


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

                    if(inputHandler.a_Input)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
    }
}
