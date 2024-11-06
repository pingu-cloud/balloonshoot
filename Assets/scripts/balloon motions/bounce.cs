using UnityEngine;

public class BounceMotion : IMotionPattern
{
    private float baseSpeed = 0.4f;
    private float bounceHeight = 0.2f;
    private float bounceSpeed = 0.4f;
    private float speedMultiplier = 1f; // Default multiplier

    public void Move(Transform balloonTransform)
    {
        float bounce = Mathf.PingPong(Time.time * bounceSpeed, bounceHeight);
        balloonTransform.position += new Vector3(0, baseSpeed * speedMultiplier * Time.deltaTime + bounce, 0);
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
