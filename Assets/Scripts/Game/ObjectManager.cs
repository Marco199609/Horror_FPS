using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance = null;

    [Header("Data Managers")]
    public PlayerData PlayerData;
    public WeaponGeneralData WeaponGeneralData;

    [Header("Game Controller")]
    public InventoryController InventoryController;
    public PlayerInput PlayerInput;
    public PlayerController PlayerController;
    public WeaponInput WeaponInput;

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
