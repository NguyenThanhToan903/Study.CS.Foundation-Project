using System.Collections.Generic;
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
    private List<string> listName;
    [SerializeField]
    private List<bool> listIsLock;

    private void Awake()
    {
        UpdateButtonStates();
    }

    private void Start()
    {
        listName = GameManager.Instance.Levels.ConvertAll(level => level.levelName);
        listIsLock = GameManager.Instance.Levels.ConvertAll(level => level.isUnlocked);
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
        Debug.Log("Reset trang thai level");
        GameManager.Instance.LockLevel("GameLevel2");
        GameManager.Instance.LockLevel("GameLevel3");
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        map2UI.interactable = GameManager.Instance.IsLevelUnlocked("GameLevel2");
        map3UI.interactable = GameManager.Instance.IsLevelUnlocked("GameLevel3");
    }


}
