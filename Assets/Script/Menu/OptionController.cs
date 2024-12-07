using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    [SerializeField]
    private Button map2UI;

    [SerializeField]
    private Button map3UI;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameData gameData;

    private void Start()
    {
        map2UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level2);
        map3UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level3);
    }

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

    public void OnResetMap()
    {
        gameData.InitializeLevels();
        map2UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level2);
        map3UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level3);
    }
}
