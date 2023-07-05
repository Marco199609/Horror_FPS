using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IPlayerInput
{
    bool FlashLightInput { get; }
    Vector2 mouseMovementInput { get; }
    Vector2 UnsmoothedMouseMovementInput { get; }
    float MouseScrollInput { get; }
    bool playerJumpInput { get; }
    Vector2 playerMovementInput { get; }
    Vector2 UnsmoothedPlayerMovementInput { get; }
    bool playerPickupInput { get; }
    bool playerRunInput { get; }
}


public class PlayerInput : MonoBehaviour, IPlayerInput
{
    private MainInput _mainInput;
    public bool playerJumpInput { get; private set; }
    public bool playerRunInput { get; private set; }
    public bool playerPickupInput { get; private set; }
    public bool FlashLightInput { get; private set; }
    public float MouseScrollInput { get; private set; }
    public Vector2 playerMovementInput { get; private set; }
    public Vector2 UnsmoothedPlayerMovementInput { get; private set; }
    public Vector2 mouseMovementInput { get; private set; }
    public Vector2 UnsmoothedMouseMovementInput { get; private set; }

    //Variables for smoothing player move
    private float smoothTime = 0.1f;
    private Vector2 currentVelocity;

    //Variables for smoothing player rotate
    private Vector2 currentMouseVelocity;

    private void Awake()
    {
        //New input manager
        _mainInput = new MainInput();
        _mainInput.Player.Enable();

    }


    private void Update()
    {
        InputSystem();

        //Input system bug fixes
        SmoothPlayerMovement();
        SmoothPlayerRotate();
        MouseScrollFix();
    }

    private void InputSystem()
    {
        playerJumpInput = _mainInput.Player.Jump.inProgress;
        playerRunInput = _mainInput.Player.Run.inProgress;
        playerPickupInput = _mainInput.Player.ItemPickup.triggered;
        FlashLightInput = _mainInput.Player.Flashlight.inProgress;
    }

    //Smoothens player WSAD input
    private void SmoothPlayerMovement()
    {
        UnsmoothedPlayerMovementInput = _mainInput.Player.Move.ReadValue<Vector2>(); ;
        playerMovementInput = Vector2.SmoothDamp(playerMovementInput, UnsmoothedPlayerMovementInput, ref currentVelocity, smoothTime);
    }
    private void SmoothPlayerRotate()
    {
        UnsmoothedMouseMovementInput = _mainInput.Player.Rotate.ReadValue<Vector2>();
        mouseMovementInput = Vector2.SmoothDamp(mouseMovementInput, UnsmoothedMouseMovementInput, ref currentMouseVelocity, smoothTime);
    }

    private void MouseScrollFix()
    {
        MouseScrollInput = _mainInput.Player.Scroll.ReadValue<float>();
        MouseScrollInput = Mathf.Abs(MouseScrollInput) > 1 ? MouseScrollInput / 120f : MouseScrollInput; //Fixes windows 120 scroll bug
    }
}
