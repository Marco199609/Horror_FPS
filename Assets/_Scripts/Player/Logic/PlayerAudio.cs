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
    private SoundManager _soundManager;

    private void Start()
    {
        if(SoundManager.Instance != null)
        {
            _soundManager = SoundManager.Instance;
            GameObject player = GameObject.FindWithTag("Player");

            _playerBreathSource = _soundManager.CreateModifiableAudioSource(
                _soundManager.PlayerBreathClip, 
                player, _soundManager.PlayerBreathClipVolume);

            _playerHeartbeatSource = _soundManager.CreateModifiableAudioSource(
                _soundManager.PlayerHeartbeatClip, 
                player, _soundManager.PlayerHeartbeatClipVolume);
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
                    int i = Random.Range(0, _soundManager.ConcreteFootstepClips.Length);
                    _soundManager.Play2DSoundEffect(_soundManager.ConcreteFootstepClips[i], _soundManager.ConcreteFootstepClipsVolume);
                }
                else
                {
                    int i = Random.Range(0, _soundManager.WoodFootstepClips.Length);
                    _soundManager.Play2DSoundEffect(_soundManager.WoodFootstepClips[i], _soundManager.WoodFootstepClipsVolume);
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
        if (_playerBreathSource != null && !_playerBreathSource.isPlaying)
        {
            if (SceneManager.GetActiveScene().name == "Level_House")
                _playerBreathSource.volume = _soundManager.PlayerBreathClipVolume * _soundManager.GlobalSoundFXVolume;
            else
                _playerBreathSource.volume = 0;

            _playerBreathSource.Play();
        }

        if(_playerHeartbeatSource != null && !_playerHeartbeatSource.isPlaying)
        {
            if (SceneManager.GetActiveScene().name == "Level_Dream")
                _playerHeartbeatSource.volume = _soundManager.PlayerHeartbeatClipVolume * _soundManager.GlobalSoundFXVolume;
            else
                _playerHeartbeatSource.volume = 0;

            _playerHeartbeatSource.Play();
        }
    }
}