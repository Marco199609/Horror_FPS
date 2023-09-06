using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    void TriggerBehaviour(float triggerDelay);

    IEnumerator Trigger(float triggerDelay);
}
