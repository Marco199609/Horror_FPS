using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IPlayerInventory
{
    private GameObject _selectedItem;
    private List<GameObject> _inventory;
    private PlayerData _playerData;
    private AudioSource _pickupAudioSource;

    private int _currentSelectedItemIndex;

    private void Awake()
    {
        _inventory = new List<GameObject>();
        _inventory.Add(null);

    }

    public void Manage(PlayerData playerData, IPlayerInput playerInput)
    {
        if (_playerData == null) _playerData = playerData;
        if(_pickupAudioSource == null) _pickupAudioSource = _playerData.InventoryHolder.GetComponent<AudioSource>();

        if (playerInput.MouseScrollInput != 0 && !playerInput.FlashLightInput)
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
        if (_inventory[_currentSelectedItemIndex] != null) _inventory[_currentSelectedItemIndex].SetActive(false); //Deactivates previous item selected
        _currentSelectedItemIndex = _inventory.Count - 1; //Sets new item as selected

        _pickupAudioSource.Play();
        interactable.transform.SetParent(_playerData.InventoryHolder);
        interactable.transform.localPosition = positionInInventory; //Vector3.Lerp(interactable.transform.localPosition, positionInInventory, 1 * Time.deltaTime);
        interactable.transform.localRotation = Quaternion.Euler(rotationInInventory); //Quaternion.Lerp(interactable.transform.rotation, Quaternion.Euler(rotationInInventory), 1 * Time.deltaTime);
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

    public void HideInventory()
    {
        _currentSelectedItemIndex = 0;
    }
}
