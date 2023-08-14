using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{


    [SerializeField] private Text fpsText;
    [SerializeField] private float hudRefreshRate = 1f;
    [SerializeField] private bool _showFramerate;
    private float _timer;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
    void Update()
    {
        ShowFPS();
    }

    void ShowFPS()
    {
        if (!_showFramerate)
        {
            if (fpsText.gameObject.activeInHierarchy) fpsText.gameObject.SetActive(false);
        }
        else
        {
            if (Time.unscaledTime > _timer)
            {
                if (!fpsText.gameObject.activeInHierarchy) fpsText.gameObject.SetActive(true);

                int fps = (int)(1f / Time.unscaledDeltaTime);
                fpsText.text = "FPS: " + fps;
                _timer = Time.unscaledTime + hudRefreshRate;
            }
        }
    }

    void MainMusicControl()
    {

    }
}
