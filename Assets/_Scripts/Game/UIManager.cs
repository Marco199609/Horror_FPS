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
    [SerializeField] private TextMeshProUGUI _actionDescription;
    
    private bool _itemsCurrentlyVisible;

    //Center point variables
    private float _transparency, _previousTransparency, _maxTransparency = 0.08f, _changeSpeed = 0.1f;
    private Color _centerPointColor;

    private void OnEnable()
    {
        //Subscribes to player UI events
        PlayerUI.ItemDescriptionActivated += ActivatePlayerUIElements;
        PlayerUI.ItemDescriptionReset += DeactivatePlayerUIElements;
        PlayerUI.ItemsBecameVisible += AreItemsVisible;
    }

    private void OnDisable()
    {
        //Unsubscribes from player Ui events
        PlayerUI.ItemDescriptionActivated -=ActivatePlayerUIElements;
        PlayerUI.ItemDescriptionReset -= DeactivatePlayerUIElements;
        PlayerUI.ItemsBecameVisible -= AreItemsVisible;
    }

    void ActivatePlayerUIElements(string interactableDescription, string actionDescription)
    {
        //Updates item description
        _interactableDescription.text = interactableDescription;
        _actionDescription.text = actionDescription;

        //Activate hand and deactivate center point
        _interactableKeyPrompt.gameObject.SetActive(true);
    }

    private void DeactivatePlayerUIElements(string blankText, string blankText2)
    {
        //Blanks item description
        _interactableDescription.text = blankText;
        _actionDescription.text = blankText2;

        //Deactivate hand and activate center point
        _interactableKeyPrompt.gameObject.SetActive(false);
    }

    //Updates center point when an interactable is in camera view
    private void AreItemsVisible(bool areItemsVisible)
    {
        _itemsCurrentlyVisible = areItemsVisible;
    }

    private void UpdateCenterPoint()
    {
        if (_itemsCurrentlyVisible && _transparency < _maxTransparency)
        {
            _transparency += _changeSpeed * 2 * Time.deltaTime;
        }
        else if (!_itemsCurrentlyVisible && _transparency > 0)
        {
            _transparency -= _changeSpeed * Time.deltaTime;
        }

        Mathf.Clamp(_transparency, 0f, _maxTransparency);

        if (_transparency != _previousTransparency) //Prevents color from being passed to the UI manager when not necessary
        {
            _centerPointColor = new Color(1, 1, 1, _transparency);

            _uiCenterPoint.color = _centerPointColor;

            _previousTransparency = _transparency;
        }
    }

    private void Update()
    {
        UpdateCenterPoint();
    }
}
