using UnityEngine;

public class FloatingPauseMotion : IMotionPattern
{
    private float baseSpeed = 2f;
    private float pauseDuration = 1f;
    private float moveDuration = 2f;
    private float timer = 0f;
    private bool isPaused = false;
    private float speedMultiplier = 1f;

    public void Move(Transform balloonTransform)
    {
        timer += Time.deltaTime;

        if (isPaused)
        {
            if (timer >= pauseDuration)
            {
                timer = 0f;
                isPaused = false;
            }
        }
        else
        {
            balloonTransform.position += Vector3.up * baseSpeed * speedMultiplier * Time.deltaTime;
            if (timer >= moveDuration)
            {
                timer = 0f;
                isPaused = true;
            }
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
