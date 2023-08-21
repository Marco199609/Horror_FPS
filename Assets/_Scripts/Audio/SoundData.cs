using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundData : MonoBehaviour
{
    [field: SerializeField] public AudioClip MainMenuMusicClip { get; private set; }
    [Range(0.0f, 1.0f)] public float MainMenuMusicClipVolume = 0.2f;

    //Door clips
    [field: SerializeField] public AudioClip DoorOpenClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DoorOpenClipVolume = 0.7f;
    [field: SerializeField] public AudioClip DoorCloseClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DoorCloseClipVolume = 0.7f;
    [field: SerializeField] public AudioClip DoorLockedClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DoorLockedClipVolume = 0.7f;
    [field: SerializeField] public AudioClip DoorUnlockClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DoorUnlockClipVolume = 0.7f;

    //Drawer clips
    [field: SerializeField] public AudioClip DrawerOpenClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DrawerOpenClipVolume = 0.7f;
    [field: SerializeField] public AudioClip DrawerCloseClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DrawerCloseClipVolume = 0.7f;
    [field: SerializeField] public AudioClip DrawerLockedClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DrawerLockedClipVolume = 0.7f;
    [field: SerializeField] public AudioClip DrawerUnlockClip { get; private set; }
    [Range(0.0f, 1.0f)] public float DrawerUnlockClipVolume = 0.7f;

    //Other interactables
    [field: SerializeField] public AudioClip LightSwitchOnClip { get; private set; }
    [Range(0.0f, 1.0f)] public float LightSwitchOnClipVolume = 0.2f;
    [field: SerializeField] public AudioClip LightSwitchOffClip { get; private set; }
    [Range(0.0f, 1.0f)] public float LightSwitchOffClipVolume = 0.2f;
    [field: SerializeField] public AudioClip MouseClickClip { get; private set; }
    [Range(0.0f, 1.0f)] public float MouseClickClipVolume = 0.2f;
    [field: SerializeField] public AudioClip PillbottleClip { get; private set; }
    [Range(0.0f, 1.0f)] public float PillbottleClipVolume = 0.2f;
    [field: SerializeField] public AudioClip[] GuitarStrumClips { get; private set; }
    [Range(0.0f, 1.0f)] public float GuitarStrumClipVolume = 0.7f;

    //Player clips
    [field: SerializeField] public AudioClip ItemInspectClip { get; private set; }
    [Range(0.0f, 1.0f)] public float ItemInspectClipVolume = 0.05f;
    [field: SerializeField] public AudioClip PlayerPickupClip { get; private set; }
    [Range(0.0f, 1.0f)] public float PlayerPickupClipVolume = 0.05f;
    [field: SerializeField] public AudioClip[] ConcreteFootstepClips { get; private set; }
    [Range(0.0f, 1.0f)] public float ConcreteFootstepClipsVolume = 0.2f;
    [field: SerializeField] public AudioClip[] WoodFootstepClips { get; private set; }
    [Range(0.0f, 1.0f)] public float WoodFootstepClipsVolume = 0.15f;
    [field: SerializeField] public AudioClip FlashlightClip { get; private set; }
    [Range(0.0f, 1.0f)] public float FlashlightClipVolume = 0.2f;
    [field: SerializeField] public AudioClip PlayerBreathClip { get; private set; }
    [Range(0.0f, 1.0f)] public float PlayerBreathClipVolume = 0.2f;
    [field: SerializeField] public AudioClip PlayerHeartbeatClip { get; private set; }
    [Range(0.0f, 1.0f)] public float PlayerHeartbeatClipVolume = 0.03f;

    //Light flicker clips
    [field: SerializeField] public AudioClip LightFlickerClip { get; private set; }
    [Range(0.0f, 1.0f)] public float LightFlickerClipVolume = 0.2f;
}
