using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{
    public PlayerController player;
    public GameObject[] livesIcons;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;

        for (int i = 0; i < livesIcons.Length; i++)
        {
            livesIcons[i].SetActive(i < player.currentLives);
        }
    }
}
