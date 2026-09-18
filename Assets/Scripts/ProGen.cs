using UnityEngine;

public class ProGen : MonoBehaviour
{
    public GameObject chunkPrefab;
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
        GameObject temp = Instantiate(chunkPrefab, nextSpawnPos, Quaternion.identity);
        nextSpawnPos = temp.transform.GetChild(1).transform.position;
    }
}
