using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour, IPlayerUI
{
    //Interactable UI variables
    private bool _playerUIActivated = true;
    private string _interactableDescription;

    public delegate void UpdateItemDescription(string description);
    public static event UpdateItemDescription ItemDescriptionActivated;
    public static event UpdateItemDescription ItemDescriptionDeactivated;

    //Center point variables
    private float _transparency, _previousTransparency, _maxTransparency = 0.08f, _changeSpeed = 0.1f; 
    private Color _centerPointColor;

    public delegate void UpdateCenterPoint(Color color);
    public static event UpdateCenterPoint CenterPointUpdated;

    public void InteractableUI(PlayerData playerData, RaycastHit hit)
    {
        if (hit.distance <= playerData.itemPickupDistance && hit.collider.GetComponent<IInteractable>() != null) //Checks if item interactable and reachable
        {
            if (!_playerUIActivated)
            {
                _interactableDescription = hit.transform.gameObject.GetComponent<IInteractable>().Description(); //Updates item description
                ItemDescriptionActivated?.Invoke(_interactableDescription); //Passes description to UI manager
                _playerUIActivated = true;
            }

            /*If ray passes from one interactable to another without a gap,
              it could cause the UI not to update.
              Do not place interactables too close to each other*/
        }
        else
        {
            if (_playerUIActivated)
            {
                _interactableDescription = ""; //Resets item description
                ItemDescriptionDeactivated?.Invoke(_interactableDescription); //Passes description to UI manager
                _playerUIActivated = false;
            }
        }
    }

    //If an interactable is in camera view, it activates the center point as a hint
    public void CenterPointControl(List<GameObject> itemsVisible)
    {
        if(itemsVisible.Count > 0 && _transparency < _maxTransparency) 
            _transparency += _changeSpeed * 2 * Time.deltaTime;
        else if(itemsVisible.Count <= 0 && _transparency > 0) 
            _transparency -= _changeSpeed * Time.deltaTime;

        Mathf.Clamp(_transparency, 0f, _maxTransparency);

        //Prevents color from being passed to the UI manager when not necessary
        if (_transparency != _previousTransparency)
        {
            _centerPointColor = new Color(1, 1, 1, _transparency);

            CenterPointUpdated?.Invoke(_centerPointColor); //Passes new color to UI manager

            _previousTransparency = _transparency; 
        }
    }
}