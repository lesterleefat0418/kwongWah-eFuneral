using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
	//Place this on the buttons that act as the keys for the keyboard
	private string keyCharacter;
	private KeyboardController kbdController;

	private void Start() {
		//keyCharacter = gameObject.name.ToCharArray()[0];
		keyCharacter = GetKeyPressed();
		kbdController = GetComponentInParent<KeyboardController>(); 
		GetComponent<Button>().onClick.AddListener(KeyPressed);
	}

	public void KeyPressed() {
		if (gameObject.name == "Backspace") {
			kbdController.Backspace();
		} else {
			Debug.Log(keyCharacter);
			kbdController.GetKey(keyCharacter);
		}
		
	}

	private string GetKeyPressed()
	{
		foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
		{
			if (Input.GetKeyDown(keyCode))
			{
				string key = keyCode.ToString();
				return key;
			}
		}

		return "";
	}
}
