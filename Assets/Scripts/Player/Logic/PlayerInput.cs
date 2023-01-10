using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool playerJumpInput { get; private set; }
    public bool playerRunInput { get; private set; }
    public Vector2 playerMovementInput { get; private set; }
    public Vector2 mouseMovementInput { get; private set; }


    private void Update()
    {
        playerJumpInput = Input.GetButtonDown("Jump");
        playerRunInput = Input.GetKey(KeyCode.LeftShift);
        playerMovementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        mouseMovementInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
