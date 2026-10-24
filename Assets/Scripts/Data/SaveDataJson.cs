using System.IO;
using UnityEngine;

public class SaveDataJson : MonoBehaviour
{
    private PlayerData playerData;

    void Start()
    {
        playerData = PlayerData.Instance;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using(StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            writer.Write(json);
        }
    }

    public void LoadData()
    {
        string json = string.Empty;

        using(StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            json = reader.ReadToEnd();
        }

        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        playerData.SetPlayerData(data.CurrentScore, data.HighestScore);
    }
}
