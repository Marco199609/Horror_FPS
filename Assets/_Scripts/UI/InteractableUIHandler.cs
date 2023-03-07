using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableUIHandler : MonoBehaviour
{
    private GameObject _interactableKeyPrompt, _inspectableOnlyMousePrompt;
    private TextMeshProUGUI _interactableDescription, _actionDescription;

    public void SetVariables(GameObject interactableKeyPrompt, GameObject inspectableOnlyMousePrompt, TextMeshProUGUI interactableDescription, TextMeshProUGUI actionDescription)
    {
        _interactableKeyPrompt = interactableKeyPrompt;
        _interactableDescription = interactableDescription;
        _actionDescription = actionDescription;
        _inspectableOnlyMousePrompt = inspectableOnlyMousePrompt;
    }
    public void ActivateInteractableUIElements(string interactableDescription, string actionDescription, bool inspectableOnly)
    {
        //Updates item description
        _interactableDescription.text = interactableDescription;
        _actionDescription.text = actionDescription;

        if(inspectableOnly ) _inspectableOnlyMousePrompt.gameObject.SetActive(true);
        else _interactableKeyPrompt.gameObject.SetActive(true);
    }

    public void DeactivateInteractableUIElements(string blankText, string blankText2, bool inspectableOnly)
    {
        //Blanks item description
        _interactableDescription.text = blankText;
        _actionDescription.text = blankText2;

        _interactableKeyPrompt.gameObject.SetActive(false);
        _inspectableOnlyMousePrompt.gameObject.SetActive(false);
    }
}
