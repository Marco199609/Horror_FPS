using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour, IWeaponSound
{
    public void ShootSound(WeaponData weaponData)
    {
        if (weaponData.ShotSound.isPlaying)
            weaponData.ShotSound.Stop();
        weaponData.ShotSound.Play();
    }
}
