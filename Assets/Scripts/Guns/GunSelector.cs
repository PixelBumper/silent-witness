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

			SelectGun((_currentGunIndex + _guns.Count + delta) % _guns.Count);

		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			SelectGun(0 % _guns.Count);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			SelectGun(1 % _guns.Count);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			SelectGun(2 % _guns.Count);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			SelectGun(3 % _guns.Count);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			SelectGun(4 % _guns.Count);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			SelectGun(5 % _guns.Count);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			SelectGun(6 % _guns.Count);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha8))
		{
			SelectGun(7 % _guns.Count);
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha9))
		{
			SelectGun(8 % _guns.Count);
		}
	}

	private void SelectGun(int gunIndex)
	{
		_guns[_currentGunIndex].Disable();

		_currentGunIndex = gunIndex;

		_guns[_currentGunIndex].Enable();
	}
}
