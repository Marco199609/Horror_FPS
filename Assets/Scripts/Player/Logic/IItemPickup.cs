using UnityEngine;

public interface IPlayerPickup
{
    void Pickup(GameObject player, RaycastHit hit, PlayerInput playerInput);
}