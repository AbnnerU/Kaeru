using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private InputActionsImpediments inputActionsImpediments;

    private Controls controls;

    public Action<Vector2> OnMoveEvent;

    public Action<float> OnJumpEvent;

    public Action<float> OnFlyEvent;

    public Action<float> OnDonwEvent;

    public Action OnSwitchEvent;

    public Action OnInteractEvent;

    public Action<float> OnAimEvent;

    public Action<float> OnShootEvent;

    public Action OnPauseEvent;

    public Action OnCheatEvent;

    private void Awake()
    {
        controls = new Controls();

        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();

        //Perfomed
        controls.Gameplay.Movement.performed += ctx => Movement_input(ctx.ReadValue<Vector2>());

        controls.Gameplay.Jump.performed += ctx => Jump_input(ctx.ReadValue<float>());

        controls.Gameplay.Fly.performed += ctx => Fly_input(ctx.ReadValue<float>());

        controls.Gameplay.Down.performed += ctx => Down_input(ctx.ReadValue<float>());

        controls.Gameplay.SwitchCaracter.performed += ctx => Switch_input();

        controls.Gameplay.Interact.performed += ctx => Interact_input();

        controls.Gameplay.Aim.performed += ctx => Aim_input(ctx.ReadValue<float>());

        controls.Gameplay.Shoot.performed += ctx => Shoot_input(ctx.ReadValue<float>());

        controls.Gameplay.Pause.performed += ctx => Pause_input();

        controls.Gameplay.Cheat.performed += ctx => Cheat_Input();

        //Canceled
        controls.Gameplay.Movement.canceled += ctx => Movement_input(ctx.ReadValue<Vector2>());

        controls.Gameplay.Jump.canceled += ctx => Jump_input(ctx.ReadValue<float>());

        controls.Gameplay.Fly.canceled += ctx => Fly_input(ctx.ReadValue<float>());

        controls.Gameplay.Down.canceled += ctx => Down_input(ctx.ReadValue<float>());

        //controls.Gameplay.SwitchCaracter.canceled += ctx => Switch_input(ctx.ReadValue<float>());

        //controls.Gameplay.Interact.canceled += ctx => Interact_input();

        controls.Gameplay.Aim.canceled += ctx => Aim_input(ctx.ReadValue<float>());

        controls.Gameplay.Shoot.canceled += ctx => Shoot_input(ctx.ReadValue<float>());
    }

    private void Cheat_Input()
    {
        //print(ctx);

        OnCheatEvent?.Invoke();
    }

    private void Movement_input(Vector2 ctx)
    {
        //print(ctx);

        OnMoveEvent?.Invoke(ctx);
    }

    private void Jump_input(float ctx)
    {
        //print(ctx);
        if (inputActionsImpediments.PlayerHoldingObject()==false)
        {
            OnJumpEvent?.Invoke(ctx);
        }
    }

    private void Fly_input(float ctx)
    {
        //print(ctx);

        OnFlyEvent?.Invoke(ctx);
    }

    private void Down_input(float ctx)
    {
        //print(ctx);

        OnDonwEvent?.Invoke(ctx);
    }

    private void Switch_input()
    {
        if (inputActionsImpediments != null)
        {
            if (inputActionsImpediments.GetPlayerGrounded() && inputActionsImpediments.PlayerHoldingObject() == false)
            {
                OnSwitchEvent?.Invoke();
            }
        }
    }

    private void Interact_input()
    {
        if (inputActionsImpediments != null)
        {
            if (inputActionsImpediments.GetPlayerGrounded())
            {
                OnInteractEvent?.Invoke();
            }
        }
        else
        {
            OnInteractEvent?.Invoke();
        }
    }

    private void Aim_input(float ctx)
    {
        OnAimEvent?.Invoke(ctx);
    }

    private void Shoot_input(float ctx)
    {
        OnShootEvent?.Invoke(ctx);
    }

    private void Pause_input()
    {
        OnPauseEvent?.Invoke();
    }

}
