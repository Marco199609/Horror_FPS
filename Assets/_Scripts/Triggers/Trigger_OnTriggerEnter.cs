using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_OnTriggerEnter : MonoBehaviour
{
    [SerializeField] private Collider _triggerCollider;
    [SerializeField] private float _delay;
    [SerializeField] private bool _deactivateThis;

    private ITriggerAction _trigger;


    private void OnTriggerEnter(Collider other)
    {
        _trigger = gameObject.GetComponent<ITriggerAction>();

        if(other == _triggerCollider)
        {
            _trigger.TriggerAction(_delay);
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
