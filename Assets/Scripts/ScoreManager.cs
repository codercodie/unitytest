using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString(); score.ToString();
    }

    public void ScoreIncrease(string rank)
    {
        if (rank == "Perfect")
        {
            score += 3;
            scoreText.text = "SCORE: " + score.ToString(); score.ToString();
        }
        if (rank == "Close")
        {
            score += 1;
            scoreText.text = "SCORE: " + score.ToString();
        }
    }
}
