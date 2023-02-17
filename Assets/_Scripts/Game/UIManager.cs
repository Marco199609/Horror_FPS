using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player UI")]
    [SerializeField] private GameObject _interactableKeyPrompt;
    [SerializeField] private Image _uiCenterPoint;
    [SerializeField] private TextMeshProUGUI _interactableDescription;

    private void OnEnable()
    {
        //Subscribes to player UI events
        PlayerUI.ItemDescriptionActivated += ActivatePlayerUIElements;
        PlayerUI.ItemDescriptionDeactivated += DeactivatePlayerUIElements;
        PlayerUI.CenterPointUpdated += CenterPointColorUpdate;
    }

    private void OnDisable()
    {
        //Unsubscribes from player Ui events
        PlayerUI.ItemDescriptionActivated -= ActivatePlayerUIElements;
        PlayerUI.ItemDescriptionDeactivated -= DeactivatePlayerUIElements;
        PlayerUI.CenterPointUpdated -= CenterPointColorUpdate;
    }

    void ActivatePlayerUIElements(string description)
    {
        //Updates item description
        _interactableDescription.text = description;

        //Activate hand and deactivate center point
        _interactableKeyPrompt.gameObject.SetActive(true);
    }

    private void DeactivatePlayerUIElements(string description)
    {
        //Blanks item description
        _interactableDescription.text = description;

        //Deactivate hand and activate center point
        _interactableKeyPrompt.gameObject.SetActive(false);
    }

    //Updates center point when an interactable is in camera view
    private void CenterPointColorUpdate(Color color)
    {
        _uiCenterPoint.color = color; 
    }
}
