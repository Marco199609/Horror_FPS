using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundData))]
public class SoundManager : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] public float GlobalSoundFXVolume = 1;
    [SerializeField] private AudioSource _musicSource, _effectsSource, _2dEffectsSource;

    public SoundData SoundData;
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        _effectsSource.spatialBlend = 1.0f;
        _2dEffectsSource.spatialBlend = 0.0f;
        _musicSource.spatialBlend = 0.0f;
    }

    public void PlaySoundEffect(AudioClip soundEffectClip, Vector3 soundEffectPosition, float soundEffectVolume, bool playOnce = false) //Set play once to true if having sound errors only
    {
        _effectsSource.transform.position = soundEffectPosition;

        if (playOnce)
        {
            if(!_effectsSource.isPlaying) _effectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * GlobalSoundFXVolume);
        }
        else _effectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * GlobalSoundFXVolume);
    }

    public void Play2DSoundEffect(AudioClip soundEffectClip, float soundEffectVolume, bool playOnce = false)
    {
        if (playOnce)
        {
            if (!_2dEffectsSource.isPlaying) _2dEffectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * GlobalSoundFXVolume);
        }
        else _2dEffectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * GlobalSoundFXVolume);
    }

    public AudioSource CreateModifiableAudioSource(AudioClip soundEffectClip, GameObject audioSourceHolder, float soundEffectVolume)
    {
        AudioSource modifiableSource = audioSourceHolder.AddComponent<AudioSource>();
        modifiableSource.playOnAwake = false;
        modifiableSource.clip = soundEffectClip;
        modifiableSource.volume = soundEffectVolume * GlobalSoundFXVolume;
        modifiableSource.spatialBlend = 1.0f;

        return modifiableSource;
    }
}