using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IPlayerUI
{
    void InteractableUI(PlayerData playerData, RaycastHit hit);
    void CenterPointControl(List<GameObject> itemsVisible);
}

public class PlayerUI : MonoBehaviour, IPlayerUI
{
    //Interactable UI variables
    private bool _playerUIActive = true, _itemsCurrentlyVisible;
    private string _blankText;

    public delegate void UpdateItemDescription(bool nonInspectable, bool inspectableOnly);
    public static event UpdateItemDescription ItemDescriptionActivated;
    public static event UpdateItemDescription ItemDescriptionReset;


    //Center UI point variables
    public delegate void UpdateCenterPoint(bool ItemsVisible);
    public static event UpdateCenterPoint ItemsBecameVisible;

    public void InteractableUI(PlayerData playerData, RaycastHit interactableHit)
    {
        if (interactableHit.distance <= playerData.InteractDistance && interactableHit.collider.GetComponent<IInteractable>() != null) 
        {
            if (!_playerUIActive)
            {
                IInteractable interactable = interactableHit.collider.GetComponent<IInteractable>();

                ItemDescriptionActivated?.Invoke(interactable.InteractableType()[0], interactable.InteractableType()[1]);//Index 1 is Inspectable only; passes description to UI manager
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
                ItemDescriptionReset?.Invoke(false, false);
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