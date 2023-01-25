using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


#region Components Required
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotate))]
[RequireComponent(typeof(PlayerCameraControl))]
[RequireComponent(typeof(PlayerFlashlight))]
[RequireComponent(typeof(PlayerInteract))]
[RequireComponent(typeof(PlayerHover))]
[RequireComponent(typeof(PlayerUI))]
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
    private IPlayerInteract _playerInteract;
    private IPlayerUI _playerUI;

    //If there is one or more items in viewport, activate ui center point
    public List<GameObject> ItemsVisible;

    private void Awake()
    {
        //Gets required scripts on this gameobject
        _playerMovement = GetComponent<IPlayerMovement>();
        _playerRotate = GetComponent<IPlayerRotate>();
        _playerCameraControl = GetComponent<ICameraControl>();
        _playerFlashlight = GetComponent<IFlashlightControl>();
        _playerHover = GetComponent<IPlayerUIHover>();
        _playerInteract = GetComponent<IPlayerInteract>();
        _playerUI = GetComponent<IPlayerUI>();

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

        ItemsVisible = new List<GameObject>();
    }

    private void Update()
    {
        //Controls player only if inventory closed (Game does not pause)
        if(!_objectManager.InventoryController.IsInventoryEnabled)
        {
            PlayerRotation();
            CameraControl();
        }

        //Player can move and control flashlight even if inventory enabled
        PlayerMovement();
        FlashlightControl();

        //Picks up items only if weapon inactive and if inventory closed
        if (!_weaponController.IsWeaponActive && !_inventoryController.IsInventoryEnabled)
        {
            ItemInteraction();
            UICenterPointControl();
        }
        else _playerData.UICenterPoint.gameObject.SetActive(false); //Deactivates center point
    }


    private void PlayerMovement()
    {
        _playerMovement.PlayerMove(_player, _playerInput);
    }

    private void PlayerRotation()
    {
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
            _playerInteract.InteractWithObject(_player, hit, _playerInput);
        }
    }

    private void CameraControl()
    {
        _playerCameraControl.ControlCamera(_player); //Controls camera head bob
    }

    private void FlashlightControl()
    {
        _playerFlashlight.FlashlightControl(_playerData.flashlight.GetComponent<Light>(), _playerData.weaponLight.GetComponent<Light>(),  _playerInput); //Controls flashlight intensity
    }

    //Controls the center point appearing and disapearing
    private void UICenterPointControl()
    {
        _playerUI.CenterPointControl(_playerData.UICenterPoint.GetComponent<Image>(), ItemsVisible);
    }
}