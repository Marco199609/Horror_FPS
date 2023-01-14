using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponGeneralData : MonoBehaviour
{
    [Header("Weapon Shoot")]
    public Transform shootRayOrigin;

    [Header("Weapon UI")]
    public GameObject weaponUICanvas;
    public TextMeshProUGUI ammoText;

    //Available weapons; weapons[0] is weapon not active.
    [Header("Weapons Available")]
    public GameObject[] WeaponsAvailable;

    private void Awake()
    {
        ObjectManager.Instance.WeaponGeneralData = this;
    }
}
