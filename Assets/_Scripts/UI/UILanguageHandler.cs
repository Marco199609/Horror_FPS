using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILanguageHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _uiTexts;

    private TextAsset _UIAsset;
    private UITextList _UITextList;

    public static UILanguageHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }

    public void ParseJSON(bool english, bool spanish)
    {
        if (english)
            _UIAsset = Resources.Load<TextAsset>("JSON/UI/en-US");
        else if (spanish)
            _UIAsset = Resources.Load<TextAsset>("JSON/UI/es-LA");
        else
            _UIAsset = Resources.Load<TextAsset>("JSON/UI/en-US"); //Prevents error when none selected

        string json = _UIAsset.text;
        _UITextList = JsonUtility.FromJson<UITextList>(json);

        UpdateLanguage();
    }

    private void UpdateLanguage()
    {
        for(int i = 0; i < _uiTexts.Length; i++)
        {
            _uiTexts[i].text = _UITextList.UITexts[i].Text;
        }
    }
}

[Serializable]
public class UITextList
{
    public UITexts[] UITexts;
}

[Serializable]
public class UITexts
{
    public string Text;
}
