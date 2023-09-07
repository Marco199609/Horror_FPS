using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting);
}
