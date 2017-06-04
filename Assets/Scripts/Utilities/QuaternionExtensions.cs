using UnityEngine;

public static class QuaternionExtensions
{
	public static void GetSnap(this Quaternion fromRotation, ref Quaternion toRotation, float step = 90)
	{
		fromRotation.GetSnapX(ref toRotation, step);
		fromRotation.GetSnapY(ref toRotation, step);
		fromRotation.GetSnapZ(ref toRotation, step);
	}

	public static void GetSnapX(this Quaternion fromRotation, ref Quaternion toRotation, float step = 90)
	{
		var rotation = Mathf.Repeat(toRotation.eulerAngles.x - fromRotation.eulerAngles.x, 360);
		var newRotation = GetSnapAngle(rotation, step);
		toRotation *= Quaternion.Euler(newRotation, 0, 0);
	}
	
	public static void GetSnapY(this Quaternion fromRotation, ref Quaternion toRotation, float step = 90)
	{
		var rotation = Mathf.Repeat(toRotation.eulerAngles.y - fromRotation.eulerAngles.y, 360);
		var newRotation = GetSnapAngle(rotation, step);
		toRotation *= Quaternion.Euler(0, newRotation, 0);
	}
	
	public static void GetSnapZ(this Quaternion fromRotation, ref Quaternion toRotation, float step = 90)
	{
		var rotation = Mathf.Repeat(toRotation.eulerAngles.z - fromRotation.eulerAngles.z, 360);
		var newRotation = GetSnapAngle(rotation, step);
		toRotation *= Quaternion.Euler(0, 0, newRotation);
	}
	
	private static float GetSnapAngle(float rotation, float step)
	{
		var halfStep = step / 2f;
		var stepCount = 360f / step;

		for (var i = 1; i <= stepCount; i++)
		{
			var currentStep = step * i;
			
			if (rotation >= currentStep - halfStep && rotation < currentStep + halfStep)
			{
				return (stepCount - i) * step;
			}
		}

		return rotation;
	}
}
