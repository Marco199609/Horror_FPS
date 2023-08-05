using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_OnTriggerEnter : MonoBehaviour
{
    [SerializeField] private Collider _triggerCollider;
    [SerializeField] private Trigger_ActivateGameObject _gameObjectActivator;
    [SerializeField] private float _delay;
    [SerializeField] private bool _deactivateThis;


    private void OnTriggerEnter(Collider other)
    {
        if(other == _triggerCollider)
        {
            _gameObjectActivator.TriggerAction(_delay);
        }

        if(_deactivateThis)
        {
            StartCoroutine(DeactivateTrigger(_delay + 0.01f));
        }
    }

    IEnumerator DeactivateTrigger(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
