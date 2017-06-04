using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
[RequireComponent(typeof(Renderer))]
public class MovingWalkway : MonoBehaviour
{

	private Renderer _renderer;

	public float Speed=1.0f;
	
	private float _timepassed = 0.0f;
	// Use this for initialization
	void Start ()
	{
		_renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnTriggerStay(Collider collider)
	{
		_timepassed += Time.deltaTime;
		_renderer.material.SetTextureOffset("_MainTex", new Vector2(0, _timepassed));
		if (collider.gameObject.layer == LayerMask.NameToLayer("Character&Monster"))
		{
			var rigidbody = collider.GetComponent<Rigidbody>();
			if (rigidbody != null)
			{
				var currentPosition = collider.transform.position;
				rigidbody.MovePosition(currentPosition + transform.forward * Speed * Time.deltaTime);	
			}
			
		}

	}
}
