using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int levelOneHighestScore = 0;
    public int levelTwoHighestScore = 0;
    public int levelThreeHighestScore = 0;
    public int endlessHighestScore = 0;

    public int highestLevelUnlocked = 1;

    public List<string> collectedLoreIDs = new List<string>();
}
