using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class playmodeSceneTransition : MonoBehaviour
{
    public TextMeshProUGUI score;
    public Button transitionButton; // Button to trigger the scene transition
    public string sceneName; // Name of the scene to load
    public AudioClip buttonClickSound; // Sound effect for button press
    private AudioSource audioSource;

    void Start()
    {
        // Ensure that the button and scene name are assigned
        if (transitionButton != null && !string.IsNullOrEmpty(sceneName))
        {
            transitionButton.onClick.AddListener(OnButtonClicked); // Add listener for the button click
        }
        else
        {
            Debug.LogWarning("Button or scene name not assigned in the inspector!");
        }

        // Get or add an AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnButtonClicked()
    {
        // Play button click sound
        PlayButtonClickSound();

        // Transition to the specified scene
        TransitionToScene();
    }

    void TransitionToScene()
    {
        CheckAndSaveHighScore();
        SceneManager.LoadScene(sceneName); // Load the specified scene
    }

    private void CheckAndSaveHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (int.Parse(score.text) > highScore)
        {
            Debug.Log("New high score!");
            PlayerPrefs.SetInt("HighScore", int.Parse(score.text));
            PlayerPrefs.Save();
        }
    }

    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
