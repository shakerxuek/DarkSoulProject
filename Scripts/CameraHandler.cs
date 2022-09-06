using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{   
    InputHandler inputHandler;
    PlayerManager playerManager;
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Transform myTransform;
    private Vector3 cameraTransformPosition;
    public LayerMask ignoreLayers;
    public LayerMask environmentLayer;
    private Vector3 cameraFollowVelocity= Vector3.zero;

    public static CameraHandler singleton;

    public float lookSpeed =0.1f;
    public float followSpeed =0.1f;
    public float pivotSpeed =0.03f;

    private float targetPosition;
    private float defaultPosition;
    private float lookAngle;
    private float pivotAngle;
    public float minimumPivot=-35;
    public float maximumPivot=35;   

    public float cameraSphereRadius =0.2f;
    public float cameraCollisionOffset =0.2f;
    public float minimumCollisionOffset = 0.2f;
    public Transform nearestLockonTarget;
    public Transform currentLockOnTarget;
    public float maximumLockonDistance =30;
    public Transform leftLockTarget;
    public Transform rightLockTarget;
    List <characterManager> avilableTargets = new List<characterManager>();
    private void Awake() 
    {   
        inputHandler=FindObjectOfType<InputHandler>();
        singleton=this;
        myTransform=transform;
        defaultPosition=cameraTransform.localPosition.z;
        ignoreLayers=~(1<<8 | 1<<9 | 1<<10);
        playerManager=FindObjectOfType<PlayerManager>();
    }
    private void Start() 
    {
        environmentLayer=LayerMask.NameToLayer("Environment");
    }

    public void FollowTarget(float delta)
    {
        Vector3 targetPosition =Vector3.SmoothDamp(myTransform.position,targetTransform.position, ref cameraFollowVelocity, delta/followSpeed);
        myTransform.position =targetPosition;

        HandleCameraCollisions(delta);
    }

    public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
    {   
        if(inputHandler.lockonFlag==false)
        {   
            cameraSphereRadius=0.2f;
            lookAngle += (mouseXInput * lookSpeed) /delta;
            pivotAngle -=(mouseYInput *pivotSpeed)/delta;
            pivotAngle=Mathf.Clamp(pivotAngle,minimumPivot,maximumPivot);

            Vector3 rotation =Vector3.zero;
            rotation.y=lookAngle;
            Quaternion targetRotation =Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation=Vector3.zero;
            rotation.x=pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraPivotTransform.localRotation = targetRotation;
        }
        else
        {
            float velocity =0;
            cameraSphereRadius=3.2f;
            Vector3 dir= currentLockOnTarget.position -transform.position;
            dir.Normalize();
            dir.y=-0.3f;

            Quaternion targetRotation= Quaternion.LookRotation(dir);
            transform.rotation=targetRotation;

            dir=currentLockOnTarget.position-cameraPivotTransform.position;
            dir.Normalize();

            targetRotation=Quaternion.LookRotation(dir);
            Vector3 eulerAngle=targetRotation.eulerAngles;
            eulerAngle.y=0;
            cameraPivotTransform.localEulerAngles=eulerAngle;
        }
        

    }

    private void HandleCameraCollisions(float delta)
    {
        targetPosition=defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();

        if(Physics.SphereCast(cameraPivotTransform.position,cameraSphereRadius, direction ,out hit, Mathf.Abs(targetPosition),ignoreLayers))
        {
            float dis= Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = -(dis-cameraCollisionOffset);
        }

        if(Mathf.Abs(targetPosition)< minimumCollisionOffset)
        {
            targetPosition =- minimumCollisionOffset;
        }

        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z,targetPosition, delta/0.2f);
        cameraTransform.localPosition =cameraTransformPosition;
    }

    public void HandleLockon()
    {   
        avilableTargets.Clear();
        float shortestDistance=Mathf.Infinity;
        float shortestDistanceOfLeftTarget = Mathf.Infinity;
        float shortestDistanceOfRightTarget = Mathf.Infinity;
        RaycastHit hit;
        Collider[] colliders =Physics.OverlapSphere(targetTransform.position, 26);

        for (int i = 0; i < colliders.Length; i++)
        {
            characterManager character=colliders[i].GetComponent<characterManager>();
            if(character !=null)
            {
                Vector3 lockTargetDirection =character.transform.position - targetTransform.position;
                float distanceFromTarget= Vector3.Distance(targetTransform.position, character.transform.position);
                float viewableAngle =Vector3.Angle(lockTargetDirection,cameraTransform.forward);

                if(character.transform.root != targetTransform.transform.root && viewableAngle >-50 &&viewableAngle < 50 && distanceFromTarget <= maximumLockonDistance)
                {   
                    if(Physics.Linecast(playerManager.lockOnTransform.position, character.lockOnTransform.position, out hit))
                    {   
                        Debug.DrawLine(playerManager.lockOnTransform.position, character.lockOnTransform.position);
                        if(hit.transform.gameObject.layer==environmentLayer)
                        {
                            Debug.Log("can not reach the target");
                        }
                        else
                        {
                            avilableTargets.Add(character);
                        }
                    }
                    
                }
            }
        }

        for (int k = 0; k <avilableTargets.Count; k++)
        {
            float distanceFromTarget =Vector3.Distance (targetTransform.position,avilableTargets[k].transform.position);

            if(distanceFromTarget<shortestDistance)
            {
                shortestDistance=distanceFromTarget;
                nearestLockonTarget=avilableTargets[k].lockOnTransform;
            }

            if(inputHandler.lockonFlag)
            {
                Vector3 relativeEnemyPosition=currentLockOnTarget.InverseTransformPoint(avilableTargets[k].transform.position);
                var distanceFromLeftTarget=currentLockOnTarget.transform.position.x-avilableTargets[k].transform.position.x;
                var distanceFromRightTarget=currentLockOnTarget.transform.position.x+avilableTargets[k].transform.position.x;

                if(relativeEnemyPosition.x>0.00 && distanceFromLeftTarget < shortestDistanceOfLeftTarget)
                {
                    shortestDistanceOfLeftTarget=distanceFromTarget;
                    leftLockTarget=avilableTargets[k].lockOnTransform;
                }
                
                if(relativeEnemyPosition.x< 0.00 && distanceFromRightTarget < shortestDistanceOfRightTarget)
                {
                    shortestDistanceOfRightTarget=distanceFromRightTarget;
                    rightLockTarget= avilableTargets[k].lockOnTransform;
                }
            }
        }
    }

    public void ClearLockOnTargets()
    {
        avilableTargets.Clear();
        nearestLockonTarget=null;
        currentLockOnTarget=null;
    }
}
