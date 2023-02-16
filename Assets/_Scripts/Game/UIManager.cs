using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] private Image _uiPickupHand;
    [SerializeField] private Image _uiCenterPoint;
    [SerializeField] private TextMeshProUGUI _interactableDescription;

    [Header("Weapon UI")]
    [SerializeField] private GameObject _weaponUICanvas;
    [SerializeField] private GameObject _ammoUI;
    [SerializeField] private GameObject _weaponUIIcon;
    [SerializeField] private TextMeshProUGUI _ammoReserveText;
    [SerializeField] private Image _crosshair;
    [SerializeField] private Image[] _bulletImages;


    private void OnEnable()
    {
        //Subscribes to player UI events
        PlayerUI.ItemDescriptionActivated += ActivatePlayerUIElements;
        PlayerUI.ItemDescriptionDeactivated += DeactivatePlayerUIElements;
        PlayerUI.CenterPointUpdated += CenterPointColorUpdate;

        //Subscribes to weapon UI events
        WeaponUI.weaponUIUpdated += WeaponUIUpdate;
        WeaponUI.CrosshairColorUpdated += WeaponCrosshairColorUpdate;
        WeaponReload.AmmoUIActivated += AmmoUIUpdate;
    }

    private void OnDisable()
    {
        //Unsubscribes from player Ui events
        PlayerUI.ItemDescriptionActivated -= ActivatePlayerUIElements;
        PlayerUI.ItemDescriptionDeactivated -= DeactivatePlayerUIElements;
        PlayerUI.CenterPointUpdated -= CenterPointColorUpdate;

        //Unsubscribes from weapon UI events
        WeaponUI.weaponUIUpdated -= WeaponUIUpdate;
        WeaponUI.CrosshairColorUpdated -= WeaponCrosshairColorUpdate;
        WeaponReload.AmmoUIActivated -= AmmoUIUpdate;
    }

    void ActivatePlayerUIElements(string description)
    {
        //Updates item description
        _interactableDescription.text = description;

        //Activate hand and deactivate center point
        _uiPickupHand.gameObject.SetActive(true);
        _uiCenterPoint.gameObject.SetActive(false);
    }

    private void DeactivatePlayerUIElements(string description)
    {
        //Blanks item description
        _interactableDescription.text = description;

        //Deactivate hand and activate center point
        _uiPickupHand.gameObject.SetActive(false);
        _uiCenterPoint.gameObject.SetActive(true);
    }

    //Updates center point when an interactable is in camera view
    private void CenterPointColorUpdate(Color color)
    {
        _uiCenterPoint.color = color; 
    }

    private void WeaponUIUpdate(int currentAmmo, int currentAmmoReserve, Sprite weaponUIIcon, bool activateWeaponCanvas)
    {
        if (_weaponUICanvas.activeInHierarchy != activateWeaponCanvas) _weaponUICanvas.SetActive(activateWeaponCanvas); //Activates or deactivates waepon canvas

        _ammoReserveText.text = currentAmmoReserve.ToString(); //Tells the player how much ammo left
        _weaponUIIcon.GetComponent<Image>().sprite = weaponUIIcon;

        //Updates bullet images on the ui
        for (int i = 0; i < _bulletImages.Length; i++)
        {
            if (currentAmmo > i) _bulletImages[i].color = new Color(1, 1, 1, 0.16f); //Sets opacity of the available bullets
            else _bulletImages[i].color = new Color(1, 1, 1, 0.03f); //Sets opacity of used bullets
        }
    }

    private void WeaponCrosshairColorUpdate(Color color)
    {
        _crosshair.color = color;
    }

    private void AmmoUIUpdate(bool activateAmmoUI)
    {
        _ammoUI.SetActive(activateAmmoUI);
    }
}
