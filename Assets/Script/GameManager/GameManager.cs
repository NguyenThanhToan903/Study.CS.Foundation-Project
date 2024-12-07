using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameData gameData;

    public GameLevel currentLevel;

    public GameLevel nextLevel;

    private void Start()
    {
        if (gameData.levelStatus.Count == 0)
        {
            gameData.InitializeLevels();
        }

        // Lấy currentLevel từ tên Scene
        string activeSceneName = SceneManager.GetActiveScene().name;

        foreach (var kvp in GameLevelMap.LevelToSceneName) // kvp: key-value pair
        {
            if (kvp.Value == activeSceneName)
            {
                currentLevel = kvp.Key;

                // Xác định level tiếp theo
                nextLevel = GetNextLevel(currentLevel);
                break;
            }
        }
    }

    public void UnlockNextLevel()
    {
        if (nextLevel != GameLevel.None && System.Enum.IsDefined(typeof(GameLevel), nextLevel))
        {
            gameData.UnlockLevel(nextLevel);
            Debug.Log($"Level {nextLevel} đã được mở khóa!");
        }
        else
        {
            Debug.LogWarning("Không có level tiếp theo để mở khóa!");
        }
    }

    public void LoadLevel()
    {
        if (gameData.IsLevelUnlocked(nextLevel))
        {
            string sceneName = GameLevelMap.LevelToSceneName[nextLevel];
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning($"Level {nextLevel} đang bị khóa!");
        }
    }

    private GameLevel GetNextLevel(GameLevel currentLevel)
    {
        int nextLevelIndex = (int)currentLevel + 1;
        if (System.Enum.IsDefined(typeof(GameLevel), nextLevelIndex))
        {
            return (GameLevel)nextLevelIndex;
        }

        return GameLevel.None;
    }
}
