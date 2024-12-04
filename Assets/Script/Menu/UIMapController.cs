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
        Debug.Log("Button pause pressed");
        if (pauseGame != null)
        {
            bool isActive = pauseGame.activeSelf;
            bool isTimerUIActive = timerUI.activeSelf;
            pauseGame.SetActive(!isActive);
            timerUI.SetActive(!isActive);
            Time.timeScale = isActive ? 1f : 0f;
        }
        else
        {
            Debug.LogWarning("Pause menu object is not assigned!");
        }
    }

    public void OnResumePressed()
    {
        if (pauseGame != null)
        {
            bool isActive = pauseGame.activeSelf;
            if (isActive)
            {
                Debug.LogWarning("Pause game ");
                pauseGame.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        else
        {
            Debug.LogWarning("Pause game object is not assigned!");
        }
        if (winGame != null)
        {
            bool isActive = winGame.activeSelf;
            if (isActive)
            {
                Debug.LogWarning("Win game ");

                winGame.SetActive(false);
                Time.timeScale = 1f;
            }

        }
        else
        {
            Debug.LogWarning("Win game object is not assigned!");
        }
        if (loseGame != null)
        {
            bool isActive = loseGame.activeSelf;
            if (isActive)
            {
                Debug.LogWarning("Lose game ");

                loseGame.SetActive(false);
                Debug.LogWarning("Lose game ");
                Time.timeScale = 1f;
            }
        }
        else
        {
            Debug.LogWarning("Lose game object is not assigned!");
        }

        Debug.Log("Reloading the current scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1f;

    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
