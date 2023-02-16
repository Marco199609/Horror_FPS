using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json.Bson;

public class WeaponUI : MonoBehaviour, IWeaponUI
{
    private int _currentAmmo, _ammoCurrentReserve;
    private bool _weaponCanvasActive, _previousEnemyState;
    private Color _crosshairColor;

    public delegate void WeaponUIEventHandler(int currentAmmo, int currentAmmoReserve, Sprite weaponUIIcon, bool activateWeaponCanvas);
    public static WeaponUIEventHandler weaponUIUpdated;

    public delegate void UICrosshairColorHandler(Color color);
    public static UICrosshairColorHandler CrosshairColorUpdated;

    public void UIUpdate(WeaponData CurrentWeaponData, WeaponController weaponController)
    {
        Weapon currentWeapon = CurrentWeaponData.Weapon;

        if (weaponController.IsWeaponActive && !ObjectManager.Instance.InventoryController.IsInventoryEnabled)
        {
            if (!_weaponCanvasActive)
            {
                _weaponCanvasActive = true;
            }

            _currentAmmo = currentWeapon.CurrentAmmo;
            _ammoCurrentReserve = currentWeapon.CurrentReserveCapacity; //Tells the player how much ammo left
        }
        else if (!weaponController.IsWeaponActive || ObjectManager.Instance.InventoryController.IsInventoryEnabled)
        {
            if (_weaponCanvasActive)
            {
                _weaponCanvasActive = false;
            }
        }

        weaponUIUpdated?.Invoke(_currentAmmo, _ammoCurrentReserve, currentWeapon.UIIcon, _weaponCanvasActive);
    }

    public void CrosshairColorUpdate(bool enemyInRange)
    {
        //Prevents crosshair update when unnecessary
        if(enemyInRange != _previousEnemyState)
        {
            if (enemyInRange) _crosshairColor = new Color(1, 0, 0, 0.08f); //Red
            else _crosshairColor = new Color(1, 1, 1, 0.08f); //White

            CrosshairColorUpdated?.Invoke(_crosshairColor);

            _previousEnemyState = enemyInRange;
        }
    }
}
