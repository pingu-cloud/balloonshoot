using UnityEngine;

public class RandomDriftMotion : IMotionPattern
{
    private float baseSpeed = 1f;
    private float driftStrength = 0.5f;
    private float speedMultiplier = 1f;

    public void Move(Transform balloonTransform)
    {
        float randomX = Random.Range(-driftStrength, driftStrength) * Time.deltaTime;
        float randomY = Random.Range(-driftStrength, driftStrength) * Time.deltaTime;
        balloonTransform.position += new Vector3(randomX, baseSpeed * speedMultiplier * Time.deltaTime + randomY, 0);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
