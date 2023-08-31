using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoEnd : MonoBehaviour, ITriggerAction
{
    private bool _freezePlayerMovement;

    public void TriggerAction(float triggerDelay)
    {
        StartCoroutine(Trigger(triggerDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);
        _freezePlayerMovement = true;
    }

    private void Update()
    {
        PlayerController.Instance.FreezePlayerMovement = true;
    }
}
