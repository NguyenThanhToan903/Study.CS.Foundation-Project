using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        Debug.Log("Play button pressed");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("GameLevel1");

    }

    public void OnSettingsButtonPressed()
    {
        Debug.Log("Settings button pressed");
        SceneManager.LoadScene("Setting");
    }

    public void OnOptionButtonPressed()
    {
        Debug.Log("Quit button pressed");
        SceneManager.LoadScene("MenuMap");
    }
}