using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour_Readable : MonoBehaviour, ITrigger
{
    [SerializeField, TextArea(15, 20)] private string _readableText;
    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        if(isInspecting)
        {
            UIManager.Instance.ReadableItemText.gameObject.SetActive(true);
            UIManager.Instance.ReadableItemText.text = _readableText;
        }
        else
        {
            UIManager.Instance.ReadableItemText.gameObject.SetActive(false);
            UIManager.Instance.ReadableItemText.text = "";
        }
    }
}
