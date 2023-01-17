using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Components Required
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotate))]
[RequireComponent(typeof(PlayerCameraControl))]
[RequireComponent(typeof(PlayerFlashLight))]
[RequireComponent(typeof(PlayerItemPickup))]
[RequireComponent(typeof(PlayerItemHover))]
#endregion


public class PlayerController : MonoBehaviour
{
    private ObjectManager objectManager;

    private PlayerData playerData;
    private InventoryController inventoryController;
    private GameController gameController;
    private PlayerInput playerInput;

    //Player scripts attached to this gameobject
    private PlayerMovement playerMovement;
    private PlayerRotate playerRotate;
    private PlayerCameraControl playerCameraControl;
    private PlayerFlashLight playerFlashlight;
    private PlayerItemPickup playerItemPickup;
    private PlayerItemHover playerItemHover;

    private GameObject player;
    Ray ray; //used for item interaction
    LayerMask itemLayerMask;

    private void Awake()
    {
        //Gets required scripts on this gameobject
        playerMovement = GetComponent<PlayerMovement>();
        playerRotate = GetComponent<PlayerRotate>();
        playerCameraControl = GetComponent<PlayerCameraControl>();
        playerFlashlight = GetComponent<PlayerFlashLight>();
        playerItemPickup = GetComponent<PlayerItemPickup>();
        playerItemHover= GetComponent<PlayerItemHover>();

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
        gameController = objectManager.GameController;

        itemLayerMask = playerData.ItemLayerMask;
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

        //picks up items only if weapon inactive and if inventory closed
        if (!objectManager.WeaponController.isWeaponActive && !objectManager.InventoryController.IsInventoryEnabled)
        {
            ItemInteraction();
        }
    }

    private void PlayerMovementAndRotation()
    {
        playerMovement.PlayerMove(player, playerData, playerInput);
        playerRotate.MouseLook(player, playerData, playerInput);
    }

    private void ItemInteraction()
    {
        RaycastHit hit;
        ray.origin = playerData.camHolder.position;
        ray.direction = playerData.camHolder.forward;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, itemLayerMask))
        {
            if (hit.distance <= playerData.itemPickupDistance) //Checks if item reachable
            {
                playerItemHover.HoverOverItem(hit, playerData, gameController);
                playerItemPickup.ItemPickup(hit, playerInput);
            }
        }
        else
            playerItemHover.DeactivateUIElements(playerData, gameController);
    }

    private void CameraControl()
    {
        playerCameraControl.ControlCamera(player, playerData);
    }

    private void FlashlightControl()
    {
        playerFlashlight.FlashlightControl(playerData, playerInput);
    }
}
