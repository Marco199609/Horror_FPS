﻿using UnityEngine;

public interface IWeaponDamage
{
    void DamageEnemy(WeaponData weaponData, RaycastHit hit);
}