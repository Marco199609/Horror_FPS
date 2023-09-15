using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_DialogueSystem : MonoBehaviour, ITrigger
{
    //Use with Trigger_OnTriggerEnter component

    [SerializeField] private int[] _dialogueIndex;
    [SerializeField] private float[] _dialogueDelay; //Delays in case of using more than one dialogue lines with the same trigger.
    [SerializeField] private bool _triggerOnInteraction, _triggerOnInspection, _randomizeDialogue;

    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        if(_triggerOnInteraction == true && isInteracting == true)
        {
            StartCoroutine(Trigger());
        }
        if(_triggerOnInspection == true && isInspecting == true)
        {
            StartCoroutine(Trigger());
        }
    }

    private IEnumerator Trigger()
    {
        if(_dialogueDelay.Length > 0)
        {
            if(_randomizeDialogue == true)
            {
                int i = Random.Range(0, _dialogueIndex.Length);
                yield return new WaitForSeconds(DialogueSystem.Instance.DialogueData.DialogueClips[i].length + _dialogueDelay[i]);
                DialogueSystem.Instance.ManageDialogues(_dialogueIndex[i]);
            }
            else
            {
                for (int i = 0; i < _dialogueIndex.Length; i++)
                {
                    //Takes the length of the audioclip, as to not overlap clips one on the other 
                    yield return new WaitForSeconds(DialogueSystem.Instance.DialogueData.DialogueClips[i].length + _dialogueDelay[i]);

                    DialogueSystem.Instance.ManageDialogues(_dialogueIndex[i]);
                }
            }
        }
        else
        {
            DialogueSystem.Instance.ManageDialogues(_dialogueIndex[0]);
        }
    }
}
