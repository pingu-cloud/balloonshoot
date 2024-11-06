using UnityEngine;

public class DiagonalUpMotion : IMotionPattern
{
    private float baseSpeed = 2f;
    private float speedMultiplier = 1f;

    public void Move(Transform balloonTransform)
    {
        balloonTransform.position += new Vector3(1, 1, 0).normalized * baseSpeed * speedMultiplier * Time.deltaTime;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }
}
