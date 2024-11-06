using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the Pause Menu panel
    public Button pauseButton;    // Reference to the Pause button
    public AudioClip pauseSound; // Sound effect for toggling pause
    
    private AudioSource audioSource;

    private bool isPaused = false;

    void Start()
    {
        // Initialize
        Time.timeScale = 1;

        // Set up button listeners
        pauseButton.onClick.AddListener(TogglePause);

        // Get or add an AudioSource component
        audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // Play pause toggle sound
        PlaySoundEffect(pauseSound);

        if (isPaused)
        {
            // Show the pause menu and pause the game
            pauseMenu.SetActive(true);
            Time.timeScale = 0; // Freeze the game
        }
        else
        {
            // Hide the pause menu and resume the game
            pauseMenu.SetActive(false);
            Time.timeScale = 1; // Unfreeze the game
        }
    }

    

    private void PlaySoundEffect(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
