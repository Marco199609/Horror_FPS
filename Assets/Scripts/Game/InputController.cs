using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Components Required
[RequireComponent(typeof(WeaponInput))]
[RequireComponent(typeof(PlayerInput))]
#endregion

public class InputController : MonoBehaviour
{
    [SerializeField] private WeaponController weaponController;
    [SerializeField] private WeaponInput weaponInput;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerInput playerInput;


    void Update()
    {
    }
}
