using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	private void Start()
	{
		
	
	}

	public void StartGame()
	{
		MainController.SwitchScene("GameScene");
	}
}
