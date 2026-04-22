using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string HighScoreKey = "highscore";

    public static ScoreManager Instance { get; private set; }

    [Header("Optional UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        CurrentScore = 0;
        HighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        if (points <= 0)
        {
            return;
        }

        CurrentScore += points;

        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt(HighScoreKey, HighScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        UpdateScoreUI();
    }

    public void SetScoreText(TextMeshProUGUI newScoreText)
    {
        scoreText = newScoreText;
        UpdateScoreUI();
    }

    public void SetHighScoreText(TextMeshProUGUI newHighScoreText)
    {
        highScoreText = newHighScoreText;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = CurrentScore.ToString();
        }

        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + HighScore;
        }
    }
}