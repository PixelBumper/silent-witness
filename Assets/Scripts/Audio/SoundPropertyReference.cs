using UnityEngine;

public class SoundPropertyReference : MonoBehaviour
{
    [SerializeField] private float _spacingDistance = 10f;

    public float GetSpacingSecondsMultiplier(Transform otherTransform)
    {
        var referenceAxis = transform.position.x;
        var actualAxis = otherTransform.position.x;
        var actualDistance = Mathf.Abs(referenceAxis - actualAxis);
        var adjustedDistance = Mathf.Max((actualDistance - _spacingDistance) / _spacingDistance + 1f, 0.1f);

        return adjustedDistance;
    }

    public float GetTempoPercentage(Transform otherTransform)
    {
        var referenceAxis = transform.position.y;
        var actualAxis = otherTransform.position.y;
        return Mathf.Abs(referenceAxis - actualAxis);
    }
}