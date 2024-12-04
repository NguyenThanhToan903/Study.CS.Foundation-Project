using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data", order = 1)]
public class GameData : ScriptableObject
{
    public Dictionary<GameLevel, bool> levelStatus = new Dictionary<GameLevel, bool>();

    private void OnEnable()
    {
        if (levelStatus.Count == 0)
        {
            InitializeLevels();
        }
    }

    public void InitializeLevels()
    {
        levelStatus.Clear();
        foreach (GameLevel level in System.Enum.GetValues(typeof(GameLevel)))
        {
            levelStatus[level] = (level == GameLevel.Level1);
        }
    }

    public void UnlockLevel(GameLevel level)
    {
        if (levelStatus.ContainsKey(level))
        {
            levelStatus[level] = true;
        }
        else
        {
            Debug.LogWarning($"Level {level} không tồn tại trong danh sách!");
        }
    }

    public bool IsLevelUnlocked(GameLevel level)
    {
        return levelStatus.ContainsKey(level) && levelStatus[level];
    }
}
