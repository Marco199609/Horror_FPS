using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private GameObject _selectedItem;
    [NonSerialized] public int Index;

    public GameObject Manage(List<GameObject> inventory, IPlayerInput playerInput)
    {
        if (playerInput.MouseScrollInput != 0)
        {
            if (Index < inventory.Count - 1 && inventory.Count > 1)
            {
                Index++;
            } 
            else Index = 0;

            _selectedItem = inventory[Index];

            for (int i = 0; i < inventory.Count; i++)
            {
                if(inventory[i] == _selectedItem && inventory[i] != null) _selectedItem.SetActive(true);
                else if (inventory[i] != null) inventory[i].SetActive(false);
            }
        }
        else if (Index > inventory.Count)
        {
            Index = 0;
            _selectedItem = inventory[Index];
        }
        else if(_selectedItem != inventory[Index]) _selectedItem = inventory[Index];

        return _selectedItem;
    }
}
