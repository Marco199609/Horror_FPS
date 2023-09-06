using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


#region Components Required
[RequireComponent(typeof(IPlayerMovement))]
[RequireComponent(typeof(IPlayerRotate))]
[RequireComponent(typeof(ICameraControl))]
[RequireComponent(typeof(IFlashlightControl))]
[RequireComponent(typeof(IPlayerInteract))]
[RequireComponent(typeof(IPlayerUI))]
[RequireComponent(typeof(IPlayerInput))]
[RequireComponent(typeof(IPlayerAudio))]
[RequireComponent(typeof(IPlayerInventory))]
[RequireComponent(typeof(IPlayerInspect))]
[RequireComponent(typeof(PlayerStressControl))]
#endregion

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private PlayerData _playerData;


    private Ray _ray; //used for item interaction
    private CinemachineBrain _cinemachine;

    private IPlayerMovement _playerMovement;
    private IPlayerRotate _playerRotate;
    private ICameraControl _playerCameraControl;
    private IPlayerInteract _playerInteract;
    private IPlayerUI _playerUI;
    private IPlayerInput _playerInput;
    private IPlayerAudio _playerAudio;


    public GameObject Player { get; private set; }
    public IFlashlightControl PlayerFlashlight;
    public IPlayerInventory Inventory { get; private set; }
    public PlayerStressControl StressControl;
    public IPlayerInspect PlayerInspect { get; private set; }

    //If there is one or more items in viewport, activate ui center point
    [NonSerialized] public List<GameObject> InteractablesInSight;

   //Used in crawler, to freeze player for next level
    public bool FreezePlayerMovement, FreezePlayerRotation;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        _playerData = GetComponentInChildren<PlayerData>();

        _playerMovement = GetComponent<IPlayerMovement>();
        _playerRotate = GetComponent<IPlayerRotate>();
        _playerCameraControl = GetComponent<ICameraControl>();
        _playerInteract = GetComponent<IPlayerInteract>();
        _playerUI = GetComponent<IPlayerUI>();
        _playerInput = GetComponent<IPlayerInput>();
        _playerAudio = GetComponent<IPlayerAudio>();

        StressControl = GetComponent<PlayerStressControl>();
        PlayerFlashlight = GetComponent<IFlashlightControl>();
        Inventory = GetComponent<IPlayerInventory>();
        PlayerInspect = GetComponent<IPlayerInspect>();


        Player = _playerData.gameObject;
    }

    void Start()
    {
        _gameSettings = FindObjectOfType<GameSettings>();
        InteractablesInSight = new List<GameObject>();
        _cinemachine = _playerData.Camera.GetComponent<CinemachineBrain>();
    }

    private void Update()
    {
        if (_gameSettings == null || _gameSettings != null && !_gameSettings.Pause)
        {
            if (!PlayerInspect.Inspecting())
            {
                CameraControl();
                PlayerMovement();
                PlayerAudioControl();
                FlashlightControl();
                ItemInteraction();
                InventoryManage();
            }

            ManageInspection();
            PlayerStressControl();
        }

        PlayerCameraRotation();

        if(FreezePlayerRotation)
        {
            CinemachinePOV vCamCinemachinePOV = _playerData.VirtualCamera.GetCinemachineComponent<CinemachinePOV>();
            vCamCinemachinePOV.m_HorizontalAxis.m_MaxSpeed = 0;
            vCamCinemachinePOV.m_VerticalAxis.m_MaxSpeed = 0;
        }
    }

    private void PlayerMovement()
    {
        if (!FreezePlayerMovement)
            _playerMovement.PlayerMove(Player, _playerInput);
    }

    private void PlayerCameraRotation()
    {
        if(!FreezePlayerRotation)
            _playerRotate.RotatePlayer(_playerData, _playerInput, PlayerInspect.Inspecting());
    }

    private void CameraControl()
    {
        _playerCameraControl.ControlCameraHeadBob(Player, _playerInput);
    }

    private void PlayerAudioControl()
    {
        _playerAudio.PlayerAudioControl(_playerData, _playerInput);
    }

    private void PlayerStressControl()
    {
        StressControl.ManageStress();
    }

    private void FlashlightControl()
    {
        PlayerFlashlight.FlashlightControl(_playerData, _playerInput);
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
        _ray.direction = _playerData.Camera.forward;

        if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
        {
            _playerUI.InteractableUI(_playerData, hit); //Activates UI elements when hovering over interactables
            _playerInteract.Interact(_playerData, hit, _playerInput, PlayerInspect);
        }

        _playerUI.CenterPointControl(InteractablesInSight); //Controls the center point appearing and disapearing when interactables in cammera view
    }
}