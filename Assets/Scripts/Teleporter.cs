using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

	public GameObject ExitPlatform;

	public float TeleporterRange = 1f; 
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	
	private void OnTriggerStay(Collider other)
	{
		if (Vector3.Distance(other.transform.position, transform.position)<=TeleporterRange)
		{
			var transformPosition = ExitPlatform.transform.position;
			transformPosition.y = transformPosition.y + 0.3f;
			other.transform.position = transformPosition;
		}
	}
}
