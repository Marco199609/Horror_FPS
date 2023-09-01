using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Trigger_LevelLoader _levelLoaderTrigger;
    [SerializeField] private Image _levelChangeMask;
    [SerializeField] private TextMeshProUGUI _tipText;
    [SerializeField] private PlayerStressControl _playerStress;

    public void StartGame()
    {
        //Makes mask for level change instant
        _levelChangeMask.color = new Color (0, 0, 0, 1);
        _tipText.color = new Color(1, 1, 1, 1);

        _levelLoaderTrigger.TriggerAction(0);
        GameSettings.Instance.Pause = false;
        GameSettings.Instance.CursorControl();
        _playerStress.AddStress();
    }

    public void ContinueGame()
    {
        GameSettings.Instance.Pause = false;
        UIManager.Instance.SettingsCanvas.SetActive(false);
        UIManager.Instance.MainMenuCanvas.SetActive(false);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("SnowHorse", LoadSceneMode.Single);
        DontDestroyOnLoad[] nonDestructables = FindObjectsOfType<DontDestroyOnLoad>();

        for (int i = 0; i < nonDestructables.Length; i++)
        {
            Destroy(nonDestructables[i].gameObject);
        }
    }

    public void OpenSettings()
    {
        UIManager.Instance.SettingsCanvas.SetActive(true);
        UIManager.Instance.MainMenuCanvas.SetActive(false);
    }

    public void CloseSettings()
    {
        UIManager.Instance.SettingsCanvas.SetActive(false);
        UIManager.Instance.MainMenuCanvas.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}