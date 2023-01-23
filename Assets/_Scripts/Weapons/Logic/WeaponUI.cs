using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    private Ray _ray;
    public void UIUpdate(WeaponGeneralData weaponGeneralData, WeaponData currentWeaponData, TextMeshProUGUI ammoText, 
        WeaponController weaponController, GameObject weaponUICanvas)
    {
        if(weaponController.isWeaponActive && !ObjectManager.Instance.InventoryController.IsInventoryEnabled)
        {
            if (!weaponUICanvas.activeInHierarchy) weaponUICanvas.SetActive(true);

            ammoText.text = currentWeaponData.CurrentReserveCapacity.ToString(); //Tells the player how much ammo left
            weaponGeneralData.weaponUIIcon.GetComponent<Image>().sprite = currentWeaponData.UIIcon;

            //Updates bullet images on the ui
            for (int i = 0; i < weaponGeneralData.BulletImages.Length; i++)
            {
                if (currentWeaponData.currentAmmo > i) weaponGeneralData.BulletImages[i].color = new Color(1, 1, 1, 0.16f); //Sets opacity of the available bullets
                else weaponGeneralData.BulletImages[i].color = new Color(1, 1, 1, 0.03f); //Sets opacity of used bullets
            }
        }
        else if(!weaponController.isWeaponActive || ObjectManager.Instance.InventoryController.IsInventoryEnabled)
            weaponUICanvas.SetActive(false);

        CrosshairActivate(weaponGeneralData, currentWeaponData, weaponController);
    }

    private void CrosshairActivate(WeaponGeneralData weaponGeneralData, WeaponData currentWeaponData, WeaponController weaponController)
    {
        if(weaponController.isWeaponActive)
        {
            RaycastHit hit;
            _ray.origin = Camera.main.ViewportToWorldPoint(weaponGeneralData.Crosshair.transform.position);
            _ray.direction = Camera.main.transform.forward;

            if (Physics.Raycast(_ray, out hit, currentWeaponData.weaponRange) && hit.collider.CompareTag("Enemy"))
            {
                weaponGeneralData.Crosshair.color = new Color(1, 0, 0, 0.08f); //Red
            }
            else weaponGeneralData.Crosshair.color = new Color(1, 1, 1, 0.08f); //White
        }
    }
}
