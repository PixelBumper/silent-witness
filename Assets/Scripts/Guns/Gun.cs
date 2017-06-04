using UnityEngine;

public abstract class Gun : MonoBehaviour
{
	[SerializeField] private GunIcon _icon;
	protected Targeter Targeter;

	protected virtual void Awake()
	{
		Targeter = GetComponent<Targeter>();

		if (Targeter == null)
		{
			Debug.LogError("Gun needs a Targeter component to work properly", this);
		}
	}

	public void Enable()
	{
		enabled = true;
		
		if (_icon)
		{
			_icon.Enable();
		}
	}

	public void Disable()
	{
		enabled = false;
		
		if (_icon)
		{
			_icon.Disable();
		}
	}
}
