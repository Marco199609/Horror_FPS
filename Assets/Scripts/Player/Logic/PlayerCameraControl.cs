using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{

    private bool _enable = true;
    private bool isRunning;
    private bool isStartPosAlreadySet;
    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;

    public void ControlCamera(GameObject player, PlayerData playerData)
    {
        if(!isStartPosAlreadySet)
        {
            _startPos = playerData.camTransform.localPosition;
            isStartPosAlreadySet = true;
        }

        if (!_enable) return;

        CheckMotion(playerData);
        ResetPosition(playerData);
        playerData.camTransform.LookAt(FocusTarget(player, playerData));
    }


    private void PlayMotion(Vector3 motion, PlayerData playerData)
    {
        playerData.camTransform.localPosition += motion;
    }

    private void CheckMotion(PlayerData playerData)
    {
        float speed = new Vector3(playerData.characterController.velocity.x, 0, playerData.characterController.velocity.z).magnitude;

        if (speed < _toggleSpeed) return;
        if (!playerData.characterController.isGrounded) return;

        if (speed > playerData.walkSpeed + 1)
            isRunning = true;
        else
            isRunning = false;

        PlayMotion(FootStepMotion(isRunning, playerData), playerData);
    }



    private Vector3 FootStepMotion(bool isRunning, PlayerData playerData)
    {
        Vector3 pos = Vector3.zero;

        if(!isRunning)
        {
            pos.y += Mathf.Sin(Time.time * playerData.camMovementFrequency) * playerData.camMovementAmplitude;
            pos.x += Mathf.Cos(Time.time * playerData.camMovementFrequency / 2) * playerData.camMovementAmplitude * 2;
        }
        else
        {
            pos.y += Mathf.Sin(Time.time * playerData.camMovementFrequency * (playerData.runSpeed / playerData.walkSpeed))
                * playerData.camMovementAmplitude;
            pos.x += Mathf.Cos(Time.time * playerData.camMovementFrequency / 2 * (playerData.runSpeed / playerData.walkSpeed))
                * playerData.camMovementAmplitude * 2;
        }

        return pos;
    }



    private void ResetPosition(PlayerData playerData)
    {
        if (playerData.camTransform.localPosition == _startPos) return;
        playerData.camTransform.localPosition = Vector3.Lerp(playerData.camTransform.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget(GameObject player, PlayerData playerData)
    {
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y + playerData.camHolder.localPosition.y, player.transform.position.z);
        pos += playerData.camHolder.forward * 15.0f;
        return pos;
    }
}