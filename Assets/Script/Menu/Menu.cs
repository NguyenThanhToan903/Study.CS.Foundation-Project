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

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void Start()
    {
        playerInput.Navigate.Esc.started += _ => TogglePauseGame();
    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void TogglePauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            pauseMenuGame.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
