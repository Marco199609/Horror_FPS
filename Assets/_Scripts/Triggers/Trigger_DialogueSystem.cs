using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_DialogueSystem : MonoBehaviour, ITriggerAction
{
    //Use with Trigger_OnTriggerEnter component

    [SerializeField] private int[] _dialogueIndex;
    [SerializeField] private float[] _dialogueDelay; //Delays in case of using more than one dialogue lines with the same trigger.

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);

        if(_dialogueDelay.Length > 0)
        {
            for(int i = 0; i < _dialogueIndex.Length; i++)
            {
                DialogueSystem.Instance.ManageDialogues(_dialogueIndex[i]);

                //Takes the length of the audioclip, as to not overlap clips one on the other 
                yield return new WaitForSeconds(DialogueSystem.Instance._dialogueData.DialogueClips[i].length + _dialogueDelay[i]); 
            }
        }
        else
        {
            DialogueSystem.Instance.ManageDialogues(_dialogueIndex[0]);
        }
    }

    public void TriggerAction(float triggerDelay)
    {
        StartCoroutine(Trigger(triggerDelay));
    }
}
