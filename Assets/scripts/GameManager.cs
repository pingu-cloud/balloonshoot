using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ObjectPooler objectPooler; // Reference to ObjectPooler component for regular balloons
    public GameObject bounceBalloonPrefab; // Prefab specifically for BounceMotion balloons
    public Slider spawnFrequencySlider; // Reference to the slider
    public TextMeshProUGUI scoreText; // Reference to score display
    public TextMeshProUGUI comboText; // Reference to combo display
    public Image combo;
    private float spawnInterval;
    private float spawnTimer;
    private int score = 0; // Player's current score
    private int comboCount = 0; // Current combo count
    private float comboTimer = 0f; // Timer for maintaining the combo
    private float comboDuration = 2f; // Max time allowed between actions to keep the combo going
    private int comboMultiplier = 1; // Multiplier increases with combo count
    private float speedMultiplier = 1f; // Initial speed multiplier
    private int speedIncreaseThreshold = 200; // Score threshold for speed increase
    private int spawnSpeedThreshold = 400; // Threshold for spawn speed increase
    private bool hasSpawnedBounceBalloon = false; // Track if BounceMotion balloon has spawned at 5x combo

    private void Start()
    {
        spawnInterval = spawnFrequencySlider.value; // Initialize spawn interval with slider value
        spawnTimer = spawnInterval;

        // Add listener to update spawn interval when slider value changes
        spawnFrequencySlider.onValueChanged.AddListener(UpdateSpawnInterval);

        UpdateScoreText(); // Initialize the score display
        UpdateComboText(); // Initialize the combo display
    }

    private void Update()
    {
        // Manage spawn timer for balloons
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            SpawnRandomBalloonWithRandomPattern();
            spawnTimer = spawnInterval;
        }

        // Manage combo timer if there's an active combo
        if (comboCount > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0f)
            {
                ResetCombo();
            }
        }
    }

    private void SpawnRandomBalloonWithRandomPattern()
    {
        IMotionPattern motionPattern;

        // Check if we should spawn a BounceMotion balloon based on 5x combo
        if (comboMultiplier == 5 && !hasSpawnedBounceBalloon)
        {
            motionPattern = new BounceMotion();
            hasSpawnedBounceBalloon = true; // Mark as spawned
        }
        else
        {
            // Get a motion pattern based on the current score
            motionPattern = GetRandomBalloonPatternBasedOnScore();
        }

        GameObject balloon;

        // Spawn the bounce balloon prefab if using BounceMotion, otherwise use the object pool
        if (motionPattern is BounceMotion)
        {
            // Instantiate a new bounce balloon prefab
            balloon = Instantiate(bounceBalloonPrefab, new Vector3(Random.Range(-5f, 5f), -5f, 0), Quaternion.identity);
        }
        else
        {
            // Use object pool for regular balloons
            int balloonType = Random.Range(1, 7);
            string balloonTag = "Balloon" + balloonType;

            Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), -5f, 0);
            balloon = objectPooler.SpawnFromPool(balloonTag, spawnPosition, Quaternion.identity);
        }

        // Set motion pattern and apply speed multiplier (only for non-BounceMotion balloons)
        Balloon balloonScript = balloon.GetComponent<Balloon>();
        balloonScript.SetMotionPattern(motionPattern);

        // Only apply speed multiplier to balloons that aren't BounceMotion
        if (!(motionPattern is BounceMotion))
        {
            balloonScript.SetSpeedMultiplier(speedMultiplier);
        }
    }

    private IMotionPattern GetRandomBalloonPatternBasedOnScore()
    {
        // Logic for determining motion patterns based on score (e.g., only certain patterns at certain scores)
        if (score < 400)
        {
            int patternType = Random.Range(0, 2);
            if (patternType == 0) return new StraightUpMotion();
            else return new DiagonalUpMotion();
        }
        else if (score < 600)
        {
            int patternType = Random.Range(0, 4);
            switch (patternType)
            {
                case 0: return new StraightUpMotion();
                case 1: return new DiagonalUpMotion();
                case 2: return new FloatingPauseMotion();
                case 3: return new RandomDriftMotion();
            }
        }
        else if (score < 800)
        {
            int patternType = Random.Range(0, 5);
            switch (patternType)
            {
                case 0: return new StraightUpMotion();
                case 1: return new DiagonalUpMotion();
                case 2: return new FloatingPauseMotion();
                case 3: return new RandomDriftMotion();
                case 4: return new ZigzagMotion();
            }
        }
        else
        {
            int patternType = Random.Range(0, 5);
            switch (patternType)
            {
                case 0: return new StraightUpMotion();
                case 1: return new DiagonalUpMotion();
                case 2: return new FloatingPauseMotion();
                case 3: return new RandomDriftMotion();
                case 4: return new ZigzagMotion();
            }
        }

        return new StraightUpMotion(); // Default motion pattern if none of the conditions match
    }

    public void AddScore(int points, bool isSpecialBalloon = false)
    {
        // Only increment score without affecting combo if it's a special balloon
        if (!isSpecialBalloon)
        {
            score += points * comboMultiplier;
            IncreaseCombo();
        }
        else
        {
            score += points; // Add score directly without applying combo
        }

        UpdateScoreText();
        CheckForSpeedIncrease();
    }

    private void UpdateScoreText()
    {
        scoreText.text =  score.ToString();
    }

    private void UpdateComboText()
    {
        if (comboMultiplier > 1)
        {
            comboText.text =  comboMultiplier.ToString()+"x";
            combo.gameObject.SetActive(true);
            comboText.gameObject.SetActive(true);
        }
        else
        {
            combo.gameObject.SetActive(false);
            comboText.gameObject.SetActive(false);
        }
    }

    private void IncreaseCombo()
    {
        comboCount++;
        comboTimer = comboDuration;
        comboMultiplier = Mathf.Clamp(comboCount, 1, 5);
        UpdateComboText();
    }

    public void ResetCombo()
    {
        comboCount = 0;
        comboMultiplier = 1;
        hasSpawnedBounceBalloon = false; // Reset spawn condition for the bounce balloon
        UpdateComboText();
    }

    private void CheckForSpeedIncrease()
    {
        if (score >= speedIncreaseThreshold)
        {
            speedMultiplier += 0.6f;
            speedIncreaseThreshold += 200;

            foreach (Balloon balloon in FindObjectsOfType<Balloon>())
            {
                if (!(balloon.GetMotionPattern() is BounceMotion))
                {
                    balloon.SetSpeedMultiplier(speedMultiplier);
                }
            }
        }

        if (score >= spawnSpeedThreshold)
        {
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.2f);
            spawnSpeedThreshold += 400;
        }
    }

    public void AdjustSpeedMultiplier(float adjustment)
    {
        speedMultiplier = Mathf.Max(0.1f, speedMultiplier + adjustment);
        UpdateBalloonSpeeds();
    }

    private void UpdateBalloonSpeeds()
    {
        foreach (Balloon balloon in FindObjectsOfType<Balloon>())
        {
            if (!(balloon.GetMotionPattern() is BounceMotion))
            {
                balloon.SetSpeedMultiplier(speedMultiplier);
            }
        }
    }

    private void UpdateSpawnInterval(float value)
    {
        spawnInterval = value;
    }
}
