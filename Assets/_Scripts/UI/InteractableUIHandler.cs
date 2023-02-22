using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableUIHandler : MonoBehaviour
{
    private GameObject _interactableKeyPrompt;
    private TextMeshProUGUI _interactableDescription, _actionDescription;

    public void SetVariables(GameObject interactableKeyPrompt, TextMeshProUGUI interactableDescription, TextMeshProUGUI actionDescription)
    {
        _interactableKeyPrompt = interactableKeyPrompt;
        _interactableDescription = interactableDescription;
        _actionDescription = actionDescription;
    }
    public void ActivateInteractableUIElements(string interactableDescription, string actionDescription)
    {
        //Updates item description
        _interactableDescription.text = interactableDescription;
        _actionDescription.text = actionDescription;

        //Activate hand and deactivate center point
        _interactableKeyPrompt.gameObject.SetActive(true);
    }

    public void DeactivateInteractableUIElements(string blankText, string blankText2)
    {
        //Blanks item description
        _interactableDescription.text = blankText;
        _actionDescription.text = blankText2;

        //Deactivate hand and activate center point
        _interactableKeyPrompt.gameObject.SetActive(false);
    }
}
