using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSound : MonoBehaviour, IWeaponSound
{
    private void OnEnable()
    {
        WeaponShoot.WeaponShot += ShootSound;
    }

    private void OnDisable()
    {
        WeaponShoot.WeaponShot -= ShootSound;
    }

    public void ShootSound(WeaponData weaponData, EnemyData enemyData)
    {
        if (weaponData.ShotSound.isPlaying)
            weaponData.ShotSound.Stop();
        weaponData.ShotSound.Play();
    }
}
