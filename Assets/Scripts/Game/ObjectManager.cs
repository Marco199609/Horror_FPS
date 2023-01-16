using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance = null;

    [Header("Data Managers")]
    public PlayerData PlayerData;
    public WeaponGeneralData WeaponGeneralData;

    [Header("Game Controllers")]
    public GameController GameController;
    public InventoryController InventoryController;
    public PlayerController PlayerController;
    public WeaponController WeaponController;

    [Header("Input Managers")]
    public PlayerInput PlayerInput;
    public WeaponInput WeaponInput;
    public InventoryInput InventoryInput;

    [Header("Game Objects")]
    public GameObject Player;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }
}