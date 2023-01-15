using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private bool _activateInventoryPanel;
    public void ShowInventoryUI(GameController gameController, InventoryInput inventoryInput, InventoryController inventoryController)
    {
        if(inventoryInput.OpenInventory)
        {
            _activateInventoryPanel = _activateInventoryPanel ? false : true;
            inventoryController.IsInventoryEnabled = inventoryController.IsInventoryEnabled ? false : true;
        }

        gameController.InventoryPanel.SetActive(_activateInventoryPanel);
    }
}
