using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance = null;

    [Header("Data Managers")]
    public PlayerData PlayerData;

    [Header("Game Controllers")]
    public GameController GameController;
    public PlayerController PlayerController;

    [Header("Input Managers")]
    public PlayerInput PlayerInput;

    [Header("Player Scripts")]
    public PlayerFlashlight PlayerFlashlight;

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
