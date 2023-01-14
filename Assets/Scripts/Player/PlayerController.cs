using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Components Required
[RequireComponent(typeof(PlayerData))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotate))]
#endregion

public class PlayerController : MonoBehaviour
{
    [Header("Player Scripts")]
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerRotate playerRotate;
    [SerializeField] private PlayerCameraControl playerCameraControl;
    [SerializeField] private PlayerFlashLight playerFlashLight;

    [Header("Inventory Management")]
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private PlayerItemPickup playerItemPickup;
    [SerializeField] private GameObject handIcon;

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
        //Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementAndRotation();
        InventoryControl();
        CameraControl();
        FlashlightControl();
    }

    private void PlayerMovementAndRotation()
    {
        playerMovement.PlayerMove(playerData, playerInput);

        playerRotate.MouseLook(playerData, playerInput);
    }

    private void InventoryControl()
    {
        playerItemPickup.ItemPickup(playerInput, inventoryController, playerData, handIcon);
    }


    private void CameraControl()
    {
        playerCameraControl.ControlCamera(playerData);
    }

    private void FlashlightControl()
    {
        playerFlashLight.FlashlightControl(playerData);
    }
}
