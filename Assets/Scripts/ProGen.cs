using UnityEngine;

public class ProGen : MonoBehaviour
{
    public GameObject[] chunkPrefabsA;
    public GameObject[] chunkPrefabsB;
    public Vector3 nextSpawnPos;
    private bool spawnA = true;

    void Start()
    {
        SpawnChunk();
    }


    void Update()
    {
        
    }

    public void SpawnChunk()
    {
        GameObject[] selectedArray;

        if (spawnA)
        {
            selectedArray = chunkPrefabsA;
            spawnA = false;
        }
        else
        {
            selectedArray = chunkPrefabsB;
            spawnA = true;
        }

        // select chunk from array
        int randomIndex = Random.Range(0, selectedArray.Length);
        // spawn chunk from array
        GameObject temp = Instantiate(selectedArray[randomIndex], nextSpawnPos, Quaternion.identity);
        // set spawn position of next chunk
        nextSpawnPos = temp.transform.GetChild(1).transform.position;
    }
}
