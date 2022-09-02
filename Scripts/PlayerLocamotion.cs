using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocamotion : MonoBehaviour
{   
    PlayerManager playerManager;
    Transform cameraObject;
    InputHandler inputHandler;
    public Vector3 moveDirection;

    public Transform myTransfrom;
    public AnimatorHandler animatorHandler;
    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    float groundDetectionRayStartPoint =0.5f;
    float minimumDistanceNeededToBeginFall=1f;
    float groundDirectionRayDistance=0.2f;
    LayerMask ignoreForGroundCheck;
    public float inAirTimer;
    [SerializeField]
    float movementSpeed =5;
    float sprintSpeed =7;
    [SerializeField]
    float rotationSpeed =10;
    float fallingSpeed=200;

    void Start()
    {   
        playerManager=GetComponent<PlayerManager>();
        rigidbody =GetComponent<Rigidbody>();
        inputHandler =GetComponent<InputHandler>();
        cameraObject =Camera.main.transform;
        myTransfrom=transform;
        animatorHandler=GetComponentInChildren<AnimatorHandler>();
        animatorHandler.Initialize();

        playerManager.isGrounded=true;
        ignoreForGroundCheck =~(1<<8 |1 <<11);
    }

    Vector3 normalVector;
    Vector3 targetPosition;

    private void HandleRotation(float delta)
    {
        Vector3 targetDir =Vector3.zero;
        float moveOveride =inputHandler.moveAmount;

        targetDir = cameraObject.forward * inputHandler.vertical;
        targetDir += cameraObject.right * inputHandler.horizontal;

        targetDir.Normalize();
        targetDir.y=0;

        if(targetDir == Vector3.zero)
            targetDir= myTransfrom.forward;

        float rs=rotationSpeed;

        Quaternion tr = Quaternion.LookRotation(targetDir);
        Quaternion targetRotation = Quaternion.Slerp(myTransfrom.rotation, tr, rs*delta);

        myTransfrom.rotation=targetRotation;
    }

    

    public void HandleMovement(float delta)
    {   
        if(inputHandler.rollFlag)
            return;
        if(playerManager.isInteracting)
            return;
        moveDirection = cameraObject.forward*inputHandler.vertical;
        moveDirection +=cameraObject.right*inputHandler.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float speed = movementSpeed;
        if(inputHandler.sprintFlag && inputHandler.moveAmount>0.5)
        {
            speed=sprintSpeed;
            playerManager.isSpringting=true;
            moveDirection *=speed;
        }
        else
        {    
            moveDirection *= speed;
            playerManager.isSpringting=false;
        }
        
        Vector3 projectedVelocity =Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;   

        animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount,0,playerManager.isSpringting);
        
        if(animatorHandler.canRotate)
        {
            HandleRotation(delta);
        }
    }

    public void HandleSprinting(float delta)
    {
        if(animatorHandler.anim.GetBool("isInteracting"))
            return;

        if(inputHandler.rollFlag)
        {
            moveDirection = cameraObject.forward*inputHandler.vertical;
            moveDirection+=cameraObject.right*inputHandler.horizontal;

            if(inputHandler.moveAmount>0)
            {
                animatorHandler.PlayTargetAnimation("roll",true);
                moveDirection.y=0;
                Quaternion rollRotation =Quaternion.LookRotation(moveDirection);
                myTransfrom.rotation=rollRotation;
            }
            else
            {
                animatorHandler.PlayTargetAnimation("backstep",true);
            }
        }
    }

    public void HandleFalling(float delta, Vector3 moveDirection)
    {
        playerManager.isGrounded=false;
        RaycastHit hit;
        Vector3 origin =myTransfrom.position;
        origin.y+=groundDetectionRayStartPoint;

        if(Physics.Raycast(origin,myTransfrom.forward,out hit, 0.4f))
        {
            moveDirection=Vector3.zero;
        }

        if(playerManager.isInAir)
        {
            rigidbody.AddForce(-Vector3.up *fallingSpeed*(1+inAirTimer));
            rigidbody.AddForce(moveDirection*fallingSpeed/6f);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin=origin+dir*groundDirectionRayDistance;

        targetPosition =myTransfrom.position;

        // Debug.DrawRay(origin, -Vector3.up*minimumDistanceNeededToBeginFall, Color.red, 0.1f,false);
        if(Physics.Raycast(origin, -Vector3.up,out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
        {
            normalVector=hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded =true;
            targetPosition.y=tp.y;

            if(playerManager.isInAir)
            {
                if(inAirTimer >0.2f)
                {
                    Debug.Log("you were in the air for"+inAirTimer);
                    animatorHandler.PlayTargetAnimation("Land",true);
                    inAirTimer=0;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Empty",false);
                    inAirTimer=0;
                }
                playerManager.isInAir=false;
            }
        }
        else
        {
            if(playerManager.isGrounded)
            {
                playerManager.isGrounded=false;
            }
            if(playerManager.isInAir==false)
            {
                if(playerManager.isInteracting==false)
                {
                    animatorHandler.PlayTargetAnimation("Falling",true);
                }
                Vector3 vel =rigidbody.velocity;
                vel.Normalize();
                rigidbody.velocity=vel*(movementSpeed/2);
                playerManager.isInAir=true;
            }
        }
        
        if(playerManager.isInteracting || inputHandler.moveAmount >0)
        {
            myTransfrom.position =Vector3.Lerp(myTransfrom.position, targetPosition, Time.deltaTime/0.2f);
        }
        else
        {
            myTransfrom.position =targetPosition;
        }
    }

    public void HandelJumping()
    {
        if(playerManager.isInteracting)
            return;

        if(inputHandler.jump_Input)
        {
            playerManager.isInteracting=false;
            moveDirection =cameraObject.forward*inputHandler.vertical;
            moveDirection+= cameraObject.right *inputHandler.horizontal;
                
            animatorHandler.PlayTargetAnimation("Jump forward",true);
            moveDirection.y=0;
            Quaternion jumpRotation= Quaternion.LookRotation(moveDirection);
            myTransfrom.rotation=jumpRotation;
        }
    }
}
