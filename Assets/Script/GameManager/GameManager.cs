using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameData gameData;

    public GameLevel currentLevel;
    public GameLevel nextLevel;

    private void OnEnable()
    {
        if (gameData == null)
        {
            gameData = Resources.Load<GameData>("GameData");
            if (gameData == null)
            {
                Debug.LogError("Không tìm thấy GameData trong Resources");
            }
            else
            {
                gameData.InitializeLevels();
            }
        }
    }


    private void Start()
    {
        if (gameData == null)
        {
            Debug.LogError("GameData chưa được gán trong Inspector!");
            return;
        }

        // Khởi tạo dữ liệu nếu chưa có
        if (gameData.GetLevels().Count == 0)
        {
            gameData.InitializeLevels();
        }

        // Lấy currentLevel từ tên Scene
        string activeSceneName = SceneManager.GetActiveScene().name;

        // Tìm level tương ứng với scene hiện tại
        foreach (var levelStatus in gameData.GetLevels())
        {
            if (GameLevelMap.LevelToSceneName.ContainsValue(activeSceneName))
            {
                currentLevel = levelStatus.level;
                nextLevel = GetNextLevel(currentLevel);
                break;
            }
        }
    }

    public void UnlockNextLevel()
    {
        if (nextLevel != GameLevel.None)
        {
            gameData.UnlockLevel(nextLevel);
            Debug.Log($"Level {nextLevel} đã được mở khóa!");
        }
        else
        {
            Debug.LogWarning("Không có level tiếp theo để mở khóa!");
        }
    }

    public void LoadNextLevel()
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
