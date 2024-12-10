using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMapController : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseGame;

    [SerializeField]
    private GameObject winGame;

    [SerializeField]
    private GameObject loseGame;

    [SerializeField]
    private GameObject timerUI;


    public void OnPausePressed()
    {
        Debug.Log("Nhan Button pause");
        if (pauseGame != null)
        {
            bool isActive = pauseGame.activeSelf;
            bool isTimerUIActive = timerUI.activeSelf;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClip);
            pauseGame.SetActive(!isActive);
            timerUI.SetActive(!isTimerUIActive);
            Time.timeScale = isActive ? 1f : 0f;
        }
        else
        {
            Debug.LogWarning("Pause menu chua duoc gan");
        }
    }

    public void OnResumePressed()
    {
        if (pauseGame != null)
        {
            bool isActive = pauseGame.activeSelf;
            bool isTimerUIActive = timerUI.activeSelf;
            if (isActive)
            {
                pauseGame.SetActive(false);
                timerUI.SetActive(!isTimerUIActive);
                Time.timeScale = 1f;
            }
        }
        else
        {
            Debug.LogWarning("Pause game chua duoc gan");
        }
        WinGame();
        LoseGame();
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClip);
    }

    public void OnNextPressed()
    {
        if (winGame != null)
        {
            bool isActive = winGame.activeSelf;
            if (isActive)
            {
                winGame.SetActive(false);
                string nextLevelName = GameManager.Instance.GetNextLevelName(SceneManager.GetActiveScene().name);
                if (GameManager.Instance.IsLevelUnlocked(nextLevelName))
                {
                    SceneManager.LoadScene(nextLevelName);
                }
                else
                {
                    Debug.LogWarning($"{nextLevelName} chua duoc mo khoa");
                }
                Time.timeScale = 1.0f;
            }
        }
        else
        {
            Debug.LogWarning("WinGame chua duoc gan");
        }
    }

    private void WinGame()
    {
        if (winGame != null)
        {
            bool isActive = winGame.activeSelf;
            if (isActive)
            {
                winGame.SetActive(false);
                Debug.Log("Choi lai");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1.0f;
            }
        }
        else
        {
            Debug.LogWarning("WinGame chua duoc gan");
        }
    }

    private void LoseGame()
    {
        if (loseGame != null)
        {
            bool isActive = loseGame.activeSelf;
            if (isActive)
            {
                loseGame.SetActive(false);
                Debug.Log("Choi lai");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1.0f;
            }
        }
        else
        {
            Debug.LogWarning("Lose game chua duoc gan");
        }
    }
    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}

