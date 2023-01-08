using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]

    [SerializeField] private CharacterController controller;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float groundDistance;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    [Header("Player Mouse Look")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform mainCamera;

    [Header("Player Scripts")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerMouseMovement playerMouseMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.PlayerMove(controller, walkSpeed, runSpeed, gravity, jumpHeight, groundDistance, groundCheck, groundMask);

        playerMouseMovement.MouseMove(mouseSensitivity, mainCamera);
    }
}
