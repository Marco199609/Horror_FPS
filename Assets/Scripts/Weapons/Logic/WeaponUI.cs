using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public void UIUpdate(int currentAmmo, int reserveCapacity, TextMeshProUGUI ammoText)
    {
        ammoText.text = "Ammo: " + currentAmmo + "/" + reserveCapacity;
    }
}
