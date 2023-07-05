using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICameraControl
{
    void ControlCameraHeadBob(GameObject player);
}

public class PlayerCameraControl : MonoBehaviour, ICameraControl
{

    private bool _enable = true;
    private bool _isRunning;
    private bool _isStartPosAlreadySet;

    private float _toggleSpeed = 3.0f;

    private Vector3 _startPos;

    private PlayerData _playerData;
    public void ControlCameraHeadBob(GameObject player)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        if (!_isStartPosAlreadySet)
        {
            _startPos = _playerData.Camera.localPosition;
            _isStartPosAlreadySet = true;
        }

        if (!_enable) return;

        //CheckMotion();
        //ResetPosition();
        //_playerData.Camera.LookAt(FocusTarget(player));
    }


    private void PlayMotion(Vector3 motion)
    {
        _playerData.CinemachineLookAt.localPosition += motion;
        _playerData.camHolder.localPosition += motion;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(_playerData.characterController.velocity.x, 0, _playerData.characterController.velocity.z).magnitude;

        if (speed < _toggleSpeed) return;
        if (!_playerData.characterController.isGrounded) return;

        if (speed > _playerData.walkSpeed + 1) _isRunning = true;
        else _isRunning = false;

        PlayMotion(FootStepMotion(_isRunning));
    }

    private Vector3 FootStepMotion(bool isRunning)
    {
        Vector3 pos = Vector3.zero;

        if (!isRunning)
        {
            pos.y += Mathf.Sin(Time.time * _playerData.camMovementFrequency) * _playerData.camMovementAmplitude * 2;
            pos.x += Mathf.Cos(Time.time * _playerData.camMovementFrequency / 2) * _playerData.camMovementAmplitude;
        }
        else
        {
            float runMovementAmplitude = _playerData.camMovementAmplitude * (_playerData.runSpeed / _playerData.walkSpeed);

            pos.y += Mathf.Sin(Time.time * _playerData.camMovementFrequency * (_playerData.runSpeed / _playerData.walkSpeed))
                * runMovementAmplitude * 2;
            pos.x += Mathf.Cos(Time.time * _playerData.camMovementFrequency / 2 * (_playerData.runSpeed / _playerData.walkSpeed))
                * runMovementAmplitude;
        }

        return pos;
    }

    private void ResetPosition()
    {
        if (_playerData.Camera.localPosition == _startPos) return;
        _playerData.Camera.localPosition = Vector3.Lerp(_playerData.Camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget(GameObject player)
    {
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + _playerData.camHolder.localPosition.y, player.transform.position.z);
        pos += _playerData.camHolder.forward * 15.0f;
        return pos;
    }
}