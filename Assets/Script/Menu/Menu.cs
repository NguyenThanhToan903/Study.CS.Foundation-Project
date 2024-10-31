using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuGame;

    [SerializeField]
    private PlayerInput playerInput;

    private bool isPaused = false;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void Start()
    {
        playerInput.Navigate.Esc.started += _ => PauseGame();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuGame.SetActive(true);
            isPaused = true;
        }
        else
        {
            ResumeGame();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuGame.SetActive(false);
        isPaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
