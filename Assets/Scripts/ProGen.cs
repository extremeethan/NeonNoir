using UnityEngine;

public class ProGen : MonoBehaviour
{
    public GameObject[] chunkPrefabs;
    public Vector3 nextSpawnPos;

    void Start()
    {
        SpawnChunk();
    }


    void Update()
    {
        
    }

    public void SpawnChunk()
    {
        // select chunk from array
        int randomIndex = Random.Range(0, chunkPrefabs.Length);
        // spawn chunk from array
        GameObject temp = Instantiate(chunkPrefabs[randomIndex], nextSpawnPos, Quaternion.identity);
        // set spawn position of next chunk
        nextSpawnPos = temp.transform.GetChild(1).transform.position;
    }
}
