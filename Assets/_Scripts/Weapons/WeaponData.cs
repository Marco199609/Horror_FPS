using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponData : MonoBehaviour, IInteractable
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
     * Shot sound: Shot audio source.
     * Reload sound: reload audio source.
     * Weapon UI Icon: Sprite that appears beside the ammo stats in the UI.
    */

    [field: SerializeField] public int weaponIndex; // 0 to 2: 0 is reserved for no weapon, 1 is for short weapon and 2 is for  long weapon

    [field: SerializeField] public bool isWeapon { get; private set; }

    [field: SerializeField] public float fireRate { get; private set; }
    [field: SerializeField] public float reloadSpeed { get; private set; }
    [field: SerializeField] public float switchingSpeed { get; private set; }
    [field: SerializeField] public float weight { get; private set; }

    [field: SerializeField] public int weaponDamage { get; private set; }
    [field: SerializeField] public int weaponRange { get; private set; }
    [field: SerializeField] public int magazineCapacity { get; private set; }
    [field: SerializeField] public int maxReserveCapacity { get; private set; }

    [field: SerializeField] public Sprite inventoryIcon { get; private set; }
    [field: SerializeField] public Sprite UIIcon { get; private set; }
    [field: SerializeField] public string WeaponDescription { get; private set; }

    public int currentAmmo;
    public int CurrentReserveCapacity;

    public AudioSource
        shotSound,
        reloadSound;

    [field: SerializeField] public GameObject WeaponModel { get; private set; }

    #region Pickup Interaction
    public void Interact()
    {
        ObjectManager.Instance.InventoryController.AddWeapon(this);
        PlaceWeaponOnHolder();

    }
    private void PlaceWeaponOnHolder()
    {
        WeaponGeneralData weaponGeneralData = ObjectManager.Instance.WeaponGeneralData;

        if (GetComponent<Collider>() != false) GetComponent<Collider>().enabled = false;

        //Place weapon in weapon holder
        transform.SetParent(weaponGeneralData.transform);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        gameObject.SetActive(false);

        ChangeWeaponLayer();
    }
    private void ChangeWeaponLayer()
    {
        gameObject.layer = 6; // 6 is weapon layer
        int childs = transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            transform.GetChild(i).gameObject.layer = 6;
        }
    }
    #endregion

    public string Description()
    {
        return WeaponDescription;
    }


    private void OnBecameVisible()
    {
        ObjectManager.Instance.PlayerController.ItemsVisible.Add(this.gameObject); //Must contain a mesh renderer to work
    }

    private void OnBecameInvisible()
    {
        ObjectManager.Instance.PlayerController.ItemsVisible.Remove(this.gameObject); //Must contain a mesh renderer to work
    }


}
