using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoProjector;

    public float skipDelay = 1f;

    private float videoTimer;
    private bool videoFinished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (videoFinished)
            return;

        videoTimer += Time.deltaTime;

        if (videoTimer >= skipDelay && Input.anyKeyDown)
        {
            FinishVideo();
        }

        if (videoTimer >= skipDelay && Input.touchCount > 0)
        {
            FinishVideo();
        }
    }

    void FinishVideo()
    {
        if (videoFinished)
            return;

        videoFinished = true;

        videoPlayer.Stop();
        videoProjector.SetActive(false);

        Object.FindFirstObjectByType<GameManager>().isGameActive = true;
    }
}
