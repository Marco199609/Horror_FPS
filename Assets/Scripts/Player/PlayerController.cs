using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Components Required
[RequireComponent(typeof(PlayerData))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerMouseMovement))]
#endregion

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]

    [SerializeField] private CharacterController controller;

    [Header("Player Mouse Look")]
    [SerializeField] private Transform mainCamera;

    [Header("Player Scripts")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerMouseMovement playerMouseMovement;

    #region Player Input
    //Shows a header and instructions in the inspector
    [Header("Player Input", order = 1)]
    [Space(-10, order = 2)]
    [Header("Player Inputs are managed in the Input Manager.\nAttach the Input Manager here.", order = 3)]
    [Space(5, order = 4)]
    [SerializeField] private PlayerInput playerInput;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerMovement.PlayerMove(controller, playerData, playerInput);

        playerMouseMovement.MouseLook(playerData, mainCamera, playerInput);
    }
}
