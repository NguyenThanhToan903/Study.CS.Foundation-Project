using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data", order = 1)]
public class GameData : ScriptableObject
{

    [System.Serializable]
    public class LevelStatus
    {
        public GameLevel level;
        public bool isUnlocked;
    }

    [SerializeField]
    private List<LevelStatus> levels = new List<LevelStatus>();

    public Dictionary<GameLevel, bool> levelStatus = new Dictionary<GameLevel, bool>();

    private void OnEnable()
    {
        if (levels.Count == 0)
        {
            InitializeLevels();
        }
        else
        {
            // Đồng bộ danh sách vào từ điển
            SyncDictionary();
        }
    }

    public void InitializeLevels()
    {
        levels.Clear();
        levelStatus.Clear();

        foreach (GameLevel level in System.Enum.GetValues(typeof(GameLevel)))
        {
            bool isUnlocked = (level == GameLevel.Level1); // Level1 mặc định được mở khóa
            levels.Add(new LevelStatus { level = level, isUnlocked = isUnlocked });
            levelStatus[level] = isUnlocked;
        }
    }

    public void UnlockLevel(GameLevel level)
    {
        if (levelStatus.ContainsKey(level))
        {
            levelStatus[level] = true;

            LevelStatus status = levels.Find(l => l.level == level);
            if (status != null)
            {
                status.isUnlocked = true;
            }
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

    private void SyncDictionary()
    {
        levelStatus.Clear();
        foreach (var level in levels)
        {
            levelStatus[level.level] = level.isUnlocked;
        }
    }
}


