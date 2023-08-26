using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public interface IPlayerAudio
{
    void Footsteps(PlayerData playerData, IPlayerInput playerInput);
    void PlayerBreathAndHeartbeat();
}

public class PlayerAudio : MonoBehaviour, IPlayerAudio
{
    private int _currentHeartbeatIndex = 0;
    private float _footStepTimer, _breathingTimer = 0, _heartbeatTimer = 0;
    private AudioSource _playerBreathSource, _playerHeartbeatSource;
    private SoundData _soundData;

    private void Start()
    {
        if(SoundManager.Instance != null)
        {
            _soundData = SoundManager.Instance.SoundData;
            GameObject player = GameObject.FindWithTag("Player");

            /*_playerBreathSource = SoundManager.Instance.CreateModifiableAudioSource(
                _soundData.PlayerBreathClip, 
                player, _soundData.PlayerBreathClipVolume);

            _playerHeartbeatSource = SoundManager.Instance.CreateModifiableAudioSource(
                _soundData.PlayerHeartbeatClip, 
                player, _soundData.PlayerHeartbeatClipVolume);*/
        }         
    }
    public void Footsteps(PlayerData playerData, IPlayerInput playerInput)
    {
        if(playerInput.UnsmoothedPlayerMovementInput != Vector2.zero)
        {
            if(/*_playerBreathSource != null &&*/ _footStepTimer <= 0)
            {
                if(SceneManager.GetActiveScene().name == "Level_Dream")
                {
                    int i = Random.Range(0, _soundData.ConcreteFootstepClips.Length);
                    SoundManager.Instance.Play2DSoundEffect(_soundData.ConcreteFootstepClips[i], _soundData.ConcreteFootstepClipsVolume);
                }
                else
                {
                    int i = Random.Range(0, _soundData.WoodFootstepClips.Length);
                    SoundManager.Instance.Play2DSoundEffect(_soundData.WoodFootstepClips[i], _soundData.WoodFootstepClipsVolume);
                }
                if (playerInput.playerRunInput) _footStepTimer = playerData.FootstepsRunningTime;
                else _footStepTimer = playerData.FootstepWalkingTime;
            }
            _footStepTimer -= Time.deltaTime;
        }
        else _footStepTimer = 0;

        PlayerHeartbeat(playerData);
    }

    public void PlayerBreathAndHeartbeat()
    {
        /*
        if(SceneManager.GetActiveScene().name == "Level_Dream") 
        {
            if (_playerBreathSource != null && _playerBreathSource.isPlaying) _playerBreathSource.Stop(); //Stop breathing

            if (_playerHeartbeatSource != null && !_playerHeartbeatSource.isPlaying) //Activate heartbeat
            {
                _playerHeartbeatSource.volume = _soundData.PlayerHeartbeatClipsVolume * SoundManager.Instance.GlobalSoundFXVolume * PlayerController.Instance.StressControl.StressLevel();
                _playerHeartbeatSource.Play();
            }
        }
        else if(SceneManager.GetActiveScene().name == "Level_House")
        {
            if (_playerHeartbeatSource != null && _playerHeartbeatSource.isPlaying) _playerHeartbeatSource.Stop(); //Stop heartbeat

            if (_playerBreathSource != null && !_playerBreathSource.isPlaying) //Activate breathing
            {
                _playerBreathSource.volume = _soundData.PlayerBreathClipsVolume * SoundManager.Instance.GlobalSoundFXVolume;
                _playerBreathSource.Play();
            }   
        }  */


    }


    public void PlayerHeartbeat(PlayerData playerData)
    {
        if (SceneManager.GetActiveScene().name == "Level_Dream")
        {
            if (_heartbeatTimer <= 0)
            {
                SoundManager.Instance.Play2DSoundEffect(_soundData.PlayerHeartbeatClips[_currentHeartbeatIndex], 
                    _soundData.PlayerHeartbeatClipsVolume * SoundManager.Instance.GlobalSoundFXVolume * PlayerController.Instance.StressControl.StressLevel(), true);


                //Heartbeat rate isn't equal; the first beat and the second are close together, the second and third are not. So we check if i is even to decide wait time.
                if (_currentHeartbeatIndex % 2 == 1)
                    _heartbeatTimer += playerData.HeartbeatMinimumRate; 
                else if (_currentHeartbeatIndex % 2 == 0)
                    _heartbeatTimer += 0.8f / PlayerController.Instance.StressControl.StressLevel();

                if (_currentHeartbeatIndex < _soundData.PlayerHeartbeatClips.Length - 1)
                    _currentHeartbeatIndex++;
                else _currentHeartbeatIndex = 0;
            }

            _heartbeatTimer -= Time.deltaTime;


        }
       
    }
}