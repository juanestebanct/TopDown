using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public static Score Instance { get; set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Next level")]
    public int NextLevelPoinst;
    public int Level;
    public int CurrentScore, HighScore;

    [Header("Ui Next level")]
    [SerializeField] private Image barLevel;
    [SerializeField] private TextMeshProUGUI TextLevel;
    [SerializeField] private UpdateControllers updateControllers;
    private void Awake()
    {
        Level = 1;
        Instance = this;
        if (PlayerPrefs.HasKey("HighScore")) HighScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = $"High Score: {HighScore}";
        scoreText.text = $"Score: {0}";
    }
    private void Start()
    {
        barLevel.fillAmount = (CurrentScore / NextLevelPoinst);
        TextLevel.text = $"{CurrentScore}/{NextLevelPoinst}";
    }
    public void GetPoins(int poins)
    {
        
        CurrentScore +=  poins;

        barLevel.fillAmount = ((float)CurrentScore / (float)NextLevelPoinst);
        TextLevel.text = $"{CurrentScore}/{NextLevelPoinst}";

        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt("HighScore", HighScore);
            highScoreText.text = $"High Score: \n{HighScore}";
        }
        scoreText.text = $"Score: {CurrentScore}";
    }
    public void PastLevel()
    {
        updateControllers.OpenMenuUpdate();
        print("se paso el nivel y se coloca la mejora ");
        Level++;
        barLevel.fillAmount = ((float)CurrentScore / (float)NextLevelPoinst);
        TextLevel.text = $"{CurrentScore}/{NextLevelPoinst}";
    } 
}
