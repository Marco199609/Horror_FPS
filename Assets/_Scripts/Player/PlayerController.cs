using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


#region Components Required
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerRotate))]
[RequireComponent(typeof(PlayerCameraControl))]
[RequireComponent(typeof(PlayerFlashlight))]
[RequireComponent(typeof(PlayerInteract))]
[RequireComponent(typeof(PlayerUI))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerAudio))]
[RequireComponent(typeof(PlayerInventory))]
#endregion

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private GameObject _player;
    private Ray _ray; //used for item interaction

    //Player scripts attached to this gameobject
    private IPlayerMovement _playerMovement;
    private IPlayerRotate _playerRotate;
    private ICameraControl _playerCameraControl;
    private IFlashlightControl _playerFlashlight;
    private IPlayerInteract _playerInteract;
    private IPlayerUI _playerUI;
    private IPlayerInput _playerInput;
    private IPlayerAudio _playerAudio;
    private PlayerInventory _playerInventory;

    //If there is one or more items in viewport, activate ui center point
    [NonSerialized] public List<GameObject> InteractablesInSight;

    public List<GameObject> Inventory;
    public GameObject SelectedInventoryItem;

    private void Awake()
    {
        //Gets required scripts on this gameobject
        if(_playerData == null) _playerData = GetComponentInChildren<PlayerData>();

        _playerMovement = GetComponent<IPlayerMovement>();
        _playerRotate = GetComponent<IPlayerRotate>();
        _playerCameraControl = GetComponent<ICameraControl>();
        _playerFlashlight = GetComponent<IFlashlightControl>();
        _playerInteract = GetComponent<IPlayerInteract>();
        _playerUI = GetComponent<IPlayerUI>();
        _playerInput = GetComponent<IPlayerInput>();
        _playerAudio = GetComponent<IPlayerAudio>();
        _playerInventory = GetComponent<PlayerInventory>();

        _player = _playerData.gameObject;

        //Adds this object to object manager for future use
        if(ObjectManager.Instance != null) ObjectManager.Instance.PlayerController = this;

        Inventory.Add(null);
        SelectedInventoryItem = Inventory[0];
    }

    void Start()
    {
        InteractablesInSight = new List<GameObject>();
    }

    private void Update()
    {
        CameraControl();
        PlayerMovement();
        PlayerAudioControl();
        FlashlightControl();
        ItemInteraction();
        PlayerInventory();
    }

    private void LateUpdate()
    {
        PlayerRotation();
    }

    private void PlayerMovement()
    {
        _playerMovement.PlayerMove(_player, _playerInput);
    }

    private void PlayerRotation()
    {
        _playerRotate.RotatePlayer(_player, _playerInput);
    }

    private void CameraControl()
    {
        _playerCameraControl.ControlCamera(_player); //Controls camera head bob
    }

    private void PlayerAudioControl()
    {
        _playerAudio.Footsteps(_playerData, _playerInput);
    }

    private void FlashlightControl()
    {
        _playerFlashlight.FlashlightControl(_playerData, _playerInput); //Controls flashlight intensity
    }

    private void PlayerInventory()
    {
        SelectedInventoryItem = _playerInventory.Manage(Inventory, _playerInput);
    }
    private void ItemInteraction()
    {
        RaycastHit hit;
        _ray.origin = _playerData.camHolder.position;
        _ray.direction = _playerData.camHolder.forward;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            _playerUI.InteractableUI(_playerData, hit); //Activates UI elements when hovering over interactables
            _playerInteract.Interact(_playerData, hit, _playerInput); //Interacts when player inputs
        }

        _playerUI.CenterPointControl(InteractablesInSight); //Controls the center point appearing and disapearing when interactables in cammera view
    }
}