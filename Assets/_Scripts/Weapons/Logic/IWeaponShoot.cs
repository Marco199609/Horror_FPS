using UnityEngine;

public interface IWeaponShoot
{
    void Shoot(WeaponInput weaponInput, WeaponData weaponData, EnemyData enemyData);
}