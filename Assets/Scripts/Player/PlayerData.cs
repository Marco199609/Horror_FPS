using UnityEngine;

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


    [Header("Inventory Control")]
    public float itemPickupDistance;
    public CharacterController characterController;
}
