using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInventory
{
    void Manage(PlayerData playerData, IPlayerInput playerInput);
    void Add(GameObject interactable, Vector3 positionInInventory, Vector3 rotationInInventory);
    void Remove(GameObject interactable);
    public GameObject SelectedItem();
}