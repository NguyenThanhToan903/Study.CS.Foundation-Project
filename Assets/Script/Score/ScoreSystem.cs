using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI score;

    [SerializeField]
    private GameObject winGame;

    [SerializeField]
    private TimerController timerController;

    [SerializeField]
    private RabbitSpawnManager rabbitSpawnManager;

    [SerializeField]
    private int scoreNum;

    [SerializeField]
    private GameManager gameManager;

    private void Start()
    {
        scoreNum = 0;
        score.text = "Catched: " + scoreNum;
    }

    public void takeScore()
    {
        scoreNum++;
        score.text = "Catched: " + scoreNum;
        if (scoreNum >= rabbitSpawnManager.GetRabbitCount())
        {
            timerController.WinGame();
            gameManager.UnlockNextLevel();
        }
    }
}
