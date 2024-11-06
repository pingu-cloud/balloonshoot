using UnityEngine;
using TMPro; // Import this for TextMeshPro

public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // Reference to the text field for displaying high score

    void Start()
    {
        // Retrieve the high score from PlayerPrefs (default to 0 if it doesn't exist)
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update the text field to show the high score
        highScoreText.text = highScore.ToString();
    }
}
