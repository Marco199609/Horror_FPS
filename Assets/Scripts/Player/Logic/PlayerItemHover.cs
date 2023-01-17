using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerItemHover : MonoBehaviour
{
    private void Start()
    {
        if(!ObjectManager.Instance.GameController.CustomItemMessage.gameObject.activeInHierarchy)
            ObjectManager.Instance.GameController.CustomItemMessage.gameObject.SetActive(true);
    }

    public void HoverOverItem(RaycastHit hit, PlayerData playerData, GameController gameController)
    {
        ActivateUIPickupHand(playerData);
        ShowCustomItemMessagage(hit.transform.gameObject.GetComponent<ItemData>(), gameController);
    }

    void ShowCustomItemMessagage(ItemData _itemData, GameController gameController)
    {
        if (gameController.CustomItemMessage.text == "")
            gameController.CustomItemMessage.text = _itemData.Item.message;
    }

    private void ActivateUIPickupHand(PlayerData playerData)
    {
        playerData.UIPickupHand.gameObject.SetActive(true);
        playerData.UICenterPoint.gameObject.SetActive(false);
    }

    public void DeactivateUIElements(PlayerData playerData, GameController gameController)
    {
        if(playerData.UIPickupHand.gameObject.activeInHierarchy)
            playerData.UIPickupHand.gameObject.SetActive(false);

        if(!playerData.UICenterPoint.gameObject.activeInHierarchy)
            playerData.UICenterPoint.gameObject.SetActive(true);

        if (gameController.CustomItemMessage.text != "")
            gameController.CustomItemMessage.text = "";
    }
}
