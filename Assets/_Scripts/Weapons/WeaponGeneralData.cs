using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponGeneralData : MonoBehaviour
{
    [field: SerializeField, Header("Weapon Shoot")] public Transform ShootRayOrigin { get; private set; }

    //Available weapons; weapons[0] is weapon not active.
    [Header("Weapons Available")]
    public GameObject[] WeaponsAvailable;

    private void Awake()
    {
        if(ObjectManager.Instance != null) ObjectManager.Instance.WeaponGeneralData = this;
    }
}
