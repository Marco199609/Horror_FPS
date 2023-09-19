using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedModifier : MonoBehaviour
{
    private float _playerDefaultWalkSpeed;
    private float _playerDefaultFootstepTime;

    private bool _increasePlayerSpeed;
    private bool _lowerPlayerSpeed;

    private PlayerData _playerData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerData = PlayerController.Instance.PlayerData;

            if (_playerDefaultWalkSpeed == 0) _playerDefaultWalkSpeed = _playerData.walkSpeed;
            if (_playerDefaultFootstepTime == 0) _playerDefaultFootstepTime = _playerData.FootstepWalkingTime;

            _lowerPlayerSpeed = false;
            _increasePlayerSpeed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _lowerPlayerSpeed = true;
            _increasePlayerSpeed = false;
        }
    }

    private void Update()
    {
        if(_increasePlayerSpeed)
        {
            _playerData.walkSpeed = Mathf.Lerp(_playerData.walkSpeed, _playerDefaultWalkSpeed * 2, Time.deltaTime);
            _playerData.runSpeed = Mathf.Lerp(_playerData.walkSpeed, _playerDefaultWalkSpeed * 2, Time.deltaTime);
            _playerData.FootstepWalkingTime = Mathf.Lerp(_playerData.FootstepWalkingTime, _playerDefaultFootstepTime * 0.65f, Time.deltaTime);
            _playerData.FootstepsRunningTime = Mathf.Lerp(_playerData.FootstepsRunningTime, _playerDefaultFootstepTime * 0.65f, Time.deltaTime);
        }

        if(_lowerPlayerSpeed)
        {
            _playerData.walkSpeed = Mathf.Lerp(_playerData.walkSpeed, _playerDefaultWalkSpeed, Time.deltaTime * 3);
            _playerData.runSpeed = Mathf.Lerp(_playerData.walkSpeed, _playerDefaultWalkSpeed, Time.deltaTime * 3);
            _playerData.FootstepWalkingTime = Mathf.Lerp(_playerData.FootstepWalkingTime, _playerDefaultFootstepTime, Time.deltaTime * 3);
            _playerData.FootstepsRunningTime = Mathf.Lerp(_playerData.FootstepsRunningTime, _playerDefaultFootstepTime, Time.deltaTime * 3);
        }
    }
}
