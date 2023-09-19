using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_DeactivateGameObject : MonoBehaviour, ITrigger
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _deactivationDelay;
    [SerializeField] private bool _deactivateOnInteraction = true;
    [SerializeField] private bool _deactivateOnInspection;

    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        if(_deactivateOnInteraction && isInteracting)
        {
            StartCoroutine(Trigger(_deactivationDelay));
        }
        else if(_deactivateOnInspection && isInspecting)
        {
            StartCoroutine(Trigger(_deactivationDelay));
        }
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);

        _gameObject.SetActive(false);
    }
}
