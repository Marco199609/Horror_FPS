using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_AddToInventory : MonoBehaviour, ITrigger
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Vector3 _positionInInventory, _rotationInInventory, _scaleInInventory;

    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        if(isInteracting)
        {
            PlayerController.Instance.Inventory.Add(_gameObject, _positionInInventory, _rotationInInventory, _scaleInInventory);
            this.enabled = false;
        }
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        throw new System.NotImplementedException();
    }
}
