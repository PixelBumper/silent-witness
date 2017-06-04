using UnityEngine;

public abstract class Gun : MonoBehaviour
{
	protected Targeter Targeter;

	protected virtual void Awake()
	{
		Targeter = GetComponent<Targeter>();
	}
}
