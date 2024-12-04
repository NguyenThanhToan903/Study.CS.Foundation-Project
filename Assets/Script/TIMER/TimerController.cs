using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private GameObject loseGame;

    [SerializeField]
    private GameObject winGame;

    [SerializeField]
    private GameObject timerUI;

    [SerializeField]
    private Image timerForeground;

    [SerializeField]
    private float timeRemaining;

    [SerializeField]
    private float maxTime = 10.0f;

    private void Start()
    {
        timeRemaining = maxTime;
    }

    private void Update()
    {
        if (timeRemaining > 0.0f)
        {
            timeRemaining -= Time.deltaTime;
            timerForeground.fillAmount = timeRemaining / maxTime;
        }
        else
        {
            loseGame.SetActive(true);
            timerUI.SetActive(false);
            Time.timeScale = 0.0f;
        }
    }

    public void WinGame()
    {
        winGame.SetActive(true);
        timerUI.SetActive(false);
        Time.timeScale = 0.0f;
        Debug.Log("You caught all the rabbits!");
    }
}
