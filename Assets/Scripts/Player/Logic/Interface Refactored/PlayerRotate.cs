using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour, IPlayerRotate
{
    private float _minimumY = -60F;
    private float _maximumY = 60F;
    private float _rotationY = 0F;

    private PlayerData _playerData;

    public void RotatePlayer(GameObject player, PlayerInput playerInput)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        float rotationX = player.transform.localEulerAngles.y + playerInput.mouseMovementInput.x * _playerData.mouseSensitivityX;

        _rotationY += playerInput.mouseMovementInput.y * _playerData.mouseSensitivityY;
        _rotationY = Mathf.Clamp(_rotationY, _minimumY, _maximumY);

        player.transform.localEulerAngles = new Vector3(0, rotationX, 0);
        _playerData.camHolder.localEulerAngles = new Vector3(-_rotationY, 0, 0);


    }
}
