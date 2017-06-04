using UnityEngine;

public class GrabGun : Gun 
{
	[SerializeField] private int _inputMouseButton;
	[SerializeField] private Transform _attractionPosition;
	
	[SerializeField] private float _force = 15;
	[SerializeField] private float _angularForce = 4;
	[SerializeField] private float _breakGrabSensibility = 4;
	[SerializeField] private float _releaseMaxVelocity = 2;

	private Grabbable _currentGrabbable;
	
	private float _grabTime;

	public float BreakGrabSensibility
	{
		get { return _breakGrabSensibility; }
	}

	private void Update () 
	{
		if (Input.GetMouseButtonDown(_inputMouseButton))
		{
			Fire();
		}

		if (Input.GetMouseButtonUp(_inputMouseButton) && 
		    _currentGrabbable)
		{
			Release();
		}
		
		if (_currentGrabbable)
		{
			RotateGrabbedObject();
		}		
	}

	private void Fire()
	{
		var targetedObject = Targeter.TargetedGameObject;

		if (targetedObject != null)
		{
			var grabbable = targetedObject.GetComponent<Grabbable>();
			
			if (grabbable != null)
			{	
				Grab(grabbable);
			}
		}
	}
	
	private void Grab(Grabbable grabbable)
	{		
		_currentGrabbable = grabbable;
		
		_grabTime = Time.timeSinceLevelLoad;
	
		_currentGrabbable.Rigidbody.useGravity = false;
		_currentGrabbable.OnGrabbableCollisionEnter += OnGrabbableCollisionEnter;
	}
	
	private void RotateGrabbedObject()
	{
		UpdateAttractionCenterRotation();
		
		var smoothTime = Time.deltaTime * _angularForce * (Mathf.Min(Time.timeSinceLevelLoad - _grabTime, 1f));
		
		_currentGrabbable.transform.rotation = Quaternion.Slerp(
			_currentGrabbable.transform.rotation, 
			_attractionPosition.rotation, 
			smoothTime);    
	}
	
	private void UpdateAttractionCenterRotation()
	{
		var targetedObjectRotation = _currentGrabbable.transform.rotation;
		var newAttractionCenterRotation = transform.rotation;

		targetedObjectRotation.GetSnap(ref newAttractionCenterRotation);
		
		_attractionPosition.rotation = newAttractionCenterRotation;
	}
	
	private void FixedUpdate()
	{
		if (_currentGrabbable)
		{
			MoveGrabbedObject();
		}
	}
	
	private void MoveGrabbedObject()
	{
		var deltaPosition = _attractionPosition.position - _currentGrabbable.Rigidbody.position;
		var targetVelocity = deltaPosition * _force;
		
		_currentGrabbable.Rigidbody.velocity = targetVelocity;
		_currentGrabbable.Rigidbody.angularVelocity = Vector3.zero;
	}
	
	private void OnGrabbableCollisionEnter(Collision collision)
	{
		if (collision.relativeVelocity.magnitude > _breakGrabSensibility)
		{
			Release();	
		}
	}
	
	public void Release()
	{	
		if (_currentGrabbable)
		{
			var newVelocity = _currentGrabbable.Rigidbody.velocity;
			
			newVelocity.x = Mathf.Clamp(newVelocity.x, -_releaseMaxVelocity, _releaseMaxVelocity);
			newVelocity.y = Mathf.Clamp(newVelocity.y, -_releaseMaxVelocity, _releaseMaxVelocity);
			newVelocity.z = Mathf.Clamp(newVelocity.z, -_releaseMaxVelocity, _releaseMaxVelocity);
			
			_currentGrabbable.Rigidbody.velocity = newVelocity;
			_currentGrabbable.Rigidbody.useGravity = true;
			_currentGrabbable.OnGrabbableCollisionEnter -= OnGrabbableCollisionEnter;
		
			_currentGrabbable = null;
			
			_attractionPosition.LookAt(transform);
			_attractionPosition.Rotate(new Vector3(0, 180, 0));
		}
	}
}
