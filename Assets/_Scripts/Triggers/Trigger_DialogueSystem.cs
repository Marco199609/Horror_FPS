using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_DialogueSystem : MonoBehaviour, ITrigger
{
    //Use with Trigger_OnTriggerEnter component

    [SerializeField] private int[] _dialogueIndex;
    [SerializeField] private float[] _dialogueDelay; //Delays in case of using more than one dialogue lines with the same trigger.

    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        StartCoroutine(Trigger(triggerDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(0); //Trigger delays dont work with dialogue, use in built dialogue delays

        if(_dialogueDelay.Length > 0)
        {
            for(int i = 0; i < _dialogueIndex.Length; i++)
            {
                //Takes the length of the audioclip, as to not overlap clips one on the other 
                yield return new WaitForSeconds(DialogueSystem.Instance.DialogueData.DialogueClips[i].length + _dialogueDelay[i]);

                DialogueSystem.Instance.ManageDialogues(_dialogueIndex[i]);
            }
        }
        else
        {
            DialogueSystem.Instance.ManageDialogues(_dialogueIndex[0]);
        }
    }


}
