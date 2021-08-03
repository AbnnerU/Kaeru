using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DefaltMovement : MonoBehaviour
{
    [Header("Basics")]
    [SerializeField] protected Rigidbody rb;

    [SerializeField] protected Transform cam;

    [SerializeField] protected InputController inputController;

    [Header("Movement")]
    [SerializeField] protected float aceleration=100;

    [SerializeField] protected float maxSpeed=10;

    [SerializeField] protected float turnSpeed=250;


    protected Vector3 moveInput;

    protected float smoothTurnVelocity;

    protected float defaltMaxSpeed;

    protected float defaltAceleration;

    protected float defaltUpForce;

    protected bool movementActive;

    protected virtual void OnEnable()
    {
        movementActive = true;
    }

    protected virtual void OnDisable()
    {
        movementActive = false;
    }

    protected virtual void Start()
    {
        inputController.OnMoveEvent += InputController_OnMoveEvent;       
    }

    protected abstract void FixedUpdate();

    protected abstract void InputController_OnMoveEvent(Vector2 moveInput);

    public virtual void SetNewValues(float newMaxSpeed, float newAceleration, float newUpForce)
    {
        maxSpeed = newMaxSpeed;

        aceleration = newAceleration;
    }

    public virtual void SetDefaltValues()
    {
        maxSpeed = defaltMaxSpeed;

        aceleration = defaltAceleration;
    }

    public bool GetMovementActive()
    {
        return movementActive;
    }
}
