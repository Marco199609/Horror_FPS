using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseMovement : MonoBehaviour
{
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MouseMove(PlayerData playerData, Transform mainCamera, PlayerInput playerInput)
    {
        float MouseX = playerInput.mouseMovementInput.x * playerData.mouseSensitivity * Time.deltaTime;
        float MouseY = playerInput.mouseMovementInput.y * playerData.mouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * MouseX);
    }
}
