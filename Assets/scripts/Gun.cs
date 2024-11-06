using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject muzzleFlash;
    public Transform muzzlePosition;
    public TextMeshProUGUI livesText;
    public Button pauseButton;
    public string gameOverSceneName;
    public TextMeshProUGUI score;

    public AudioClip hitSound; // Sound effect for hitting a balloon
    public AudioClip missSound; // Sound effect for missing a balloon
    private AudioSource audioSource;

    public GameObject balloonPopEffect; // Reference to the balloon pop particle effect prefab

    private Animation gunAnimation;
    private bool isFlashActive = false;
    private int lives = 10;
    private bool isPaused = false;

    void Start()
    {
        gunAnimation = GetComponent<Animation>();
        audioSource = GetComponent<AudioSource>();

        if (muzzleFlash != null) muzzleFlash.SetActive(false);

        UpdateLivesText();
        pauseButton.onClick.AddListener(TogglePause);
    }

    void Update()
    {
        if (isPaused) return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Shoot();
        }
#endif
    }

    private void Shoot()
    {
        if (isPaused || IsPointerOverUIObject()) return;

        if (gunAnimation != null && gunAnimation.clip != null)
        {
            gunAnimation.Play();
        }

        if (!isFlashActive && muzzleFlash != null && muzzlePosition != null)
        {
            StartCoroutine(ShowMuzzleFlash());
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Balloon"))
            {
                // Play the particle effect at the balloon's position
                Instantiate(balloonPopEffect, hit.transform.position, Quaternion.identity);

                // Set the balloon inactive
                hit.collider.gameObject.SetActive(false);

                Balloon balloonScript = hit.collider.GetComponent<Balloon>();
                PlayHitSound();

                if (balloonScript != null)
                {
                    IMotionPattern motionPattern = balloonScript.GetMotionPattern();

                    if (motionPattern is BounceMotion)
                    {
                        FindObjectOfType<GameManager>().AddScore(100, true);
                        FindObjectOfType<GameManager>().AdjustSpeedMultiplier(-0.8f);
                    }
                    else if (motionPattern is ZigzagMotion)
                    {
                        FindObjectOfType<GameManager>().AddScore(15);
                    }
                    else if (motionPattern is DiagonalUpMotion)
                    {
                        FindObjectOfType<GameManager>().AddScore(20);
                    }
                    else
                    {
                        FindObjectOfType<GameManager>().AddScore(10);
                    }
                }
            }
            else
            {
                PlayMissSound();
                FindObjectOfType<GameManager>().ResetCombo();
                LoseLife();
            }
        }
        else
        {
            PlayMissSound();
            FindObjectOfType<GameManager>().ResetCombo();
            LoseLife();
        }
    }

    private IEnumerator ShowMuzzleFlash()
    {
        isFlashActive = true;
        muzzleFlash.transform.position = muzzlePosition.position;
        muzzleFlash.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        muzzleFlash.SetActive(false);
        isFlashActive = false;
    }

    private void LoseLife()
    {
        if (isPaused) return;

        lives--;
        UpdateLivesText();

        if (lives <= 0)
        {
            CheckAndSaveHighScore();
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    private void UpdateLivesText()
    {
        livesText.text = lives.ToString();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    private void CheckAndSaveHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (int.Parse(score.text) > highScore)
        {
            Debug.Log("highscore");
            PlayerPrefs.SetInt("HighScore", int.Parse(score.text));
            PlayerPrefs.Save();
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    private void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    private void PlayMissSound()
    {
        if (audioSource != null && missSound != null)
        {
            audioSource.PlayOneShot(missSound);
        }
    }
}
