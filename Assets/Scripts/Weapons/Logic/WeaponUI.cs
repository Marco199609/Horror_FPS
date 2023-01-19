using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    public void UIUpdate(WeaponGeneralData weaponGeneralData, WeaponData currentWeaponData, TextMeshProUGUI ammoText, 
        WeaponController weaponController, GameObject weaponUICanvas)
    {
        if(weaponController.isWeaponActive && !ObjectManager.Instance.InventoryController.IsInventoryEnabled)
        {
            if (!weaponUICanvas.activeInHierarchy)
                weaponUICanvas.SetActive(true);

            ammoText.text = currentWeaponData.CurrentReserveCapacity.ToString();
            weaponGeneralData.weaponUIIcon.GetComponent<Image>().sprite = currentWeaponData.UIIcon;

            for (int i = 0; i < weaponGeneralData.BulletImages.Length; i++)
            {
                if (currentWeaponData.currentAmmo > i)
                    weaponGeneralData.BulletImages[i].color = new Color(1, 1, 1, 0.16f);
                else
                    weaponGeneralData.BulletImages[i].color = new Color(1, 1, 1, 0.03f);
            }
        }
        else if(!weaponController.isWeaponActive || ObjectManager.Instance.InventoryController.IsInventoryEnabled)
            weaponUICanvas.SetActive(false);
    }
}
