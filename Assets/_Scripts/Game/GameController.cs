using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public int TargetFramerate, VSyncCount;


    [SerializeField] private Text fpsText;
    [SerializeField] private float hudRefreshRate = 1f;
    [SerializeField] private bool _showFramerate;
    private float _timer;

    private void Start()
    {
        //Adds this object to object manager for future use
        //ObjectManager.Instance.GameController = this;
        
        //Application.targetFrameRate = TargetFramerate;
        //QualitySettings.vSyncCount = VSyncCount;
    }

    // Update is called once per frame
    void Update()
    {
        ShowFPS();
        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
}
