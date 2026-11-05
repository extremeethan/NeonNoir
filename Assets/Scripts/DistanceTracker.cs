using UnityEngine;
using UnityEngine.UI;

public class DistanceTracker : MonoBehaviour
{
    public Slider slider;
    public Transform player;
    public Transform levelStart;
    public Transform levelFinish;

    public GameObject distanceSlider;
    public bool isCurrentLevelEndless = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (isCurrentLevelEndless)
        {
            distanceSlider.SetActive(false);
            return;
        }

        distanceSlider.SetActive(true);
        slider.minValue = 0f;
        slider.maxValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCurrentLevelEndless)
            return;

        if (player == null || levelStart == null || levelFinish == null)
            return;

        float totalDistance = levelFinish.position.x - levelStart.position.x;

        if (totalDistance <= 0f)
            return;

        float progress = (player.position.x - levelStart.position.x) / totalDistance;

        slider.value = Mathf.Clamp01(progress);
    }
}
