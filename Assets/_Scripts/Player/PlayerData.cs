using UnityEngine;
using UnityEngine.UI;

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
    public float mouseSensitivityX = 10f;
    public float mouseSensitivityY = 10f;

    [Header("Player Camera Movement Control")]
    public Transform camHolder;
    public Transform camTransform;
    [SerializeField, Range(0, 0.1f)] public float camMovementAmplitude = 0.005f;
    [SerializeField, Range(0, 30)] public float camMovementFrequency = 10.0f;

    [Header("Light control")]
    public GameObject Flashlight;
    public float MinIntensity = 1f;
    public float MaxIntensity = 5f;
    public float CurrentIntensity;
    public float SwitchOnLimit = 1.05f;
    public float ChangeVelocity = 7;
    public float MaxEnergy = 100;
    public float DepletionSpeed = 0.3f;
    public float CurrentEnergy;

    public GameObject WeaponLight;


    [Header("Inventory Control")]
    public float itemPickupDistance = 10f;
   
    public CharacterController characterController;
    private void Awake()
    {
        //ObjectManager.Instance.PlayerData = this;
        //ObjectManager.Instance.Player = this.gameObject;
    }
}
