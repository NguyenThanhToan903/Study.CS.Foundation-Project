using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data", order = 1)]
public class GameData : ScriptableObject
{
    public Dictionary<int, bool> levelStatus = new Dictionary<int, bool>();

    public void InitializeLevels(int totalLevels)
    {
        levelStatus.Clear();
        for (int i = 1; i <= totalLevels; i++)
        {
            levelStatus.Add(i, i == 1);
        }
    }

    public void UnlockLevel(int levelID)
    {
        if (levelStatus.ContainsKey(levelID))
        {
            levelStatus[levelID] = true;
        }
    }

    public bool IsLevelUnlocked(int levelID)
    {
        return levelStatus.ContainsKey(levelID) && levelStatus[levelID];
    }
}
