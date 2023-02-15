using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHover : MonoBehaviour, IPlayerUIHover
{
    private PlayerData _playerData;

    public delegate void UpdateUI(RaycastHit hit);
    public static event UpdateUI UIActivated;
    public static event UpdateUI UIDeactivated;

    public void Hover(GameObject player, RaycastHit hit, GameController gameController)
    {
        if (_playerData == null) _playerData = player.GetComponent<PlayerData>();

        if (hit.distance <= _playerData.itemPickupDistance && hit.collider.GetComponent<IInteractable>() != null) //Checks if item interactable and reachable
        {
            UIActivated?.Invoke(hit);
        }
        else
        {
            UIDeactivated?.Invoke(hit);
        }
    }
}
