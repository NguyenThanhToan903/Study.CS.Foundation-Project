using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuController : MonoBehaviour
{
    public void BackToMenu()
    {
        Debug.Log("Back to menu ");
        SceneManager.LoadScene("Menu");
    }
}
