using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : DefaltMovement
{
    [Header("Aiming")]
    [SerializeField] private float aimigTurnSpeed;

    [Header("Fly")]
    [SerializeField] private float flyForce;  

    private float flyingValue;

    private bool defaltMovement;

    #region AdventureModeCamera
    protected float rigthAngle;

    protected float currentRigtInput;

    protected bool rigthRotation;
    #endregion


    protected override void Start()
    {
        base.Start();
        inputController.OnFlyEvent += InputController_OnFlyEvent;

        inputController.OnDonwEvent += InputController_OnDownEvent;

        defaltMaxSpeed = maxSpeed;

        defaltAceleration = aceleration;

        defaltUpForce = flyForce;

        flyingValue = 0;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        flyingValue = 0;

        defaltMovement = true;
    }

    protected override void FixedUpdate()
    {
        if (defaltMovement == false)
        {
            //Rotation
            Vector3 moveDirection = Vector3.forward * moveInput.y + Vector3.right * moveInput.x;

            Vector3 cameraProjectionForward = Vector3.ProjectOnPlane(cam.forward, Vector3.up);

            Quaternion rotationToCamera = Quaternion.LookRotation(cameraProjectionForward);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationToCamera, aimigTurnSpeed * Time.deltaTime);

            //Movement
            if (rb.velocity.magnitude < maxSpeed)
            {
                if (moveInput.magnitude > 0)
                {
                    moveDirection = rotationToCamera * moveDirection;

                    rb.AddForce(moveDirection * aceleration);
                }
            }


            //Fly      
            rb.AddForce(Vector3.up * (flyForce * flyingValue));

        }
        else
        {
            if (moveInput.magnitude > 0)
            {
                //Rotation
                Vector3 moveDirection = Vector3.forward * moveInput.y + Vector3.right * moveInput.x;

                //if (moveInput.y != 0)
                //{
                    //rigthRotation = false;

                    //currentRigtInput = 0;

                    float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.localEulerAngles.y;

                    float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, targetAngle, ref smoothTurnVelocity, turnSpeed);

                    transform.localRotation = Quaternion.Euler(0, angle, 0);          
                //}
                //else
                //{
                  
                //    if (rigthRotation == false || currentRigtInput != moveInput.x)
                //    {
                //        rigthRotation = true;

                //        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.localEulerAngles.y;

                //        rigthAngle = targetAngle;

                //        currentRigtInput = moveInput.x;
                //    }

                //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rigthAngle, ref smoothTurnVelocity, turnSpeed);

                //    transform.rotation = Quaternion.Euler(0, angle, 0);
                //}

                //Movement
                if (rb.velocity.magnitude < maxSpeed)
                {
                    rb.AddForce(aceleration * transform.forward);
                }



            }
            else
            {
                rigthRotation = false;

                currentRigtInput = 0;
            }

            //Fly      
            rb.AddForce(Vector3.up * (flyForce * flyingValue));

        }
    }

    protected override void InputController_OnMoveEvent(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }

    private void InputController_OnFlyEvent(float jumpInput)
    {
        if (jumpInput > 0)
        {
            flyingValue = 1;
        }
        else
        {
            flyingValue = 0;
        }

    }

    private void InputController_OnDownEvent(float downInput)
    {
        if (downInput > 0)
        {
            flyingValue = -1;
        }
        else
        {
            flyingValue = 0;
        }
    }


    public override void SetNewValues(float newMaxSpeed, float newAceleration, float newUpForce)
    {
        base.SetNewValues(newMaxSpeed, newAceleration, newUpForce);

        flyForce = newUpForce;
    }

    public override void SetDefaltValues()
    {
        base.SetDefaltValues();

        flyForce = defaltUpForce;
    }

    public void SetDefaltMomevemt(bool value)
    {
        defaltMovement = value;
    }


}
