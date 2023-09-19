using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_ActivateGameObject : MonoBehaviour, ITrigger
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _activationDelay;
    [SerializeField] private bool _activateOnInteraction = true;
    [SerializeField] private bool _activateOnInspection;
    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        if (_activateOnInteraction && isInteracting)
        {
            StartCoroutine(Trigger(_activationDelay));
        }
        else if (_activateOnInspection && isInspecting)
        {
            StartCoroutine(Trigger(_activationDelay));
        }
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);

        _gameObject.SetActive(true);
    }
}
