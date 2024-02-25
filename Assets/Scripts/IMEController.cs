using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IMEController : MonoBehaviour
{
    public static IMEController Instance =null;
    public InputField inputTextUI;
    public string hintText = "Type here...";

    private string inputText = "";

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Update()
    {
       /* if (Input.anyKeyDown)
        {
            string keyPressed = GetKeyPressed();

            if (!string.IsNullOrEmpty(keyPressed))
            {
                if (keyPressed == "Enter")
                {
                    Debug.Log("Input: " + inputText);
                    inputText = "";
                }
                else if (keyPressed == "Backspace")
                {
                    RemoveLastCharacter();
                }
                else
                {
                    inputText += keyPressed;
                    UpdateInputTextUI();
                }
            }
        }*/
    }

    public void GetKey(GetKeyCode code)
    {
        string keyPressed = code.key.ToString();
        EventSystem.current.SetSelectedGameObject(inputTextUI.gameObject, null);
        inputTextUI.OnPointerClick(new PointerEventData(EventSystem.current));

        Debug.Log(keyPressed);

        //this.inputText += keyPressed;
        //UpdateInputTextUI();

    }

    private void UpdateInputTextUI()
    {
        if (inputTextUI != null)
        {
            if (inputText == "")
            {
                inputTextUI.text = hintText;
            }
            else
            {
                inputTextUI.text = inputText;
            }
        }
    }

    private void RemoveLastCharacter()
    {
        if (inputText.Length > 0)
        {
            inputText = inputText.Remove(inputText.Length - 1);
            UpdateInputTextUI();
        }
    }
}
