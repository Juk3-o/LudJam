using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;

    [NonSerialized] public bool CanUseInput = true;

    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool MoveInput { get; private set; }
    public bool GetUp { get; private set; }

    public bool PauseInput;

    [SerializeField] private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MoveInput = true;
        }

        if (context.canceled)
        {
            MoveInput = false;
        }

        RawMovementInput = context.ReadValue<Vector2>();

        if (CanUseInput)
        {
            NormInputX = Mathf.RoundToInt(RawMovementInput.x);
            NormInputY = Mathf.RoundToInt(RawMovementInput.y);

        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            GetUp = true;
       
        }
        if (context.canceled)
        {
            GetUp = false;
        }

        if (CanUseInput)
        {

            if (context.started)
            {

                JumpInput = true;
                JumpInputStop = false;
                jumpInputStartTime = Time.time;
            }
            if (context.canceled)
            {
                JumpInputStop = true;
            }
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (CanUseInput)
        {

            if (context.started)
            {
                DashInput = true;
                DashInputStop = false;

                dashInputStartTime = Time.time;

            }
            else if (context.canceled)
            {

                DashInputStop = true;

            }
        }
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PauseInput = true;
            Debug.Log("pause");
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void UseDashInput() => DashInput = false;


    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }

    }
}






