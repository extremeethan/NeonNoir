using System;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public enum CutsceneType
    {
        Intro,
        Outro
    }
    public CutsceneType cutsceneType;

    public VideoPlayer videoPlayer;
    public GameObject videoProjector;

    public float skipDelay = 1f;

    private float videoTimer;
    private bool videoFinished = false;

    private Action onVideoFinished;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cutsceneType == CutsceneType.Intro)
        {
            FindFirstObjectByType<GameManager>().isGameActive = false;
        }

        videoPlayer.loopPointReached += OnVideoFinished;

        if (cutsceneType == CutsceneType.Intro)
        {
            videoPlayer.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (videoFinished)
            return;

        videoTimer += Time.unscaledDeltaTime;

        if (videoTimer >= skipDelay && (Input.anyKeyDown || Input.touchCount > 0))
        {
            FinishVideo();
        }
    }

    void OnVideoFinished(VideoPlayer videoPlayer)
    {
        FinishVideo();
    }

    public void PlayOutro(Action callback)
    {
        onVideoFinished = callback;

        videoTimer = 0f;
        videoFinished = false;

        videoProjector.SetActive(true);
        videoPlayer.Play();
    }

    void FinishVideo()
    {
        if (videoFinished)
            return;

        videoFinished = true;

        videoPlayer.Stop();
        videoProjector.SetActive(false);

        if (cutsceneType == CutsceneType.Intro)
        {
            FindFirstObjectByType<GameManager>().isGameActive = true;
        }
        else if (cutsceneType == CutsceneType.Outro)
        {
            onVideoFinished?.Invoke();
            onVideoFinished = null;
        }
    }

    void OnDestroy()
    {
        if (videoPlayer != null)
            videoPlayer.loopPointReached -= OnVideoFinished;
    }
}
