using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuController : MonoBehaviour
{
    public void BackToMenu()
    {
        Debug.Log("Back to menu ");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.buttonClip);
        SceneManager.LoadScene("Menu");
    }
}
