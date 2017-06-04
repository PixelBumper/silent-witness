using UnityEngine;

public class GrowGun : Gun 
{
	[SerializeField] private int _inputMouseButton = 1;
	[SerializeField] private float _speed = 0.3f;
	[SerializeField] private float _maximumSize = 3f;
	
	private void Update()
	{
		if (Input.GetMouseButton(_inputMouseButton))
		{
			Fire();
		}
	}

	private void Fire()
	{
		var targetedObject = Targeter.TargetedGameObject;
		
		if (targetedObject != null)
		{
			var growable = targetedObject.GetComponent<IGrowable>();

			if (growable != null)
			{
				var targetSizeDelta = _speed * Time.deltaTime;
				var deltaSize = Mathf.Clamp((_maximumSize - growable.Size) * Time.deltaTime, 0, targetSizeDelta);
				growable.Grow(deltaSize);
			}
		}
	}
}
