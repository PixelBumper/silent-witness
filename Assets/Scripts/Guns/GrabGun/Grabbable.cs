using System;
using UnityEngine;

public class Grabbable : MonoBehaviour 
{
	public event Action<Collision> OnGrabbableCollisionEnter;
	private Rigidbody _rigidbody;
	
	public Rigidbody Rigidbody
	{
		get
		{
			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();

				if (_rigidbody == null)
				{
					Debug.LogError("Grabbable needs a Rigidbody component to work properly", this);
				}
			}

			return _rigidbody;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (OnGrabbableCollisionEnter != null)
		{
			OnGrabbableCollisionEnter(collision);
		}
	}
}
