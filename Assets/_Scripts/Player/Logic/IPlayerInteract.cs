using UnityEngine;

public interface IPlayerInteract
{
    void Interact(PlayerData playerData, RaycastHit hit, IPlayerInput playerInput, IPlayerInspect playerInspect);
}