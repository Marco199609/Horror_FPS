using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{
    public WeaponData currentWeaponData { get; private set; }

    public void ChangeWeapon(GameObject[] weapons, WeaponInput weaponInput)
    {
        if(currentWeaponData == null)
        {
            SetDefaultNoWeapon(weapons);
        }

        if(weaponInput.weaponIndexInput >= 0 && (weaponInput.weaponIndexInput) < weapons.Length) //Checks if weapon index is in array bounds
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
                    else
                        weapons[i].SetActive(false);
                }
                else if (weapons[i-1] == null) //Sets weapon 0 only if no weapon on both slots
                {
                    SetDefaultNoWeapon(weapons);
                }

            }
        }
    }

    private void SetDefaultNoWeapon(GameObject[] weapons)
    {
        weapons[0].SetActive(true);
        currentWeaponData = weapons[0].GetComponent<WeaponData>();
    }
}
