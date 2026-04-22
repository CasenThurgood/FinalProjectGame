using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    [SerializeField] private string highScoreKey = "highscore";
    [SerializeField] private string label = "Best Score: ";

    private TextMeshProUGUI textComponent;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
            if (textComponent == null)
            {
                return;
            }
        }

        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        textComponent.text = label + highScore;
    }
}