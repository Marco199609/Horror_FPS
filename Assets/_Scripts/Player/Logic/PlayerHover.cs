using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHover : MonoBehaviour, IPlayerUIHover
{
    private PlayerData _playerData;
    private string _description;
    private void Start()
    {
        if (!ObjectManager.Instance.GameController.ItemOrWeaponDescription.gameObject.activeInHierarchy)
            ObjectManager.Instance.GameController.ItemOrWeaponDescription.gameObject.SetActive(true);
    }

    public void Hover(GameObject player, RaycastHit hit, GameController gameController)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        if (hit.distance <= _playerData.itemPickupDistance) //Checks if item reachable
        {
            if (hit.collider.CompareTag("Item") || hit.collider.CompareTag("Weapon"))
            {
                ActivateUIPickupHand();
                ShowCustomDescription(hit, gameController);
            }
        }
        else
        {
            DeactivateUIElements(player, gameController);
        }
    }

    private void ActivateUIPickupHand()
    {
        _playerData.UIPickupHand.gameObject.SetActive(true);
        _playerData.UICenterPoint.gameObject.SetActive(false);
    }

    void ShowCustomDescription(RaycastHit hit, GameController gameController)
    {
        //Checks if gameobject is item or weapon
        if (hit.collider.CompareTag("Item") && _description == "") _description = hit.transform.gameObject.GetComponent<ItemData>().Item.description;
        else if (hit.collider.CompareTag("Weapon") && _description == "") _description = hit.transform.gameObject.GetComponent<WeaponData>().WeaponDescription;

        if (gameController.ItemOrWeaponDescription.text == "")
            gameController.ItemOrWeaponDescription.text = _description;
    }

    private void DeactivateUIElements(GameObject player, GameController gameController)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        if (_playerData.UIPickupHand.gameObject.activeInHierarchy) _playerData.UIPickupHand.gameObject.SetActive(false);

        if (!_playerData.UICenterPoint.gameObject.activeInHierarchy) _playerData.UICenterPoint.gameObject.SetActive(true);

        if (_description != "")  _description = "";

        if (gameController.ItemOrWeaponDescription.text != _description) gameController.ItemOrWeaponDescription.text = _description;
    }
}
