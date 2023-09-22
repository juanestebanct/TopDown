using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Score : MonoBehaviour
{
    public static Score Instance { get; set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public int currentScore, highScore;

    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.HasKey("HighScore")) highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = $"High Score: \n{highScore}";
        scoreText.text = $"Score: \n{0}";
    }
    public void GetPoins(int poins)
    {
        currentScore +=  poins;
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            highScoreText.text = $"High Score: \n{highScore}";
        }
        scoreText.text = $"Score: \n{currentScore}";
    }
}
