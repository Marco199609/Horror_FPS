using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneCallSubtitles : MonoBehaviour
{
    [SerializeField] private string[] _subtitles;
    [SerializeField] private float[] _subtitleDelays;

    void Start()
    {
        for(int i = 0; i < _subtitles.Length; i++)
        {
            StartCoroutine(ShowSubtitles(_subtitleDelays[i], _subtitles[i]));
        }
    }

    private IEnumerator ShowSubtitles(float delay, string subtitle)
    {
        yield return new WaitForSeconds(delay);

        UIManager.Instance.DialogueText.text = subtitle;
    }
}