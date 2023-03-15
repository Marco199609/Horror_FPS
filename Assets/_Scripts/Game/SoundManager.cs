using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private float _globalSoundFXVolume = 1;

    [SerializeField] private AudioSource _musicSource, _effectsSource;

    public static SoundManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);

        

        _effectsSource.spatialBlend = 1.0f;
        _musicSource.spatialBlend = 0.0f;
    }

    public void PlaySoundEffect(AudioClip soundEffectClip, Vector3 soundEffectPosition, float soundEffectVolume, bool playOnce = false) //Set play once to true if having sound errors only
    {
        _effectsSource.transform.position = soundEffectPosition;

        if (playOnce)
        {
            if(!_effectsSource.isPlaying) _effectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * _globalSoundFXVolume);
        }
        else
        {
            _effectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * _globalSoundFXVolume);
        }
    }
}
