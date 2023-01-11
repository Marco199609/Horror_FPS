using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public float
        walkSpeed,
        runSpeed,
        jumpHeight,
        gravity,
        groundDistance,
        mouseSensitivityX,
        mouseSensitivityY;
    public Transform groundCheck;
    public LayerMask groundMask;
}
