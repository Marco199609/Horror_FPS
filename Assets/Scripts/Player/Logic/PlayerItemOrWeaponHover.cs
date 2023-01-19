using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerItemOrWeaponHover : MonoBehaviour, IPlayerUIHover
{
    string description;
    private void Start()
    {
        if (!ObjectManager.Instance.GameController.ItemOrWeaponDescription.gameObject.activeInHierarchy)
            ObjectManager.Instance.GameController.ItemOrWeaponDescription.gameObject.SetActive(true);
    }

    public void HoverOverItem(RaycastHit hit, GameObject player, GameController gameController)
    {
        ActivateUIPickupHand(playerData);



        ShowCustomDescription(hit, gameController);
    }

    void ShowCustomDescription(RaycastHit hit, GameController gameController)
    {
        //Checks if gameobject is item or weapon
        if (hit.collider.CompareTag("Item") && description == "") description = hit.transform.gameObject.GetComponent<ItemData>().Item.description;
        else if (hit.collider.CompareTag("Weapon") && description == "") description = hit.transform.gameObject.GetComponent<WeaponData>().WeaponDescription;

        if (gameController.ItemOrWeaponDescription.text == "")
            gameController.ItemOrWeaponDescription.text = description;
    }

    private void ActivateUIPickupHand(GameObject player)
    {
        playerData.UIPickupHand.gameObject.SetActive(true);
        playerData.UICenterPoint.gameObject.SetActive(false);
    }

    public void DeactivateUIElements(PlayerData playerData, GameController gameController)
    {
        if (playerData.UIPickupHand.gameObject.activeInHierarchy)
            playerData.UIPickupHand.gameObject.SetActive(false);

        if (!playerData.UICenterPoint.gameObject.activeInHierarchy)
            playerData.UICenterPoint.gameObject.SetActive(true);

        if (description != "")
            description = "";

        if (gameController.ItemOrWeaponDescription.text != description)
            gameController.ItemOrWeaponDescription.text = description;
    }
}
