//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UIWinLoseController : MonoBehaviour
//{
//    [SerializeField] private GameObject pauseMenu;
//    public void OnPausePressed()
//    {
//        Debug.Log("Button pause pressed");
//        if (pauseMenu != null)
//        {
//            bool isActive = pauseMenu.activeSelf;
//            pauseMenu.SetActive(!isActive);
//            Time.timeScale = isActive ? 1f : 0f;
//        }
//        else
//        {
//            Debug.LogWarning("Pause menu object is not assigned!");
//        }
//    }

//    public void OnResumePressed()
//    {
//        if (pauseMenu != null)
//        {
//            pauseMenu.SetActive(false);
//            Time.timeScale = 1f;
//        }
//        else
//        {
//            Debug.LogWarning("Pause menu object is not assigned!");
//        }
//    }

//    private void OnDestroy()
//    {
//        Time.timeScale = 1f;
//    }
//}
