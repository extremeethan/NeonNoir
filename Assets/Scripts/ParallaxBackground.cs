using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startPos, length;
    public float parallaxEffect;
    private GameObject mainCamera;

    void Start()
    {
        // sprite bg position x-axis
        startPos = transform.position.x;
        // length of each sprite across x-axis
        length = this.GetComponent<SpriteRenderer>().bounds.size.x;
        // get camera reference
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }


    void Update()
    {
        // farther distance parts of bg choose a lesser number in inspector
        float distance = mainCamera.transform.position.x* parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        float temp = mainCamera.transform.position.x * (1 - parallaxEffect); 
        if (temp > startPos + length)
        {
            startPos += length;
        }
    }
}
