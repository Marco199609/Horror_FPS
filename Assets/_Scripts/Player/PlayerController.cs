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
[RequireComponent(typeof(PlayerInspect))]
#endregion

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData _playerData;

    private GameObject _player;
    private Ray _ray; //used for item interaction

    private IPlayerMovement _playerMovement;
    private IPlayerRotate _playerRotate;
    private ICameraControl _playerCameraControl;
    private IFlashlightControl _playerFlashlight;
    private IPlayerInteract _playerInteract;
    private IPlayerUI _playerUI;
    private IPlayerInput _playerInput;
    private IPlayerAudio _playerAudio;

    public IPlayerInventory Inventory { get; private set; }
    public PlayerInspect PlayerInspect { get; private set; }

    //If there is one or more items in viewport, activate ui center point
    [NonSerialized] public List<GameObject> InteractablesInSight;

    private void Awake()
    {
        _playerData = GetComponentInChildren<PlayerData>();

        _playerMovement = GetComponent<IPlayerMovement>();
        _playerRotate = GetComponent<IPlayerRotate>();
        _playerCameraControl = GetComponent<ICameraControl>();
        _playerFlashlight = GetComponent<IFlashlightControl>();
        _playerInteract = GetComponent<IPlayerInteract>();
        _playerUI = GetComponent<IPlayerUI>();
        _playerInput = GetComponent<IPlayerInput>();
        _playerAudio = GetComponent<IPlayerAudio>();
        Inventory = GetComponent<IPlayerInventory>();
        PlayerInspect = GetComponent<PlayerInspect>();

        _player = _playerData.gameObject;

        //Adds this object to object manager for future use
        if(ObjectManager.Instance != null) ObjectManager.Instance.PlayerController = this;
    }

    void Start()
    {
        InteractablesInSight = new List<GameObject>();
    }

    private void Update()
    {
        if(!PlayerInspect.Inspecting())
        {
            CameraControl();
            PlayerMovement();
            PlayerAudioControl();
            FlashlightControl();
            ItemInteraction();
            InventoryManage();
        }

        ManageInspection();
    }

    private void LateUpdate()
    {
        if (!PlayerInspect.Inspecting())
        {
            PlayerRotation();
        }
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
        _playerCameraControl.ControlCameraHeadBob(_player);
    }

    private void PlayerAudioControl()
    {
        _playerAudio.Footsteps(_playerData, _playerInput);
    }

    private void FlashlightControl()
    {
        _playerFlashlight.FlashlightControl(_playerData, _playerInput);
    }

    private void InventoryManage()
    {
        Inventory.Manage(_playerData, _playerInput);
    }

    private void ManageInspection()
    {
        PlayerInspect.ManageInspection(_playerData, _playerInput);
    }
    private void ItemInteraction()
    {
        RaycastHit hit;
        _ray.origin = _playerData.camHolder.position;
        _ray.direction = _playerData.camHolder.forward;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            _playerUI.InteractableUI(_playerData, hit); //Activates UI elements when hovering over interactables
            _playerInteract.Interact(_playerData, hit, _playerInput);
        }

        _playerUI.CenterPointControl(InteractablesInSight); //Controls the center point appearing and disapearing when interactables in cammera view
    }
}