using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_DeactivateGameObject : MonoBehaviour, ITrigger
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _deactivationDelay;
    public void TriggerBehaviour(float triggerDelay)
    {
        StartCoroutine(Trigger(_deactivationDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);

        _gameObject.SetActive(false);
    }
}
