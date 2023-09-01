using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoEnd : MonoBehaviour, ITriggerAction
{
    [SerializeField] private float _endGameDelay = 44, _goBackToStartDelay = 50;
    [SerializeField] private GameObject _endGameCanvas;

    public void TriggerAction(float triggerDelay)
    {
        StartCoroutine(Trigger(_endGameDelay));
        StartCoroutine(GoBackToStart());
    }

    public IEnumerator Trigger(float triggerDelay)
    {
        yield return new WaitForSeconds(_endGameDelay);
        _endGameCanvas.SetActive(true);
    }

    public IEnumerator GoBackToStart()
    {
        yield return new WaitForSeconds(_goBackToStartDelay);
        DontDestroyOnLoad[] undestructibleObjects = FindObjectsOfType<DontDestroyOnLoad>();
        
        for(int  i = 0; i < undestructibleObjects.Length; i++)
        {
            Destroy(undestructibleObjects[i].gameObject);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("SnowHorse");
    }

    private void Update()
    {
        PlayerController.Instance.FreezePlayerMovement = true;
        PlayerController.Instance.StressControl.AddStress();
    }
}
