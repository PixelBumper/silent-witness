using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

	public Vector3 SpawningPosition = new Vector3(0, 1, 0);

	public float RespawningHeight = -10f;
	
	private GameObject _character;
	// Use this for initialization
	void Start ()
	{
		_character=GameObject.Find("Character");
	}
	
	// Update is called once per frame
	void Update () {
		if (_character.transform.position.y < -RespawningHeight)
		{
			_character.transform.position = SpawningPosition;
		}
	
	}
}
