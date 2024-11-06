using UnityEngine;

public interface IMotionPattern
{
    void Move(Transform balloonTransform);

    void SetSpeedMultiplier(float multiplier); // New method to adjust speed
}
