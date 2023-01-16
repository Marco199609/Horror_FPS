using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private MainInput _mainInput;

    public bool playerJumpInput { get; private set; }
    public bool playerRunInput { get; private set; }
    public bool itemPickupInput { get; private set; }
    public bool FlashLightInput { get; private set; }
    public float MouseScrollInput { get; private set; }
    public Vector2 playerMovementInput { get; private set; }
    public Vector2 mouseMovementInput { get; private set; }

    //Variables for smoothing player move
    private float smoothTime = 0.1f;
    private Vector2 currentVelocity;

    //Variables for smoothing player rotate
    private Vector2 currentMouseVelocity;

    private void Awake()
    {
        ObjectManager.Instance.PlayerInput = this;


        //New input manager
        _mainInput = new MainInput();
        _mainInput.Player.Enable();

    }


    private void Update()
    {/*
        playerJumpInput = Input.GetButtonDown("Jump");
        playerRunInput = Input.GetKey(KeyCode.LeftShift);
        itemPickupInput = Input.GetMouseButtonDown(0);
        FlashLightInput = Input.GetKey(KeyCode.F);
        MouseScrollInput = Input.GetAxisRaw("Mouse ScrollWheel");
        playerMovementInput = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        mouseMovementInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")); */

        NewInputSystem();

        //Input system bug fixes
        SmoothPlayerMovement();
        SmoothPlayerRotate();
        MouseScrollFix();
    }

    private void NewInputSystem()
    {
        playerJumpInput = _mainInput.Player.Jump.inProgress;
        playerRunInput = _mainInput.Player.Run.inProgress;
        itemPickupInput = _mainInput.Player.ItemPickup.inProgress;
        FlashLightInput = _mainInput.Player.Flashlight.inProgress;
    }

    //Smoothens player WSAD input
    private void SmoothPlayerMovement()
    {
        Vector2 movementInput = _mainInput.Player.Move.ReadValue<Vector2>(); ;
        playerMovementInput = Vector2.SmoothDamp(playerMovementInput, movementInput, ref currentVelocity, smoothTime);
    }
    private void SmoothPlayerRotate()
    {
        mouseMovementInput = _mainInput.Player.Rotate.ReadValue<Vector2>()*Time.smoothDeltaTime;
        //mouseMovementInput = Vector2.SmoothDamp(mouseMovementInput, mouseInput, ref currentMouseVelocity, 0.15f);
    }

    private void MouseScrollFix()
    {
        MouseScrollInput = _mainInput.Player.Scroll.ReadValue<float>();
        MouseScrollInput = Mathf.Abs(MouseScrollInput) > 1 ? MouseScrollInput / 120f : MouseScrollInput; //Fixes windows 120 scroll bug
    }
}
