using UnityEngine;

public interface IPlayerInteract
{
    void InteractWithObject(GameObject player, RaycastHit hit, PlayerInput playerInput);
}