using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float
        walkSpeed,
        runSpeed,
        jumpHeight,
        gravity,
        groundDistance,
        mouseSensitivity;
    public Transform groundCheck;
    public LayerMask groundMask;
}
