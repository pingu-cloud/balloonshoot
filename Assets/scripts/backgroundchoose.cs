using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button button4;
    public string nextSceneName; // Name of the next scene to load
    public AudioClip buttonClickSound; // Sound effect for button press
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign button listeners
        button1.onClick.AddListener(() => OnButtonPressed(1));
        button2.onClick.AddListener(() => OnButtonPressed(2));
        button3.onClick.AddListener(() => OnButtonPressed(3));
        button4.onClick.AddListener(() => OnButtonPressed(4));
    }

    void OnButtonPressed(int buttonValue)
    {
        // Play button click sound
        PlayButtonClickSound();

        // Store the integer value in PlayerPrefs
        PlayerPrefs.SetInt("ButtonValue", buttonValue);

        // Save PlayerPrefs to ensure it's stored immediately
        PlayerPrefs.Save();

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

    private void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
