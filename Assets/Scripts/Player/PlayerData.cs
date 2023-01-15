using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    [Header("Player Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;
    public float gravity;
    public float groundDistance;
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Player Rotation and Look")]
    public float mouseSensitivityX;
    public float mouseSensitivityY;

    [Header("Player Camera Movement Control")]
    public Transform camHolder;
    public Transform camTransform;
    [SerializeField, Range(0, 0.1f)] public float camMovementAmplitude = 0.005f;
    [SerializeField, Range(0, 30)] public float camMovementFrequency = 20.0f;

    [Header("Flashlight control")]
    public GameObject flashlight;

    [Header("Inventory Control")]
    public float itemPickupDistance;
    public Image UIPickupHand;
    public Image UICenterPoint;
    public CharacterController characterController;


    private void Awake()
    {
        ObjectManager.Instance.PlayerData = this;
        ObjectManager.Instance.Player = this.gameObject;
    }
}
