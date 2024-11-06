using UnityEngine;

public class ZigzagMotion : IMotionPattern
{
    private float baseSpeed = 2f;
    private float frequency = 5f;
    private float amplitude = 5f;
    private float speedMultiplier = 1f;

    public void Move(Transform balloonTransform)
    {
        Vector3 position = balloonTransform.position;
        position += Vector3.up * baseSpeed * speedMultiplier * Time.deltaTime;
        position.x += Mathf.Sin(Time.time * frequency) * amplitude * Time.deltaTime;
        balloonTransform.position = position;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
