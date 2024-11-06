using UnityEngine;

public class Balloon : MonoBehaviour
{
    private IMotionPattern motionPattern;

    public void SetMotionPattern(IMotionPattern pattern)
    {
        motionPattern = pattern;
    }

    public IMotionPattern GetMotionPattern()
    {
        return motionPattern;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        if (motionPattern != null)
        {
            motionPattern.SetSpeedMultiplier(multiplier);
        }
    }
    private void Update()
    {
        // Ensure the balloon moves if a motion pattern is assigned
        if (motionPattern != null)
        {
            motionPattern.Move(transform);
        }
    }
}
