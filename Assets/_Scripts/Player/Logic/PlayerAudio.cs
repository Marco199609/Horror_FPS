using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour, IPlayerAudio
{
    private float _playerWalkingTimer, _playerRunningTimer;
    public void Footsteps(PlayerData playerData, IPlayerInput playerInput)
    {
        if (playerInput.playerMovementInput != Vector2.zero)
        {
            if (playerInput.playerRunInput)
            {
                if (_playerRunningTimer <= 0)
                {
                    int i = Random.Range(0, playerData.Footsteps.Length);
                    playerData.PlayerAudioSource.PlayOneShot(playerData.Footsteps[i], 0.3f);
                    _playerRunningTimer = playerData.FootstepsRunningTime;
                }
                _playerRunningTimer -= Time.deltaTime;
            }
            else
            {
                if (_playerWalkingTimer <= 0)
                {
                    int i = Random.Range(0, playerData.Footsteps.Length);
                    playerData.PlayerAudioSource.PlayOneShot(playerData.Footsteps[i], 0.3f);
                    _playerWalkingTimer = playerData.FootstepWalkingTime;
                }
                _playerWalkingTimer -= Time.deltaTime;
            }
        }
    }
}