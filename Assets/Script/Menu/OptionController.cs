using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionController : MonoBehaviour
{
    public void OnPlayMap1()
    {
        Debug.Log("Run Map 1");
        SceneManager.LoadScene("GameLevel1");
    }

    public void OnPlayMap2()
    {
        Debug.Log("Run Map 2");
        SceneManager.LoadScene("GameLevel2");
    }
    public void OnPlayMap3()
    {
        Debug.Log("Run Map 3");
        SceneManager.LoadScene("GameLevel3");
    }


}
