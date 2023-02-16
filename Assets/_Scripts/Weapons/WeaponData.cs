using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponData : MonoBehaviour, IInteractable
{
    [field: SerializeField] public Weapon Weapon { get; private set; }
    [field: SerializeField] public AudioSource ShotSound { get; private set; }
    [field: SerializeField] public AudioSource ReloadSound { get; private set; }
    [field: SerializeField] public GameObject WeaponModel { get; private set; }

    #region Pickup Interaction
    public void Interact()
    {
        ObjectManager.Instance.InventoryController.AddWeapon(this, gameObject);
        PlaceWeaponOnHolder();

    }
    private void PlaceWeaponOnHolder()
    {
        WeaponGeneralData weaponGeneralData = ObjectManager.Instance.WeaponGeneralData;

        if (GetComponent<Collider>() != false) GetComponent<Collider>().enabled = false;

        //Place weapon in weapon holder
        transform.SetParent(weaponGeneralData.transform);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, 0));
        transform.localScale = Vector3.one; //Scale not set to one until the player gets the weapon, to make level design easier
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
        return Weapon.WeaponDescription;
    }

    private void OnBecameVisible()
    {
        ObjectManager.Instance.PlayerController.InteractablesInSight.Add(this.gameObject); //Must contain a mesh renderer to work
    }

    private void OnBecameInvisible()
    {
        ObjectManager.Instance.PlayerController.InteractablesInSight.Remove(this.gameObject); //Must contain a mesh renderer to work
    }


}
