using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CenterPointUIHandler))]
[RequireComponent(typeof(InteractableUIHandler))]
public class UIManager : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] private GameObject _interactableKeyPrompt;
    [SerializeField] private GameObject _inspectableOnlyMousePrompt;
    [SerializeField] private Image _uiCenterPoint;
    [SerializeField] private TextMeshProUGUI _playerDialogueText;

    [Header("Interactables UI")]
    [SerializeField] private TextMeshProUGUI _interactableDescription;
    [SerializeField] private TextMeshProUGUI _actionDescription;
    [SerializeField] private TextMeshProUGUI _characterDialogueText;
    
    //Required scripts
    private CenterPointUIHandler _centerPointUIHandler;
    private InteractableUIHandler _interactableUIHandler;

    private void OnEnable()
    {
        _centerPointUIHandler = GetComponent<CenterPointUIHandler>();
        _interactableUIHandler = GetComponent<InteractableUIHandler>();
        _interactableUIHandler.SetVariables(_interactableKeyPrompt, _inspectableOnlyMousePrompt, _interactableDescription, _actionDescription);

        //Subscribes to player UI events
        PlayerUI.ItemDescriptionActivated += _interactableUIHandler.ActivateInteractableUIElements;
        PlayerUI.ItemDescriptionReset += _interactableUIHandler.DeactivateInteractableUIElements;
        PlayerUI.ItemsBecameVisible += _centerPointUIHandler.AreItemsVisible;
    }

    private void OnDisable()
    {
        //Unsubscribes from player Ui events
        PlayerUI.ItemDescriptionActivated -= _interactableUIHandler.ActivateInteractableUIElements;
        PlayerUI.ItemDescriptionReset -= _interactableUIHandler.DeactivateInteractableUIElements;
        PlayerUI.ItemsBecameVisible -= _centerPointUIHandler.AreItemsVisible;
    }

    private void Update()
    {
        _centerPointUIHandler.UpdateCenterPoint(_uiCenterPoint);
    }
}
