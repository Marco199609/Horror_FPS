using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image _uiPickupHand;
    [SerializeField] private Image _uiCenterPoint;
    [SerializeField] private TextMeshProUGUI _interactableDescription;

    private string _description;

    private void OnEnable()
    {
        PlayerHover.UIActivated += ItemHoverUI;
        PlayerHover.UIDeactivated += DeactivateUIElements;
        PlayerUI.ColorUpdated += CenterPointControl;
    }

    private void OnDisable()
    {
        PlayerHover.UIActivated -= ItemHoverUI;
        PlayerHover.UIDeactivated -= DeactivateUIElements;
        PlayerUI.ColorUpdated -= CenterPointControl;
    }

    void ItemHoverUI(RaycastHit hit)
    {
        //Checks if gameobject is item or weapon
        if (_description == "") _description = hit.transform.gameObject.GetComponent<IInteractable>().Description();

        if (_interactableDescription.text == "")
            _interactableDescription.text = _description;

        ActivateUIPickupHand();
    }

    private void ActivateUIPickupHand()
    {
        _uiPickupHand.gameObject.SetActive(true);
        _uiCenterPoint.gameObject.SetActive(false);
    }

    private void DeactivateUIElements(RaycastHit hit)
    {
        if (_uiPickupHand.gameObject.activeInHierarchy) _uiPickupHand.gameObject.SetActive(false);

        if (!_uiCenterPoint.gameObject.activeInHierarchy) _uiCenterPoint.gameObject.SetActive(true);

        if (_description != "") _description = "";

        if (_interactableDescription.text != _description) _interactableDescription.text = _description;
    }

    private void CenterPointControl(Color color)
    {
        _uiCenterPoint.color = color;
    }
}
