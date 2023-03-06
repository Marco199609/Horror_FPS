using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour, IPlayerRotate
{

    private float _rotationX = 0F;
    private float _rotationY = 0F;
    private float _minimumY = -89F;
    private float _maximumY = 89F;

    private PlayerData _playerData;
    private Transform _camHolder;

    private void Awake()
    {
        _playerData = FindObjectOfType<PlayerData>();

        _rotationX = _playerData.gameObject.transform.localEulerAngles.y;
        _rotationY = _playerData.camHolder.transform.localEulerAngles.x;
    }

    public void RotatePlayer(GameObject player, IPlayerInput playerInput)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();
        if (_camHolder == null) _camHolder = _playerData.camHolder;

        _rotationX = player.transform.localEulerAngles.y + playerInput.mouseMovementInput.x * _playerData.mouseSensitivityX * Time.deltaTime;

        _rotationY += playerInput.mouseMovementInput.y * _playerData.mouseSensitivityY * Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, _minimumY, _maximumY);

        player.transform.eulerAngles = Vector3.Lerp(player.transform.eulerAngles, new Vector3(0, _rotationX, 0), 60f * Time.deltaTime);
        //_camHolder.localEulerAngles = Vector3.Lerp(_camHolder.localEulerAngles, new Vector3(-_rotationY, 0, 0), 200f * Time.deltaTime);

        /*Previous implementation
        player.transform.localEulerAngles = new Vector3(0, rotationX, 0);*/
        _playerData.camHolder.localEulerAngles = new Vector3(-_rotationY, 0, 0);
    }
}
