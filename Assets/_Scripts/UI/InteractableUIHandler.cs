using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableUIHandler : MonoBehaviour
{
    private GameObject _interactableKeyPrompt, _inspectableOnlyMousePrompt;

    public void SetVariables(GameObject interactableKeyPrompt, GameObject inspectableOnlyMousePrompt)
    {
        _interactableKeyPrompt = interactableKeyPrompt;
        _inspectableOnlyMousePrompt = inspectableOnlyMousePrompt;
    }
    public void ActivateInteractableUIElements(bool inspectableOnly)
    {
        if(inspectableOnly ) _inspectableOnlyMousePrompt.gameObject.SetActive(true);
        else _interactableKeyPrompt.gameObject.SetActive(true);
    }

    public void DeactivateInteractableUIElements(bool inspectableOnly)
    {
        _interactableKeyPrompt.gameObject.SetActive(false);
        _inspectableOnlyMousePrompt.gameObject.SetActive(false);
    }
}
