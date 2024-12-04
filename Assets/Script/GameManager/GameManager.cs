using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameData gameData;
    public int totalLevels = 3;

    private void Start()
    {
        if (gameData.levelStatus.Count == 0)
        {
            gameData.InitializeLevels(totalLevels);
        }
    }

    public void UnlockNextLevel(int currentLevelID)
    {
        int nextLevelID = currentLevelID + 1;
        if (nextLevelID <= totalLevels)
        {
            gameData.UnlockLevel(nextLevelID);
        }
    }

    public bool IsLevelUnlocked(int levelID)
    {
        return gameData.IsLevelUnlocked(levelID);
    }

    public void LoadLevel(int levelID)
    {
        if (gameData.IsLevelUnlocked(levelID))
        {
            SceneManager.LoadScene("Level" + levelID);
        }
        else
        {
            Debug.Log("Level " + levelID + " is locked!");
        }
    }
}
