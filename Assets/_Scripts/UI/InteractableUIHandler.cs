using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableUIHandler : MonoBehaviour
{
    private GameObject _interactOnlyPrompt, _inspectOnlyPrompt, _inspectAndInteractPrompt, _returnItemPrompt;

    public void SetVariables(GameObject interactOnlyPrompt, GameObject inspectOnlyPrompt, GameObject inspectAndInteractPrompt, GameObject returnItemPrompt)
    {
        _interactOnlyPrompt = interactOnlyPrompt;
        _inspectOnlyPrompt = inspectOnlyPrompt;
        _inspectAndInteractPrompt = inspectAndInteractPrompt;
        _returnItemPrompt = returnItemPrompt;
    }
    public void ActivateInteractableUIElements(bool nonInspectable, bool inspectableOnly)
    {
        if (!PlayerController.Instance.PlayerInspect.Inspecting())
        {
            if (inspectableOnly)
                _inspectOnlyPrompt.gameObject.SetActive(true);
            else if (nonInspectable)
                _interactOnlyPrompt.gameObject.SetActive(true);
            else
                _inspectAndInteractPrompt.gameObject.SetActive(true);
        }
        else
        {
            _returnItemPrompt.gameObject.SetActive(true);
            _inspectOnlyPrompt.gameObject.SetActive(false);
            _interactOnlyPrompt.gameObject.SetActive(false);
            _inspectAndInteractPrompt.gameObject.SetActive(false);
        }
    }

    public void DeactivateInteractableUIElements(bool nonInspectable, bool inspectableOnly)
    {
        _returnItemPrompt.gameObject.SetActive(false);
        _inspectOnlyPrompt.gameObject.SetActive(false);
        _interactOnlyPrompt.gameObject.SetActive(false);
        _inspectAndInteractPrompt.gameObject.SetActive(false);
    }
}
