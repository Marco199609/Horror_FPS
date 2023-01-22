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
        if (!ObjectManager.Instance.GameController.InteractableDescription.gameObject.activeInHierarchy)
            ObjectManager.Instance.GameController.InteractableDescription.gameObject.SetActive(true);
    }

    public void Hover(GameObject player, RaycastHit hit, GameController gameController)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        if (hit.distance <= _playerData.itemPickupDistance && hit.collider.GetComponent<IInteractable>() != null) //Checks if item interactable and reachable
        {
            ActivateUIPickupHand();
            ShowCustomDescription(hit, gameController);
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
        if (_description == "") _description = hit.transform.gameObject.GetComponent<IInteractable>().Description();

        if (gameController.InteractableDescription.text == "")
            gameController.InteractableDescription.text = _description;
    }

    private void DeactivateUIElements(GameObject player, GameController gameController)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        if (_playerData.UIPickupHand.gameObject.activeInHierarchy) _playerData.UIPickupHand.gameObject.SetActive(false);

        if (!_playerData.UICenterPoint.gameObject.activeInHierarchy) _playerData.UICenterPoint.gameObject.SetActive(true);

        if (_description != "")  _description = "";

        if (gameController.InteractableDescription.text != _description) gameController.InteractableDescription.text = _description;
    }
}
