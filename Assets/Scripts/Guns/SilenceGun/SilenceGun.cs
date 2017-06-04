using UnityEngine;

public class SilenceGun : Gun
{
	[SerializeField] private int _inputMouseButton = 2;
	
	private void Update()
	{
		if (Input.GetMouseButtonDown(_inputMouseButton))
		{
			Fire();
		}
	}

	private void Fire()
	{
		var targetedObject = Targeter.TargetedGameObject;
		
		if (targetedObject != null)
		{
			var silencable = targetedObject.GetComponent<ISilencable>();

			if (silencable != null)
			{
				silencable.ToggleSilence();
			}
		}
	}
}
