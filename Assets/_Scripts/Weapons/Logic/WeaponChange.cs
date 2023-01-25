using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour, IWeaponChange
{
    public WeaponData currentWeaponData { get; private set; }

    private int _currentWeaponIndex = 0;
    private bool _isSwitchingWeapon;

    public void ChangeWeapon(GameObject[] weapons, WeaponInput weaponInput)
    {
        if (currentWeaponData == null) SetDefaultNoWeapon(weapons[0]);

        ChangeWeaponWithNumberKeys(weapons, weaponInput);
        ChangeWeaponWithMouseScroll(weapons, weaponInput);
    }

    private void SetDefaultNoWeapon(GameObject noWeapon)
    {
        noWeapon.SetActive(true);
        currentWeaponData = noWeapon.GetComponent<WeaponData>();
    }

    private void ChangeWeaponWithMouseScroll(GameObject[] weapons, WeaponInput weaponInput)
    {
        if (weaponInput.mouseScrollInput > 0 && _isSwitchingWeapon == false && !ObjectManager.Instance.PlayerInput.FlashLightInput)
        {
            _currentWeaponIndex++;
            _isSwitchingWeapon = true;
        }
        else if (weaponInput.mouseScrollInput < 0 && _isSwitchingWeapon == false && !ObjectManager.Instance.PlayerInput.FlashLightInput)
        {
            _currentWeaponIndex--;
            _isSwitchingWeapon = true;
        }

        if (_currentWeaponIndex < 0) _currentWeaponIndex = weapons.Length;
        else if (_currentWeaponIndex > weapons.Length) _currentWeaponIndex = 0;

        //Change weapon
        if (_isSwitchingWeapon)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] != null)
                {
                    //Enables weapon selected and changes current weapon data for weapon controller
                    if (i == _currentWeaponIndex)
                    {
                        weapons[i].SetActive(true);
                        currentWeaponData = weapons[i].GetComponent<WeaponData>();
                    }
                    else weapons[i].SetActive(false);
                }
                else if (weapons[i - 1] == null) SetDefaultNoWeapon(weapons[0]); //Sets weapon 0 only if no weapon on both slots
            }
        }

        if (weaponInput.mouseScrollInput == 0) _isSwitchingWeapon = false;
    }

    private void ChangeWeaponWithNumberKeys(GameObject[] weapons, WeaponInput weaponInput)
    {
        if (weaponInput.weaponIndexInput >= 0 && (weaponInput.weaponIndexInput) < weapons.Length) //Checks if weapon index is in array bounds
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] != null)
                {
                    //Enables weapon selected and changes current weapon data for weapon controller
                    if (i == weaponInput.weaponIndexInput)
                    {
                        weapons[i].SetActive(true);
                        currentWeaponData = weapons[i].GetComponent<WeaponData>();
                    }
                    else weapons[i].SetActive(false);
                }
                else if (weapons[i - 1] == null) SetDefaultNoWeapon(weapons[0]); //Sets weapon 0 only if no weapon on both slots

            }
        }
    }
}
