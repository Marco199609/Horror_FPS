using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGeneralData : MonoBehaviour
{
    [Header("Weapon Shoot")]
    public Transform shootRayOrigin;

    [Header("Weapon UI")]
    public GameObject weaponUICanvas;
    public GameObject weaponUIIcon;
    public TextMeshProUGUI ammoText;
    public Image[] BulletImages;

    //Available weapons; weapons[0] is weapon not active.
    [Header("Weapons Available")]
    public GameObject[] WeaponsAvailable;
    [SerializeField] private GameObject weapon0; //Weapon 0 is no weapon

    private void Awake()
    {
        WeaponsAvailable = new GameObject[3];
        WeaponsAvailable[0] = weapon0;
        ObjectManager.Instance.WeaponGeneralData = this;
    }
}
