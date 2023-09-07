using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_JumpScare : MonoBehaviour, ITrigger
{
    [SerializeField] private AudioSource _jumpScareSource;
    [SerializeField] private AudioClip[] _jumpScareClips;

    public void TriggerBehaviour(float triggerDelay, bool isInteracting, bool isInspecting)
    {
        StartCoroutine(Trigger(triggerDelay));
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(triggerDelay);
        PlayerController.Instance.StressControl.AddStress();
        int jumpScareClipIndex = Random.Range(0, _jumpScareClips.Length - 1);
        _jumpScareSource.PlayOneShot(_jumpScareClips[jumpScareClipIndex]);
    }
}
