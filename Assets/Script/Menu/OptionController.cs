//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class OptionController : MonoBehaviour
//{
//    [SerializeField]
//    private Button map2UI;

//    [SerializeField]
//    private Button map3UI;

//    [SerializeField]
//    private GameManager gameManager;

//    [SerializeField]
//    private GameData gameData;

//    private void Start()
//    {
//        map2UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level2);
//        map3UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level3);
//    }

//    public void OnPlayMap1()
//    {
//        Debug.Log("Run Map 1");
//        SceneManager.LoadScene("GameLevel1");
//    }

//    public void OnPlayMap2()
//    {

//        Debug.Log("Run Map 2");
//        SceneManager.LoadScene("GameLevel2");

//    }
//    public void OnPlayMap3()
//    {
//        Debug.Log("Run Map 3");
//        SceneManager.LoadScene("GameLevel3");
//    }

//    public void OnResetMap()
//    {
//        gameData.InitializeLevels();
//        map2UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level2);
//        map3UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level3);
//    }
//}

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
        if (gameData == null)
        {
            Debug.LogError("GameData chưa được gán trong Inspector!");
            return;
        }

        // Cập nhật trạng thái nút ban đầu
        UpdateButtonStates();
    }

    public void OnPlayMap1()
    {
        Debug.Log("Run Map 1");
        SceneManager.LoadScene("GameLevel1");
    }

    public void OnPlayMap2()
    {
        if (!gameData.IsLevelUnlocked(GameLevel.Level2))
        {
            Debug.LogWarning("Level 2 chưa được mở khóa!");
            return;
        }

        Debug.Log("Run Map 2");
        SceneManager.LoadScene("GameLevel2");
    }

    public void OnPlayMap3()
    {
        if (!gameData.IsLevelUnlocked(GameLevel.Level3))
        {
            Debug.LogWarning("Level 3 chưa được mở khóa!");
            return;
        }

        Debug.Log("Run Map 3");
        SceneManager.LoadScene("GameLevel3");
    }

    public void OnResetMap()
    {
        Debug.Log("Reset trạng thái các level.");
        gameData.InitializeLevels();
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        if (gameData == null)
        {
            Debug.LogError("Không thể cập nhật trạng thái nút vì GameData chưa được gán!");
            return;
        }

        // Kiểm tra trạng thái mở khóa của từng level trong List levels
        map2UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level2);
        map3UI.interactable = gameData.IsLevelUnlocked(GameLevel.Level3);
    }


}
