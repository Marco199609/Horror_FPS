using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour, IWeaponUI
{
    private GameObject _weaponUICanvas;
    private TextMeshProUGUI _ammoText;
    public void UIUpdate(WeaponGeneralData weaponGeneralData, WeaponData CurrentWeaponData, WeaponController weaponController)
    {
        Weapon currentWeapon = CurrentWeaponData.Weapon;

        if (_weaponUICanvas == null) _weaponUICanvas = weaponGeneralData.WeaponUICanvas;
        if (_ammoText == null) _ammoText = weaponGeneralData.AmmoText;

        if (weaponController.IsWeaponActive && !ObjectManager.Instance.InventoryController.IsInventoryEnabled)
        {
            if (!_weaponUICanvas.activeInHierarchy) _weaponUICanvas.SetActive(true);

            _ammoText.text = currentWeapon.CurrentReserveCapacity.ToString(); //Tells the player how much ammo left
            weaponGeneralData.WeaponUIIcon.GetComponent<Image>().sprite = currentWeapon.UIIcon;

            //Updates bullet images on the ui
            for (int i = 0; i < weaponGeneralData.BulletImages.Length; i++)
            {
                if (currentWeapon.CurrentAmmo > i) weaponGeneralData.BulletImages[i].color = new Color(1, 1, 1, 0.16f); //Sets opacity of the available bullets
                else weaponGeneralData.BulletImages[i].color = new Color(1, 1, 1, 0.03f); //Sets opacity of used bullets
            }
        }
        else if (!weaponController.IsWeaponActive || ObjectManager.Instance.InventoryController.IsInventoryEnabled) _weaponUICanvas.SetActive(false);
    }

    public void CrosshairActivate(WeaponGeneralData weaponGeneralData)
    {
        weaponGeneralData.Crosshair.color = new Color(1, 0, 0, 0.08f); //Red
    }

    public void CrosshairDeactivate(WeaponGeneralData weaponGeneralData)
    {
        weaponGeneralData.Crosshair.color = new Color(1, 1, 1, 0.08f); //White
    }
}
