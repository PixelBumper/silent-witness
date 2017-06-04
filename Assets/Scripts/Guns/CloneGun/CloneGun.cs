using UnityEngine;

public class CloneGun : Gun 
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
			var clonable = targetedObject.GetComponent<IClonable>();

			if (clonable != null)
			{
				clonable.Clone();
			}
		}
	}
}
