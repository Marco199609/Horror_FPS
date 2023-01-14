using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private float minimumY = -60F;
    private float maximumY = 60F;

    float rotationY = 0F;

    public void MouseLook(GameObject player, PlayerData playerData, PlayerInput playerInput)
    {
        float rotationX = player.transform.localEulerAngles.y + playerInput.mouseMovementInput.x * playerData.mouseSensitivityX;

        rotationY += playerInput.mouseMovementInput.y * playerData.mouseSensitivityY;
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

        player.transform.localEulerAngles = new Vector3(0, rotationX, 0);
        playerData.camHolder.localEulerAngles = new Vector3(-rotationY, 0, 0);


    }
}
