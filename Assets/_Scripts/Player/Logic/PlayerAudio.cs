using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IPlayerAudio
{
    void Footsteps(PlayerData playerData, IPlayerInput playerInput);
    void PlayerBreathAndHeartbeat();
}

public class PlayerAudio : MonoBehaviour, IPlayerAudio
{
    private float timer;
    private AudioSource _playerBreathSource, _playerHeartbeatSource;
    private SoundData _soundData;

    private void Start()
    {
        if(SoundManager.Instance != null)
        {
            _soundData = SoundManager.Instance.SoundData;
            GameObject player = GameObject.FindWithTag("Player");

            _playerBreathSource = SoundManager.Instance.CreateModifiableAudioSource(
                _soundData.PlayerBreathClip, 
                player, _soundData.PlayerBreathClipVolume);

            _playerHeartbeatSource = SoundManager.Instance.CreateModifiableAudioSource(
                _soundData.PlayerHeartbeatClip, 
                player, _soundData.PlayerHeartbeatClipVolume);
        }         
    }
    public void Footsteps(PlayerData playerData, IPlayerInput playerInput)
    {
        if(playerInput.UnsmoothedPlayerMovementInput != Vector2.zero)
        {
            if(_playerBreathSource != null && timer <= 0)
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
                if (playerInput.playerRunInput)  timer = playerData.FootstepsRunningTime;
                else timer = playerData.FootstepWalkingTime;
            }
            timer -= Time.deltaTime;
        }
        else timer = 0;
    }

    public void PlayerBreathAndHeartbeat()
    {
        if(SceneManager.GetActiveScene().name == "Level_Dream") 
        {
            if (_playerBreathSource != null && _playerBreathSource.isPlaying) _playerBreathSource.Stop(); //Stop breathing

            if (_playerHeartbeatSource != null && !_playerHeartbeatSource.isPlaying) //Activate heartbeat
            {
                _playerHeartbeatSource.volume = _soundData.PlayerHeartbeatClipVolume * SoundManager.Instance.GlobalSoundFXVolume;
                _playerHeartbeatSource.Play();
            }
        }
        else if(SceneManager.GetActiveScene().name == "Level_House")
        {
            if (_playerHeartbeatSource != null && _playerHeartbeatSource.isPlaying) _playerHeartbeatSource.Stop(); //Stop heartbeat

            if (_playerBreathSource != null && !_playerBreathSource.isPlaying) //Activate breathing
            {
                _playerBreathSource.volume = _soundData.PlayerBreathClipVolume * SoundManager.Instance.GlobalSoundFXVolume;
                _playerBreathSource.Play();
            }   
        }  
    }
}