using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{   
    PlayerManager playerManager;
    public Animator anim;
    InputHandler inputHandler;
    PlayerLocamotion playerLocamotion;
    int vertical;
    int horizontal;
    public bool canRotate;
    public void Initialize()
    {   
        playerManager=GetComponentInParent<PlayerManager>();
        anim= GetComponent<Animator>();
        vertical =Animator.StringToHash("Vertical");
        horizontal=Animator.StringToHash("Horizontal");
        inputHandler=GetComponentInParent<InputHandler>();
        playerLocamotion=GetComponentInParent<PlayerLocamotion>();
    }

    public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSpringting)
    {
        float v =0;

        if(verticalMovement >0 && verticalMovement <0.55f)
        {
            v=0.5f;
        }
        else if(verticalMovement >0.55f)
        {
            v=1;
        }
        else if(verticalMovement <0 && verticalMovement >-0.55f)
        {
            v=-0.5f;
        }
        else if(verticalMovement <0.55f)
        {
            v=-1;
        }
        else
        {
            v=0;
        }

        float h=0;

        if(horizontalMovement >0 && horizontalMovement <0.55f)
        {
            h = 0.5f;
        }
        else if(horizontalMovement <0.55f)
        {
            h=-1;
        }
        else
        {
            h=0;
        }

        if(isSpringting)
        {
            v=2;
            h=horizontalMovement;
        }

        anim.SetFloat(vertical,v,0.1f,Time.deltaTime);
        anim.SetFloat(horizontal,h,0.1f,Time.deltaTime);
    }
    public void CanRotate()
    {
        canRotate=true;
        
    }

    public void StopRotation()
    {
        canRotate=false;
    }

    public void PlayTargetAnimation(string targetAnim, bool isInteracting)
    {
        anim.applyRootMotion =isInteracting;
        anim.SetBool("isInteracting",isInteracting);
        anim.CrossFade(targetAnim, 0.2f);
    }

    private void OnAnimatorMove() 
    {
        if(playerManager.isInteracting ==false)
            return;
        float delta =Time.deltaTime;
        playerLocamotion.rigidbody.drag=0;
        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y=0; 
        Vector3 Velocity = deltaPosition/delta;
        playerLocamotion.rigidbody.velocity =Velocity;
    }

    public void EnableCombo()
    {
        anim.SetBool("canDocombo", true);
    }

    public void DisableCombo()
    {
        anim.SetBool("canDocombo", false);
    }
}
