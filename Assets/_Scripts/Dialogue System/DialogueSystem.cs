using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    private TMP_Text _dialogueUI;
    public DialogueData _dialogueData;
    private DialogueTextList _dialogueTextList;


    public static DialogueSystem Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        _dialogueData = GetComponent<DialogueData>();
        ParseJSON();
    }

    private void Update()
    {
        if(_dialogueUI == null && SceneManager.GetActiveScene().name != "MainMenu")
        {
            _dialogueUI = GameObject.FindWithTag("DialogueText").GetComponent<TMP_Text>();
        }
    }

    public void ManageDialogues(int dialogueIndex)
    {
        _dialogueData.DialogueAudioSource.PlayOneShot(_dialogueData.DialogueClips[dialogueIndex]);

        StartCoroutine(SetTextUI(_dialogueTextList.DialogueTexts[dialogueIndex].Text,
            _dialogueData.DialogueClips[dialogueIndex].length + 0.2f));
    }

    private IEnumerator SetTextUI(string dialogueText, float duration)
    {
        _dialogueUI.text = dialogueText;
        yield return new WaitForSeconds(duration);
        _dialogueUI.text = "";
    }

    public void ChangeLanguage()
    {
        ParseJSON();
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