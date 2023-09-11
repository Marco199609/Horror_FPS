using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public interface IPlayerAudio
{
    void PlayerAudioControl(PlayerData playerData, IPlayerInput playerInput);
}

public class PlayerAudio : MonoBehaviour, IPlayerAudio
{
    private int _currentHeartbeatIndex = 0, _currentBreathIndex = 0;
    private float _footStepTimer, _breathingTimer = 0, _heartbeatTimer = 0;
    private AudioSource _heartbeatSource, _breathSource, _footstepsSource;
    private SoundData _soundData;

    private void Start()
    {
        if(SoundManager.Instance != null)
        {
            _soundData = SoundManager.Instance.SoundData;
        }

        _heartbeatSource = SoundManager.Instance.CreateModifiableAudioSource(null, PlayerController.Instance.Player, 1);
        _heartbeatSource.reverbZoneMix = 0;
        _breathSource = SoundManager.Instance.CreateModifiableAudioSource(null, PlayerController.Instance.Player, 1);
        _footstepsSource = SoundManager.Instance.CreateModifiableAudioSource(null, PlayerController.Instance.Player, 1);
    }

    public void PlayerAudioControl(PlayerData playerData, IPlayerInput playerInput)
    {
        PlayerFootsteps(playerData, playerInput);
        PlayerHeartbeat(playerData);
        PlayerBreath(playerData);
    }


    private void PlayerFootsteps(PlayerData playerData, IPlayerInput playerInput)
    {

        if(playerInput.UnsmoothedPlayerMovementInput != Vector2.zero)
        {
            if(/*_playerBreathSource != null &&*/ _footStepTimer <= 0)
            {
                if(SceneManager.GetActiveScene().name == "Level_Dream")
                {
                    int i = Random.Range(0, _soundData.ConcreteFootstepClips.Length);
                    _footstepsSource.PlayOneShot(_soundData.ConcreteFootstepClips[i], _soundData.ConcreteFootstepClipsVolume);
                }
                else
                {
                    int i = Random.Range(0, _soundData.WoodFootstepClips.Length);
                    _footstepsSource.PlayOneShot(_soundData.WoodFootstepClips[i], _soundData.WoodFootstepClipsVolume);
                }

                if (playerInput.playerRunInput) _footStepTimer = playerData.FootstepsRunningTime;
                else _footStepTimer = playerData.FootstepWalkingTime;
            }
            _footStepTimer -= Time.deltaTime;
        }
        else _footStepTimer = 0;
    }

    private void PlayerHeartbeat(PlayerData playerData)
    {
        if (SceneManager.GetActiveScene().name == "Level_Dream")
        {
            if (_heartbeatTimer <= 0)
            {
                float currentStressLevel = PlayerController.Instance.StressControl.StressLevel();
                float volume = _soundData.PlayerHeartbeatClipsVolume * SoundManager.Instance.GlobalSoundFXVolume * PlayerController.Instance.StressControl.StressLevel();

                _heartbeatSource.PlayOneShot(_soundData.PlayerHeartbeatClips[_currentHeartbeatIndex], volume);

                //Heartbeat rate isn't equal; the first beat and the second are close together, the second and third are not. So we check if i is even to decide wait time.
                if (_currentHeartbeatIndex == 0 || _currentHeartbeatIndex == 2 || _currentHeartbeatIndex == 4)
                    _heartbeatTimer += playerData.HeartbeatMinimumRate / currentStressLevel;
                if (_currentHeartbeatIndex == 1 || _currentHeartbeatIndex == 3 || _currentHeartbeatIndex == 5)
                    _heartbeatTimer += (playerData.HeartbeatMinimumRate / currentStressLevel) * 1.5f;

                //Loops from 0 to heatbeat clips length
                if (_currentHeartbeatIndex < _soundData.PlayerHeartbeatClips.Length - 1)
                    _currentHeartbeatIndex++;
                else _currentHeartbeatIndex = 0;
            }

            _heartbeatTimer -= Time.deltaTime;
        }  
    }

    private void PlayerBreath(PlayerData playerData)
    {
        if (SceneManager.GetActiveScene().name == "Level_House") //Level_House
        {
            if (_breathingTimer <= 0)
            {
                float currentStressLevel = PlayerController.Instance.StressControl.StressLevel();
                float volume = _soundData.PlayerBreathClipsVolume * SoundManager.Instance.GlobalSoundFXVolume * currentStressLevel;

                _breathSource.pitch = 0.8f + (currentStressLevel / 12.5f); //Minimum pitch is 0.8; To get to maximum of 1, sum 0.2 when maximum stress level of 2.5f;
                _breathSource.PlayOneShot(_soundData.PlayerBreathClips[_currentBreathIndex], volume);

                if (_currentBreathIndex == 0 || _currentBreathIndex == 2 || _currentBreathIndex == 4)
                    _breathingTimer += playerData.BreathingMinimumRate / currentStressLevel;
                if (_currentBreathIndex == 1 || _currentBreathIndex == 3 || _currentBreathIndex == 5)
                    _breathingTimer += (playerData.BreathingMinimumRate / currentStressLevel) * 2;


                //Loops from 0 to heatbeat clips length
                if (_currentBreathIndex < _soundData.PlayerBreathClips.Length - 1)
                    _currentBreathIndex++;
                else _currentBreathIndex = 0;
            }

            _breathingTimer -= Time.deltaTime;
        }
    }
}