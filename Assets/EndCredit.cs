using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Video;

public class EndCredit : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private bool isCompleted = false;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (videoPlayer != null)
        {
            if (!videoPlayer.isPlaying && !isCompleted)
            {               
                LoadMenuScene();
            }
        }
    }

    private void LoadMenuScene()
    {
        isCompleted = true;
        SceneManager.LoadScene(0);
    }
}
