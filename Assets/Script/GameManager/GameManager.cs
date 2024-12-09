using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [System.Serializable]
    public class LevelData
    {
        public string levelName;
        public bool isUnlocked;
    }

    public List<LevelData> Levels = new List<LevelData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeLevels();
    }

    private void InitializeLevels()
    {
        if (Levels.Count == 0)
        {
            Levels.Add(new LevelData { levelName = "GameLevel1", isUnlocked = true });
            Levels.Add(new LevelData { levelName = "GameLevel2", isUnlocked = false });
            Levels.Add(new LevelData { levelName = "GameLevel3", isUnlocked = false });
        }
    }

    public bool IsLevelUnlocked(string levelName)
    {
        var level = Levels.Find(l => l.levelName == levelName);
        return level != null && level.isUnlocked;
    }

    public void UnlockLevel(string levelName)
    {
        int index = Levels.FindIndex(l => l.levelName == levelName);
        if (index >= 0 && index < Levels.Count)
        {
            Levels[index].isUnlocked = true;
            Debug.Log($"{Levels[index].levelName} da duoc mo khoa");
        }
    }

    public string GetNextLevelName(string currentLevelName)
    {
        int index = Levels.FindIndex(l => l.levelName == currentLevelName);
        if (index >= 0 && index + 1 < Levels.Count)
        {
            return Levels[index + 1].levelName;
        }
        return null;
    }

    public void LockLevel(string levelName)
    {
        int index = Levels.FindIndex(l => l.levelName == levelName);
        if (index >= 0 && index < Levels.Count)
        {
            Levels[index].isUnlocked = false;
            Debug.Log($"{Levels[index].levelName} da duoc khoa");
        }
    }
}
