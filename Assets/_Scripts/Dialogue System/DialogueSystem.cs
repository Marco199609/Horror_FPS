using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    public DialogueData DialogueData;

    private DialogueTextList _dialogueTextList;
    private TextAsset _dialogueAsset;

    public static DialogueSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        DialogueData = GetComponent<DialogueData>();
        ParseJSON();
        UILanguageHandler.Instance.ParseJSON(DialogueData.English, DialogueData.Spanish);
    }

    public void ManageDialogues(int dialogueIndex)
    {
        DialogueData.DialogueAudioSource.PlayOneShot(DialogueData.DialogueClips[dialogueIndex]);

        StartCoroutine(SetTextUI(_dialogueTextList.DialogueTexts[dialogueIndex].Text,
            DialogueData.DialogueClips[dialogueIndex].length + 0.2f));
    }

    private IEnumerator SetTextUI(string dialogueText, float duration)
    {
        UIManager.Instance.DialogueText.text = dialogueText;
        yield return new WaitForSeconds(duration);
        UIManager.Instance.DialogueText.text = "";
    }

    public void ChangeLanguage()
    {
        ParseJSON();
        UILanguageHandler.Instance.ParseJSON(DialogueData.English, DialogueData.Spanish);
    }

    private void ParseJSON()
    {
        if (DialogueData.English)
            _dialogueAsset = Resources.Load<TextAsset>("JSON/Dialogue/en-US");
        else if (DialogueData.Spanish)
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