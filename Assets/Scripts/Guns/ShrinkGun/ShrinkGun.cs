using UnityEngine;

public class ShrinkGun : Gun 
{
	[SerializeField] private int _inputMouseButton = 1;
	[SerializeField] private float _speed = 0.3f;
	[SerializeField] private float _minimumSize = 3f;
	
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
			var shrinkable = targetedObject.GetComponent<IShrinkable>();

			if (shrinkable != null)
			{
				var targetSizeDelta = _speed * Time.deltaTime;
				var deltaSize = Mathf.Clamp((shrinkable.Size - _minimumSize) * Time.deltaTime, 0, targetSizeDelta);
				shrinkable.Shrink(deltaSize);
			}
		}
	}
}
