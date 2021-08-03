using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : DefaltMovement
{

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GroundCheck groundCheck;

    [Header("Sloped Surface")]

    [SerializeField] private Transform raycastPositionReference;

    [SerializeField] private float rayCastSize;

    [SerializeField] private float slopingGroundForce;

    [Header("Steps")]
    [SerializeField] private Transform stepMaxSize;

    [SerializeField] private Transform stepMinSize;

    [SerializeField] private float stepRayCastSizeMin;

    [SerializeField] private float stepRayCastSizeMax;

    [SerializeField] private float stepSmooth;

    [Header("Drag")]

    [SerializeField] private float onGroundDrag;

    [SerializeField] private float onAirDrag;

    [Header("On Air Control")]
    [SerializeField] private float onAirControlSpeed;

    [Header("Jump")]

    [SerializeField]private float jumpForce;

    [SerializeField] private float gravityForce;

    public Action<bool> OnGroundEvent;

    public Action<float> OnMoveEvent;

    public Action<bool> OnJumpEvent;

    public Action<bool> OnFallEvent;

    public enum PlayerState { OnGround, OnAir , Jump/*, DoubleJump, Jump*/}

    private PlayerState playerState;

    private bool lastGrondedValue;

    private bool onSlopedSurface;

    #region AdventureModeCamera
    protected float rigthAngle;

    protected float currentRigtInput;

    protected bool rigthRotation;
    #endregion



    protected override void Start()
    {
        base.Start();

        inputController.OnJumpEvent += InputController_OnJumpEvent;

        lastGrondedValue = true;
    }

    protected override void FixedUpdate()
    {
        if (movementActive)
        {
           
            if (groundCheck.GetGrounded())
            {
                playerState = PlayerState.OnGround;
            }
            else
            {
                playerState = PlayerState.OnAir;
            }


            if (playerState == PlayerState.OnGround)
            {
                if (lastGrondedValue == false)
                {
                    lastGrondedValue = true;

                    OnGroundEvent?.Invoke(true);

                    OnFallEvent?.Invoke(false);
                }

                rb.drag = onGroundDrag;
             
                //Inputs movement
                 if (moveInput.magnitude > 0)
                 {
                    //Rotation
                    Vector3 moveDirection = Vector3.forward * moveInput.y + Vector3.right * moveInput.x;

                    //if (moveInput.y != 0)
                    //{
                        //rigthRotation = false;

                        //currentRigtInput = 0;

                        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cam.localEulerAngles.y;

                        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, turnSpeed);

                        transform.rotation = Quaternion.Euler(transform.rotation.x, angle, transform.rotation.z);
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

                    // Sloped Surface
                    RaycastHit hit;

                    if (Physics.Raycast(raycastPositionReference.position, -transform.up, out hit, rayCastSize, groundLayer))
                    {
                       
                        if (hit.normal != Vector3.up && rb.velocity.y < 0)
                        {
                            onSlopedSurface = true;
                            //print("Sloped Surface");
                            rb.AddForce(Vector3.up * -slopingGroundForce);
                        }
                        else
                        {
                            onSlopedSurface = false;
                        }
                    }
                    else
                    {
                        onSlopedSurface = false;
                    }

                    if (hit.normal == Vector3.up)
                    {
                        Steps();
                    }

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

                OnMoveEvent?.Invoke(moveInput.magnitude);
               
            }
            else
            {
                if (lastGrondedValue == true)
                {
                    lastGrondedValue = false;

                    OnGroundEvent?.Invoke(false);
                }

                rb.drag = onAirDrag;

                if (rb.velocity.y < 3)
                {
                    //print("Gravity");
                    rb.AddForce(Vector3.up * -gravityForce);

                    OnFallEvent?.Invoke(true);
                }

                //Air control

                Vector3 cameraProjection = Vector3.ProjectOnPlane(cam.forward, Vector3.up);
                float dot = Vector3.Dot(transform.forward, cameraProjection);
                if (dot < -0.5f) //Looking to camera
                {
                    if (moveInput.y > 0)
                    {
                        rb.AddForce((Vector3.ProjectOnPlane(cam.forward, Vector3.up) * (moveInput.y * onAirControlSpeed)) + (-transform.right * (moveInput.x * onAirControlSpeed)));
                    }
                    else
                    {
                        rb.AddForce(-transform.right * (moveInput.x * onAirControlSpeed));
                    }
                }
                else if (dot > 0.5f) //Oposte to camera
                {
                    if (moveInput.y < 0)
                    {
                        rb.AddForce((transform.forward * (moveInput.y * onAirControlSpeed)) + (transform.right * (moveInput.x * onAirControlSpeed)));
                    }
                    else
                    {
                        rb.AddForce(transform.right * (moveInput.x * onAirControlSpeed));
                    }
                }

            }
        }

    }

    private void Steps()
    {                  
            RaycastHit stepDetection;
            if (Physics.Raycast(stepMinSize.position, transform.TransformDirection(1.5f, 0, 1), out stepDetection, stepRayCastSizeMin, groundLayer))
            {
                 RaycastHit stepDetectionUpper;
                 if (!Physics.Raycast(stepMaxSize.position, transform.TransformDirection(1.5f, 0, 1), out stepDetectionUpper, stepRayCastSizeMax, groundLayer))
                 {
                     rb.position += new Vector3(0, stepSmooth, 0);
                     return;
                 }

            }

             RaycastHit stepDetectionTwo;
             if (Physics.Raycast(stepMinSize.position, transform.TransformDirection(-1.5f, 0, 1), out stepDetectionTwo, stepRayCastSizeMin, groundLayer))
             {
                 RaycastHit stepDetectionUpper;
                 if (!Physics.Raycast(stepMaxSize.position, transform.TransformDirection(-1.5f, 0, 1), out stepDetectionUpper, stepRayCastSizeMax, groundLayer))
                 {
                      rb.position += new Vector3(0, stepSmooth, 0);
                      return;
                 }
             }

            RaycastHit stepDetectionMiddle;
            if (Physics.Raycast(stepMinSize.position, transform.TransformDirection(0, 0, 1), out stepDetectionMiddle, stepRayCastSizeMin, groundLayer))
            {
                 RaycastHit stepDetectionUpper;
                 if (!Physics.Raycast(stepMaxSize.position, transform.TransformDirection(0, 0, 1), out stepDetectionUpper, stepRayCastSizeMax, groundLayer))
                 {
                      rb.position += new Vector3(0, stepSmooth, 0);
                      return;
                  }
            }

    }

    private void InputController_OnJumpEvent(float jumpInput)
    {
        if (jumpInput > 0 && movementActive)
        {
            if (playerState == PlayerState.OnGround)
            {
                
                if (onSlopedSurface)
                {
                    rb.AddForce(Vector3.up * (jumpForce + (slopingGroundForce * 2) + gravityForce));
                }
                else
                {
                    rb.AddForce(Vector3.up * jumpForce);
                }

                OnJumpEvent?.Invoke(true);

                playerState = PlayerState.OnAir;
            }
        }
    }

    protected override void InputController_OnMoveEvent(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }

    private void OnDrawGizmos()
    {
        //Ground detection
        Gizmos.color = Color.red;

        Gizmos.DrawLine(raycastPositionReference.position, new Vector3(raycastPositionReference.position.x, raycastPositionReference.position.y - rayCastSize, raycastPositionReference.position.z));

        //Steps detection
        Gizmos.DrawLine(stepMinSize.position, stepMinSize.position + (new Vector3(1.5f, 0, 1) * stepRayCastSizeMin));
        Gizmos.DrawLine(stepMinSize.position, stepMinSize.position + (new Vector3(-1.5f, 0, 1) * stepRayCastSizeMin));
        Gizmos.DrawLine(stepMinSize.position, stepMinSize.position + (new Vector3(0, 0, 1) * stepRayCastSizeMin));


        Gizmos.DrawLine(stepMaxSize.position, stepMaxSize.position+(new Vector3(1.5f, 0, 1) * stepRayCastSizeMax));
        Gizmos.DrawLine(stepMaxSize.position, stepMaxSize.position+(new Vector3(-1.5f, 0, 1) * stepRayCastSizeMax));
        Gizmos.DrawLine(stepMaxSize.position, stepMaxSize.position + (new Vector3(0, 0, 1) * stepRayCastSizeMax));
    }


    #region AdventureModeCamera
    //if (moveInput.magnitude > 0)
    //{
    //    if (moveInput.y != 0)
    //    {
    //        float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + cam.eulerAngles.y;

    //        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothTurnVelocity, turnSpeed);

    //        transform.rotation = Quaternion.Euler(0, angle, 0);

    //        rigthRotation = false;

    //        currentRigtInput = 0;
    //    }
    //    else
    //    {
    //        if (rigthRotation == false || currentRigtInput != moveInput.x)
    //        {
    //            rigthRotation = true;

    //            rigthAngle = transform.eulerAngles.y + (moveInput.x * 90 - currentRigtInput * 90);

    //            //0                 //-1
    //            currentRigtInput = moveInput.x;



    //        }
    //        //float targetAngle = moveInput.x * 90 + transform.eulerAngles.y;

    //        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rigthAngle, ref smoothTurnVelocity, turnSpeed);

    //        transform.rotation = Quaternion.Euler(0, angle, 0);
    //    }


    //    rb.AddForce(transform.forward * aceleration);
    //}
    #endregion
}
