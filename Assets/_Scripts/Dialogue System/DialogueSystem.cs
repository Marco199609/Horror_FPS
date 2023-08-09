using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text DialogueUI;
    public DialogueData _dialogueData;
    private DialogueTextList _dialogueTextList;


    public static DialogueSystem instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        _dialogueData = GetComponent<DialogueData>();
        ParseJSON();
    }

    public void ManageDialogues(int dialogueIndex)
    {
        _dialogueData.DialogueAudioSource.PlayOneShot(_dialogueData.DialogueClips[dialogueIndex]);

        StartCoroutine(SetTextUI(_dialogueTextList.DialogueTexts[dialogueIndex].Text,
            _dialogueData.DialogueClips[dialogueIndex].length + 1));
    }

    private IEnumerator SetTextUI(string dialogueText, float duration)
    {
        DialogueUI.text = dialogueText;
        yield return new WaitForSeconds(duration);
        DialogueUI.text = "";
    }


    TextAsset _dialogueAsset;
    private void ParseJSON()
    {
        if (_dialogueData.english)
            _dialogueAsset = Resources.Load<TextAsset>("JSON/Dialogue/en-US");
        else if (_dialogueData.spanish)
            _dialogueAsset = Resources.Load<TextAsset>("JSON/Dialogue/es-LA");
        else
            _dialogueAsset = Resources.Load<TextAsset>("JSON/Dialogue/no-SUBTITLES");

        string json = _dialogueAsset.text;
        _dialogueTextList = JsonUtility.FromJson<DialogueTextList>(json);
    }
}


[Serializable]
public class DialogueTextList
{
    public DialogueTexts[] DialogueTexts;
}

[Serializable]
public class DialogueTexts
{
    public string Text;
}