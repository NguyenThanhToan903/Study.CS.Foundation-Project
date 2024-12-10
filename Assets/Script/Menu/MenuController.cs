using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        Debug.Log("Play button pressed");
        Time.timeScale = 1.0f;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClip);
        SceneManager.LoadScene("GameLevel1");
    }

    public void OnOptionButtonPressed()
    {
        Debug.Log("Quit button pressed");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClip);
        SceneManager.LoadScene("MenuMap");
    }
}