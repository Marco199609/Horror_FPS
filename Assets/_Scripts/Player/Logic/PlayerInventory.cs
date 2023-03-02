using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IPlayerInventory
{
    private GameObject _selectedItem;
    private List<GameObject> _inventory;
    private PlayerData _playerData;

    private int _currentSelectedItemIndex;

    private void Awake()
    {
        _inventory = new List<GameObject>();
        _inventory.Add(null);
    }

    public void Manage(PlayerData playerData, IPlayerInput playerInput)
    {
        if (_playerData == null) _playerData = playerData;

        if (playerInput.MouseScrollInput != 0)
        {
            if (_currentSelectedItemIndex < _inventory.Count - 1 && _inventory.Count > 1)
            {
                _currentSelectedItemIndex++;
            }
            else _currentSelectedItemIndex = 0;

            _selectedItem = _inventory[_currentSelectedItemIndex];

            for (int i = 0; i < _inventory.Count; i++)
            {
                if (_inventory[i] == _selectedItem && _inventory[i] != null) _selectedItem.SetActive(true);
                else if (_inventory[i] != null) _inventory[i].SetActive(false);
            }
        }
        else if (_currentSelectedItemIndex > _inventory.Count)
        {
            _currentSelectedItemIndex = 0;
            _selectedItem = _inventory[_currentSelectedItemIndex];
        }
        else if (_selectedItem != _inventory[_currentSelectedItemIndex]) _selectedItem = _inventory[_currentSelectedItemIndex];
    }

    public void Add(GameObject interactable, Vector3 positionInInventory, Vector3 rotationInInventory)
    {
        _inventory.Add(interactable);
        _currentSelectedItemIndex = _inventory.Count - 1;

        interactable.transform.SetParent(_playerData.InventoryHolder);
        interactable.transform.localPosition = positionInInventory;
        interactable.transform.localRotation = Quaternion.Euler(rotationInInventory);
        interactable.GetComponent<Collider>().enabled = false;
    }

    public void Remove(GameObject interactable)
    {
        _inventory.Remove(interactable);
        _currentSelectedItemIndex = 0;
    }

    public GameObject SelectedItem()
    {
        return _selectedItem;
    }
}
