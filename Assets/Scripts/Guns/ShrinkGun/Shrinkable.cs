using UnityEngine;

public class Shrinkable : MonoBehaviour, IShrinkable
{
	public void Shrink(float sizeDelta)
	{
		transform.localScale -= Vector3.one * sizeDelta;
	}

	public float Size
	{
		get { return transform.localScale.x; }
	}
}
