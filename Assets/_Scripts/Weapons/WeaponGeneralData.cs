using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGeneralData : MonoBehaviour
{
    [field: SerializeField, Header("Weapon Shoot")] public Transform ShootRayOrigin { get; private set; }
    [field: SerializeField, Header("Weapon UI")] public GameObject WeaponUICanvas { get; private set; }
    [field: SerializeField] public GameObject AmmoUI { get; private set; }
    [field: SerializeField] public GameObject WeaponUIIcon { get; private set; }
    [field: SerializeField] public TextMeshProUGUI AmmoText { get; private set; }
    [field: SerializeField] public Image Crosshair { get; private set; }
    [field: SerializeField] public Image[] BulletImages { get; private set; }

    //Available weapons; weapons[0] is weapon not active.
    [Header("Weapons Available")]
    public GameObject[] WeaponsAvailable;

    private void Awake()
    {
        if(ObjectManager.Instance != null) ObjectManager.Instance.WeaponGeneralData = this;
    }
}
