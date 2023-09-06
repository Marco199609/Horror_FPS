using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_ActivateGameObject : MonoBehaviour, ITrigger
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _activationDelay;
    public void TriggerBehaviour(float triggerDelay)
    {
        StartCoroutine(Trigger(_activationDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);

        _gameObject.SetActive(true);
    }
}
