using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IPlayerAudio
{
    void Footsteps(PlayerData playerData, IPlayerInput playerInput);
    void PlayerBreath();
    void PlayerHeartbeat();
}

public class PlayerAudio : MonoBehaviour, IPlayerAudio
{
    private AudioSource _playerBreathSource;
    private float timer;

    private void Start()
    {
        if(SoundManager.Instance != null)
            _playerBreathSource = SoundManager.Instance.CreateModifiableAudioSource(SoundManager.Instance.PlayerBreathClip, GameObject.FindWithTag("Player"), SoundManager.Instance.PlayerBreathClipVolume);
    }
    public void Footsteps(PlayerData playerData, IPlayerInput playerInput)
    {
        if(playerInput.UnsmoothedPlayerMovementInput != Vector2.zero)
        {
            if(_playerBreathSource != null && timer <= 0)
            {
                if(SceneManager.GetActiveScene().name == "Level_Dream")
                {
                    int i = Random.Range(0, SoundManager.Instance.ConcreteFootstepClips.Length);
                    SoundManager.Instance.Play2DSoundEffect(SoundManager.Instance.ConcreteFootstepClips[i], SoundManager.Instance.FootstepClipsVolume);
                }
                else
                {
                    int i = Random.Range(0, SoundManager.Instance.WoodFootstepClips.Length);
                    SoundManager.Instance.Play2DSoundEffect(SoundManager.Instance.WoodFootstepClips[i], SoundManager.Instance.FootstepClipsVolume);
                }
                if (playerInput.playerRunInput)  timer = playerData.FootstepsRunningTime;
                else timer = playerData.FootstepWalkingTime;
            }
            timer -= Time.deltaTime;
        }
        else timer = 0;
    }

    public void PlayerBreath()
    {
        if(_playerBreathSource != null && !_playerBreathSource.isPlaying)
        {
            _playerBreathSource.volume = SoundManager.Instance.PlayerBreathClipVolume * SoundManager.Instance.GlobalSoundFXVolume;
            _playerBreathSource.Play();
        }
    }

    public void PlayerHeartbeat()
    {
        throw new System.NotImplementedException();
    }
}