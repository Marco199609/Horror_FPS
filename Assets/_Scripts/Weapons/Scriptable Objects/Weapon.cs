using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : ScriptableObject
{
    /*
     * This script has private variables that are set in inspector, and public variables that return them. This is because Unity doesn't serialize properties in scriptable objects.
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
     * Shot sound: Shot audio source.
     * Reload sound: reload audio source.
     * Weapon UI Icon: Sprite that appears beside the ammo stats in the UI.
     */

    #region Private Serialized Variables

    [Header("General")]
    [SerializeField] private string _name;
    [SerializeField] private bool _isWeapon;
    [SerializeField] private string _weaponDescription;
    [SerializeField, Range(0, 2)] private int _weaponIndex; // 0 to 2: 0 is reserved for no weapon, 1 is for short weapon and 2 is for  long weapon
    [SerializeField] private GameObject _weaponModel;

    [Header("UI")]
    [SerializeField] private Sprite _inventoryIcon;
    [SerializeField] private Sprite _UIIcon;

    [Header("Audio")]
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private AudioSource _reloadSound;

    [Header("Atributes")]
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private int _maxReserveCapacity;
    [SerializeField] private float _fireRate;
    [SerializeField] private float  _reloadSpeed;
    [SerializeField] private float _switchingSpeed;
    [SerializeField] private float _weight;
    [SerializeField] private int _weaponDamage;
    [SerializeField] private int _weaponRange;

    #endregion

    #region Public Unserialized Variables

    //General
    public string Name { get => _name; set => _name = value; }
    public bool IsWeapon { get => _isWeapon; set => _isWeapon = value; }
    public string WeaponDescription { get => name; set => name = value; }
    public int WeaponIndex { get => _weaponIndex; set => _weaponIndex = value; } // 0 to 2: 0 is reserved for no weapon, 1 is for short weapon and 2 is for  long weapon
    public GameObject WeaponModel { get => _weaponModel; set => _weaponModel = value; }

    //UI 
    public Sprite InventoryIcon { get => _inventoryIcon; set => _inventoryIcon = value; }
    public Sprite UIIcon { get => _UIIcon; set => _UIIcon = value; }

    //Audio
    public AudioSource ShotSound { get => _shotSound; set => _shotSound = value; }
    public AudioSource ReloadSound { get => _reloadSound; set => _reloadSound = value; }

    //Attibutes
    public int MagazineCapacity { get => _magazineCapacity; set => _magazineCapacity = value; }
    public int MaxReserveCapacity { get => _maxReserveCapacity; set => _maxReserveCapacity = value; }
    public float FireRate { get => _fireRate; set => _fireRate = value; }
    public float ReloadSpeed { get => _reloadSpeed; set => _reloadSpeed = value; }
    public float SwitchingSpeed { get => _switchingSpeed; set => _switchingSpeed = value; }
    public float Weight { get => _weight; set => _weight = value; }
    public int WeaponDamage { get => _weaponDamage; set => _weaponDamage = value; }
    public int WeaponRange { get => _weaponRange; set => _weaponRange = value; }

    [Header("Public Variables")]
    public int CurrentAmmo;
    public int CurrentReserveCapacity;

    #endregion
}
