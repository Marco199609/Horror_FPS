using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    public void ShootSound(WeaponData weaponData)
    {
        if (weaponData.ShotSound.isPlaying)
            weaponData.ShotSound.Stop();
        weaponData.ShotSound.Play();
    }
}
