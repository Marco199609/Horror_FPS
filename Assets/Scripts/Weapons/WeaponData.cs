using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    /*
     * Weapon damage: Typical amount of damage the weapon inflicts when it hits the target.
     * Armor Penetration: Some enemies are given a defense stat that reduces or negates damage, and being able to perform an Armor-Piercing Attack can be important.
     * Weapon Range: Maximum distance that can exist between user and opponent and still hit them with it. Measured in meters.
     * Fire rate: For ranged weapons, how many shots the weapon fires in a given period of time. Measured in seconds.
     * Reload speed: How quickly the next projectile(s) is loaded into the weapon. Measured in seconds.
     * Switching Speed: How long it takes to switch to and/or from another weapon. Measured in seconds.
     * Weight: How much a weapon affects your character's movement speed. Measured in grams.
     * Magazine capacity: Number of attacks possible before a reload is required.
     * Reserve capacity: Number of rounds of ammo or number of replacement clips that can be carried at one time.
     * Is Auto: Defines if the weapon is automatic or manual.
    */

    public float armorPenetration, fireRate, reloadSpeed, switchingSpeed, weight;
    public int weaponDamage, weaponRange, magazineCapacity, currentAmmo, reserveCapacity;

    public bool isAuto;
    public AudioSource shotSound, reloadSound;
}
