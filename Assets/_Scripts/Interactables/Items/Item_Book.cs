using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Book : MonoBehaviour, ITrigger
{
    [SerializeField] private GameObject _requiredKey, _closedBook, _openBook;
    [Header("Behaviour Components")]
    [SerializeField] private GameObject[] _interactionBehaviours;
    [SerializeField] private GameObject[] _inspectionBehaviours;
    [SerializeField] private bool _isOpen;

    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        if (PlayerController.Instance.Inventory.SelectedItem() == _requiredKey && isInteracting || _isOpen)
        {
            PlayerController.Instance.Inventory.Remove(_requiredKey);
            Destroy(_requiredKey);

            _closedBook.SetActive(false);
            _openBook.SetActive(true);
            _isOpen = true;

            for (int i = 0; i < _interactionBehaviours.Length; i++)
            {
                _interactionBehaviours[i].GetComponent<ITrigger>().TriggerBehaviour(0, isInteracting, isInspecting);
            }
        }

        if(_isOpen)
        {
            for (int i = 0; i < _inspectionBehaviours.Length; i++)
            {
                _inspectionBehaviours[i].GetComponent<ITrigger>().TriggerBehaviour(0, isInteracting, isInspecting);
            }
        }
    }
}
