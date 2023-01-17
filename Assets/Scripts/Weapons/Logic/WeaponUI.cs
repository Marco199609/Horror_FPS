using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public void UIUpdate(WeaponGeneralData weaponGeneralData, int currentAmmo, int reserveCapacity, TextMeshProUGUI ammoText)
    {
        ammoText.text = reserveCapacity.ToString();

        for(int i = 0; i < weaponGeneralData.BulletImages.Length; i++)
        {
            if(currentAmmo > i)
            {
                weaponGeneralData.BulletImages[i].color = new Color(1, 1, 1, 0.16f);
            }
            else
                weaponGeneralData.BulletImages[i].color = new Color(1, 1, 1, 0.03f);
        }
    }
}
