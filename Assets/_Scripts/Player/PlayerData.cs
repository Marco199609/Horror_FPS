using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerData : MonoBehaviour
{
    [Header("Player Movement")]
    public float walkSpeed = 10f;
    public float runSpeed = 10f;
    public float jumpHeight = 8f;
    public float gravity = -19.62f;
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Player Rotation and Look")]
    public float mouseSensitivityX = 25f;
    public float mouseSensitivityY = 10f;
    public Transform CinemachineLookAt;
    public CinemachineVirtualCamera VirtualCamera;

    [Header("Player Camera Movement Control")]
    public Transform camHolder;
    public Transform Camera;
    [SerializeField, Range(0, 0.1f)] public float camMovementAmplitude = 0.005f;
    [SerializeField, Range(0, 30)] public float camMovementFrequency = 10.0f;

    [Header("Light control")]
    public GameObject Flashlight;
    public float MinIntensity = 1f;
    public float MaxIntensity = 5f;
    public float ChangeVelocity = 7;

    //public GameObject WeaponLight;

    [Header("Player Audio Control")]
    public AudioSource PlayerAudioSource;
    public float FootstepWalkingTime;
    public float FootstepsRunningTime;
    public AudioClip[] Footsteps;

    [Header("Interactable Control")]
    public float InteractDistance = 5f;
    public Transform InventoryHolder;
   
    public CharacterController characterController;
}
