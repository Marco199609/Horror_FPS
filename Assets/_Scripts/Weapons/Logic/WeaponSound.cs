using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour
{
    public void ShootSound(WeaponData weaponData)
    {
        if (weaponData.shotSound.isPlaying)
            weaponData.shotSound.Stop();
        weaponData.shotSound.Play();
    }
}
