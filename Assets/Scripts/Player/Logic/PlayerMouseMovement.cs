using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMovement : MonoBehaviour
{
    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    private float minimumY = -60F;
    private float maximumY = 60F;

    float rotationY = 0F;

    void Start()
    {
        //Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    public void MouseLook(PlayerData playerData, Transform mainCamera, PlayerInput playerInput)
    {
        if (axes == RotationAxes.MouseXAndY)
        {
            float rotationX = transform.localEulerAngles.y + playerInput.mouseMovementInput.x * playerData.mouseSensitivityX;

            rotationY += playerInput.mouseMovementInput.y * playerData.mouseSensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(0, rotationX, 0);
            mainCamera.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }
        else if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, playerInput.mouseMovementInput.x * playerData.mouseSensitivityX, 0);
        }
        else
        {
            rotationY += playerInput.mouseMovementInput.y * playerData.mouseSensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            mainCamera.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }
}
