using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SnowHorseScene : MonoBehaviour
{
    //private float _timer = 10f;
    [SerializeField] private VideoPlayer _snowhorseVideo;
    private bool _videoPlayed;

    private void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        //_timer -= Time.deltaTime;
        if(!_snowhorseVideo.isPlaying)
        {
            if(!_videoPlayed)
            {
                _snowhorseVideo.Play();
                _videoPlayed = true;
            }
            else if(_videoPlayed && !_snowhorseVideo.isPlaying)
            {
                Cursor.visible = true;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
