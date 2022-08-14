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
        
        inputHandler.TickInput(delta);
        playerLocamotion.HandleMovement(delta);
        playerLocamotion.HandleSprinting(delta);
        playerLocamotion.HandleFalling(delta,playerLocamotion.moveDirection);
        
    
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


        if(isInAir)
        {
            playerLocamotion.inAirTimer =playerLocamotion.inAirTimer + Time.deltaTime; 
        }
    }
}
