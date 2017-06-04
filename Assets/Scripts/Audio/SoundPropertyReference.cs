using UnityEngine;

public class SoundPropertyReference : MonoBehaviour
{
    [SerializeField] private float _spacingDistancePerPortion = 1f;
    [SerializeField] private float _secondsPerPortion = 0.5f;
    [SerializeField] private float _normalTempoHeightOverReferene = 2f;

    public float SecondsPerPortion
    {
        get { return _secondsPerPortion; }
    }

    /// <summary>
    /// Gets the amount of portions we should displace this element depending on the distance to the reference object. The amount of portions is defined by the music itself. 
    /// </summary>
    /// <param name="otherTransform">Position of the other object</param>
    /// <returns>Amount of seconds for the period</returns>
    public float GetPeriodInSeconds(Transform otherTransform)
    {
        var referenceAxis = transform.position.x;
        var actualAxis = otherTransform.position.x;
        var actualDistance = Mathf.Floor(Mathf.Abs(referenceAxis - actualAxis)) / _spacingDistancePerPortion;
        var actualPortions = Mathf.Max(1, (int) actualDistance);

        return actualPortions * _spacingDistancePerPortion;
    }

    public float GetTempoPercentage(Transform otherTransform)
    {
        var referenceAxis = transform.position.y;
        var actualAxis = otherTransform.position.y / _normalTempoHeightOverReferene;
        var calculatedPitch = actualAxis - referenceAxis;

        return Mathf.Max(0.1f, Mathf.Abs(calculatedPitch));
    }
}