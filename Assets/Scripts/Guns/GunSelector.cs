using System;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{
	[SerializeField] private List<Gun> _guns;
	[SerializeField] private float _delay = .1f;
	
	private float _lastSelectTime;
	private int _currentGunIndex;

	private void Start()
	{
		_guns[0].Enable();
		
		for (var i = 1; i < _guns.Count; i++)
		{
			_guns[i].Disable();
		}
	}

	private void Update()
	{
		if (Math.Abs(Input.mouseScrollDelta.y) > Mathf.Epsilon &&
		    Time.time - _lastSelectTime >= _delay)
		{
			_lastSelectTime = Time.time;

			var delta = -(int)Mathf.Sign(Input.mouseScrollDelta.y);
			
			_guns[_currentGunIndex].Disable();
			
			_currentGunIndex = (_currentGunIndex + _guns.Count + delta) % _guns.Count;

			_guns[_currentGunIndex].Enable();
		}
	}
}
