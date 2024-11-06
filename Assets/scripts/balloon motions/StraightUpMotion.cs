using UnityEngine;

public class StraightUpMotion : IMotionPattern
{
    private float baseSpeed = 2f;
    private float speedMultiplier = 1f;

    public void Move(Transform balloonTransform)
    {
        balloonTransform.position += Vector3.up * baseSpeed * speedMultiplier * Time.deltaTime;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
