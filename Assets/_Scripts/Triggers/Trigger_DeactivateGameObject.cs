using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_DeactivateGameObject : MonoBehaviour, ITriggerAction
{
    public GameObject _gameObject;
    public void TriggerAction(float triggerDelay)
    {
        StartCoroutine(Trigger(triggerDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);

        _gameObject.SetActive(false);
    }
}
