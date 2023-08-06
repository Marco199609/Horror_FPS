using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_DeactivateGameObject : MonoBehaviour, ITriggerAction
{
    public GameObject[] _gameObjects;
    public void TriggerAction(float triggerDelay)
    {
        StartCoroutine(Trigger(triggerDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);

        for (int i = 0; i < _gameObjects.Length; i++)
        {
            _gameObjects[i].SetActive(false);
        }
    }
}
