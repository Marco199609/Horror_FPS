using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Components Required
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotate))]
[RequireComponent(typeof(PlayerCameraControl))]
[RequireComponent(typeof(PlayerFlashLight))]
[RequireComponent(typeof(PlayerItemPickup))]
#endregion


public class PlayerController : MonoBehaviour
{
    private ObjectManager objectManager;

    private PlayerData playerData;
    private InventoryController inventoryController;
    private PlayerInput playerInput;

    //Player scripts attached to this gameobject
    private PlayerMovement playerMovement;
    private PlayerRotate playerRotate;
    private PlayerCameraControl playerCameraControl;
    private PlayerFlashLight playerFlashLight;
    private PlayerItemPickup playerItemPickup;

    private GameObject player;

    private void Awake()
    {
        //Gets required scripts on this gameobject
        playerMovement = GetComponent<PlayerMovement>();
        playerRotate = GetComponent<PlayerRotate>();
        playerCameraControl = GetComponent<PlayerCameraControl>();
        playerFlashLight = GetComponent<PlayerFlashLight>();
        playerItemPickup = GetComponent<PlayerItemPickup>();



        //Adds this object to object manager for future use
        ObjectManager.Instance.PlayerController = this;
    }

    void Start()
    {
        //Gets object manager
        objectManager = ObjectManager.Instance;

        //Gets objects from object manager
        playerData = objectManager.PlayerData;
        player = objectManager.Player;
        playerInput = objectManager.PlayerInput;
        inventoryController = objectManager.InventoryController;
    }

    // Update is called once per frame
    void Update()
    {
        //Controls player only if inventory closed (Game does not pause)
        if(!objectManager.InventoryController.IsInventoryEnabled)
        {
            PlayerMovementAndRotation();
            CameraControl();
        }

        FlashlightControl();

        if (!objectManager.WeaponController.isWeaponActive)
        {
            ItemPickup();
        }
    }

    private void PlayerMovementAndRotation()
    {
        playerMovement.PlayerMove(player, playerData, playerInput);
        playerRotate.MouseLook(player, playerData, playerInput);
    }

    private void ItemPickup()
    {
        playerItemPickup.ItemPickup(playerInput, inventoryController, playerData);
    }

    private void CameraControl()
    {
        playerCameraControl.ControlCamera(player, playerData);
    }

    private void FlashlightControl()
    {
        playerFlashLight.FlashlightControl(player, playerData, playerInput);
    }
}
