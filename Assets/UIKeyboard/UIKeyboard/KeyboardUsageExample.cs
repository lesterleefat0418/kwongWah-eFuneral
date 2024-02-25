using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;


public class KeyboardUsageExample : MonoBehaviour
{
    public KeyboardController keyboard;
    public InputField outputText;

    private void Update() {
        outputText.text = keyboard.typedString;
    }
}
