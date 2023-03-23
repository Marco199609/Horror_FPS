using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField, Range(0.0f, 1.0f)] private float _globalSoundFXVolume = 1;

    [field: SerializeField] public AudioClip MainMenuMusicClip { get; private set; }
    [Range(0.0f, 1.0f)] public float MainMenuMusicClipVolume = 0.2f;

    //Door clips
    [field: SerializeField] public AudioClip DoorOpenClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DoorOpenClipVolume = 0.2f;
    [field: SerializeField] public AudioClip DoorCloseClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DoorCloseClipVolume = 0.2f;
    [field: SerializeField] public AudioClip DoorLockedClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DoorLockedClipVolume = 0.2f;
    [field: SerializeField] public AudioClip DoorUnlockClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DoorUnlockClipVolume = 0.2f;

    //Drawer clips
    [field: SerializeField] public AudioClip DrawerOpenClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DrawerOpenClipVolume = 0.2f;
    [field: SerializeField] public AudioClip DrawerCloseClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DrawerCloseClipVolume = 0.2f;
    [field: SerializeField] public AudioClip DrawerLockedClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DrawerLockedClipVolume = 0.2f;
    [field: SerializeField] public AudioClip DrawerUnlockClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DrawerUnlockClipVolume = 0.2f;

    //Other interactables
    [field: SerializeField] public AudioClip LightSwitchOnClip { get; private set; }
    [Range(0.0f, 1.0f)] public float LightSwitchOnClipVolume = 0.2f;
    [field: SerializeField] public AudioClip LightSwitchOffClip { get; private set; }
    [Range(0.0f, 1.0f)] public float LightSwitchOffClipVolume = 0.2f;
    [field: SerializeField] public AudioClip MouseClickClip { get; private set; }
    [Range(0.0f, 1.0f)] public float MouseClickClipVolume = 0.2f;

    //Player clips
    [field: SerializeField] public AudioClip ItemInspectClip { get; private set; }
    [Range(0.0f, 1.0f)] public float ItemInspectClipVolume = 0.2f;
    [field: SerializeField] public AudioClip PlayerPickupClip { get; private set; }
    [Range(0.0f, 1.0f)] public float PlayerPickupClipVolume = 0.2f;
    [field: SerializeField] public AudioClip[] FootstepClips { get; private set; }
    [Range(0.0f, 1.0f)] public float FootstepClipsVolume = 0.2f;
    [field: SerializeField] public AudioClip FlashlightClip { get; private set; }
    [Range(0.0f, 1.0f)] public float FlashlightClipVolume = 0.2f;
    [field: SerializeField] public AudioClip PlayerBreathClip { get; private set; }
    [Range(0.0f, 1.0f)] public float PlayerBreathClipVolume = 0.2f;

    //Light flicker clips
    [field: SerializeField] public AudioClip LightFlickerClip { get; private set; }
    [Range(0.0f, 1.0f)] public float LightFlickerClipVolume = 0.2f;

    [SerializeField] private AudioSource _musicSource, _effectsSource, _2dEffectsSource;

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
        _2dEffectsSource.spatialBlend = 0.0f;
        _musicSource.spatialBlend = 0.0f;
    }

    public void PlaySoundEffect(AudioClip soundEffectClip, Vector3 soundEffectPosition, float soundEffectVolume, bool playOnce = false) //Set play once to true if having sound errors only
    {
        _effectsSource.transform.position = soundEffectPosition;

        if (playOnce)
        {
            if(!_effectsSource.isPlaying) _effectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * _globalSoundFXVolume);
        }
        else _effectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * _globalSoundFXVolume);
    }

    public void Play2DSoundEffect(AudioClip soundEffectClip, float soundEffectVolume, bool playOnce = false)
    {
        if (playOnce)
        {
            if (!_2dEffectsSource.isPlaying) _2dEffectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * _globalSoundFXVolume);
        }
        else _2dEffectsSource.PlayOneShot(soundEffectClip, soundEffectVolume * _globalSoundFXVolume);
    }

    public AudioSource CreateModifiableAudioSource(AudioClip soundEffectClip, GameObject audioSourceHolder, float soundEffectVolume)
    {
        AudioSource modifiableSource = audioSourceHolder.AddComponent<AudioSource>();
        modifiableSource.playOnAwake = false;
        modifiableSource.clip = soundEffectClip;
        modifiableSource.volume = soundEffectVolume;
        modifiableSource.spatialBlend = 1.0f;

        return modifiableSource;
    }
}