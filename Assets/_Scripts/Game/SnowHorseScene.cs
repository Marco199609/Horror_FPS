using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SnowHorseScene : MonoBehaviour
{
    private float _timer = 10f;
    private VideoPlayer _snowhorseVideo;

    private void Awake()
    {
        Cursor.visible = false;
        _snowhorseVideo = GetComponent<VideoPlayer>();
    }


    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0)
        {
            Cursor.visible = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
