using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public TextMeshProUGUI score;
    private int scoreNum;


    private void Start()
    {
        scoreNum = 0;
        score.text = "Catched: " + scoreNum;
    }

    public void takeScore()
    {
        scoreNum++;
        score.text = "Catched: " + scoreNum;
    }


}
