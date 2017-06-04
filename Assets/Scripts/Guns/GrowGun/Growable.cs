using UnityEngine;

public class Growable : MonoBehaviour, IGrowable
{
	public void Grow(float sizeDelta)
	{
		transform.localScale += Vector3.one * sizeDelta;
	}

	public float Size
	{
		get { return transform.localScale.x; }
	}
}
