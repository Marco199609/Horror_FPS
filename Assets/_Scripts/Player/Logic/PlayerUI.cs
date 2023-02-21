using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IPlayerUI
{
    //Interactable UI variables
    private bool _playerUIActive = true, _itemsCurrentlyVisible;
    private string _interactableDescription, _blankInteractableDescription;

    public delegate void UpdateItemDescription(string description);
    public static event UpdateItemDescription ItemDescriptionActivated;
    public static event UpdateItemDescription ItemDescriptionReset;


    //Center UI point variables
    public delegate void UpdateCenterPoint(bool ItemsVisible);
    public static event UpdateCenterPoint ItemsBecameVisible;

    public void InteractableUI(PlayerData playerData, RaycastHit interactable)
    {
        if (interactable.distance <= playerData.itemPickupDistance && interactable.collider.GetComponent<IInteractable>() != null) 
        {
            if (!_playerUIActive)
            {
                _interactableDescription = interactable.collider.GetComponent<IInteractable>().Description(); //Updates item description
                ItemDescriptionActivated?.Invoke(_interactableDescription); //Passes description to UI manager
                _playerUIActive = true;
            }

            /*If ray passes from one interactable to another without a gap,
              it could cause the UI not to update.
              Do not place interactables too close to each other*/
        }
        else
        {
            if (_playerUIActive)
            {
                ItemDescriptionReset?.Invoke(_blankInteractableDescription);
                _playerUIActive = false;
            }
        }
    }

    //If an interactable is in camera view, it activates the center point as a hint
    public void CenterPointControl(List<GameObject> itemsVisible)
    {
        if (itemsVisible.Count > 0 && !_itemsCurrentlyVisible)
        {
            _itemsCurrentlyVisible = true;
            ItemsBecameVisible?.Invoke(_itemsCurrentlyVisible);
        }
        else if (itemsVisible.Count <= 0 && _itemsCurrentlyVisible)
        {
            _itemsCurrentlyVisible = false;
            ItemsBecameVisible?.Invoke(_itemsCurrentlyVisible);
        }
    }
}