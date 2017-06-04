﻿using UnityEngine;

public abstract class Gun : MonoBehaviour
{
	protected Targeter Targeter;

	protected virtual void Awake()
	{
		Targeter = GetComponent<Targeter>();

		if (Targeter == null)
		{
			Debug.LogError("Gun needs a Targeter component to work properly", this);
		}
	}
}
