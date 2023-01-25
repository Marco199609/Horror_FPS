using UnityEngine;

public interface IWeaponChange
{
    WeaponData currentWeaponData { get; }

    void ChangeWeapon(GameObject[] weapons, WeaponInput weaponInput);
}