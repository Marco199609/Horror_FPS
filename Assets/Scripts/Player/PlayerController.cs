using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Components Required
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotate))]
[RequireComponent(typeof(PlayerCameraControl))]
[RequireComponent(typeof(PlayerFlashlight))]
[RequireComponent(typeof(PlayerItemPickup))]
[RequireComponent(typeof(PlayerWeaponPickup))]
[RequireComponent(typeof(PlayerHover))]
#endregion

public class PlayerController : MonoBehaviour
{
    private ObjectManager _objectManager;
    private GameObject _player;
    private PlayerInput _playerInput;
    private GameController _gameController;
    private WeaponController _weaponController;
    private InventoryController _inventoryController;

    private PlayerData _playerData;
    private Ray _ray; //used for item interaction

    //Player scripts attached to this gameobject
    private IPlayerMovement _playerMovement;
    private IPlayerRotate _playerRotate;
    private ICameraControl _playerCameraControl;
    private IFlashlightControl _playerFlashlight;
    private IPlayerUIHover _playerHover;
    private IPlayerPickup[] _playerPickup;

    private void Awake()
    {
        //Gets required scripts on this gameobject
        _playerMovement = GetComponent<IPlayerMovement>();
        _playerRotate = GetComponent<IPlayerRotate>();
        _playerCameraControl = GetComponent<ICameraControl>();
        _playerFlashlight = GetComponent<IFlashlightControl>();
        _playerHover = GetComponent<IPlayerUIHover>();
        _playerPickup = GetComponents<IPlayerPickup>();

        //Adds this object to object manager for future use
        ObjectManager.Instance.PlayerController = this;
    }

    void Start()
    {
        //Gets object manager and objects required
        _objectManager = ObjectManager.Instance;
        _player = _objectManager.Player;
        _playerInput = _objectManager.PlayerInput;
        _gameController = _objectManager.GameController;
        _weaponController = _objectManager.WeaponController;
        _inventoryController = _objectManager.InventoryController;

        _playerData = _player.GetComponent<PlayerData>();
    }

    void Update()
    {
        //Controls player only if inventory closed (Game does not pause)
        if(!_objectManager.InventoryController.IsInventoryEnabled)
        {
            PlayerMovementAndRotation();
            CameraControl();
        }

        FlashlightControl();

        //Picks up items only if weapon inactive and if inventory closed
        if (!_weaponController.isWeaponActive && !_inventoryController.IsInventoryEnabled)
        {
            ItemInteraction();
        }
        else _playerData.UICenterPoint.gameObject.SetActive(false); //Deactivates center point
    }

    private void PlayerMovementAndRotation()
    {
        _playerMovement.PlayerMove(_player, _playerInput);
        _playerRotate.RotatePlayer(_player, _playerInput);
    }

    private void ItemInteraction()
    {
        RaycastHit hit;
        _ray.origin = _playerData.camHolder.position;
        _ray.direction = _playerData.camHolder.forward;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            _playerHover.Hover(_player, hit, _gameController); //Activates UI elements when hovering over items and weapons

            if (_playerInput.playerPickupInput)
            {
                //There are weapon pickup and item pickup scripts
                for (int i = 0; i < _playerPickup.Length; i++)
                {
                    _playerPickup[i].Pickup(_player, hit, _playerInput);
                }
            }
        }
    }

    private void CameraControl()
    {
        _playerCameraControl.ControlCamera(_player); //Controls camera head bob
    }

    private void FlashlightControl()
    {
        _playerFlashlight.FlashlightControl(_playerData.flashlight.GetComponent<Light>(), _playerInput); //Controls flashlight intensity
    }
}