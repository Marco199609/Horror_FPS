using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_JumpScare : MonoBehaviour, ITriggerAction
{
    [SerializeField] private AudioSource _jumpScareSource;
    [SerializeField] private AudioClip[] _jumpScareClips;

    public void TriggerAction()
    {
        int jumpScareClipIndex = Random.Range(0, _jumpScareClips.Length - 1);
        _jumpScareSource.PlayOneShot(_jumpScareClips[jumpScareClipIndex]);
    }
}
