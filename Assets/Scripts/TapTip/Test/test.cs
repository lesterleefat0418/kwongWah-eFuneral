using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class test : MonoBehaviour
{
    [DllImport("User32.dll", EntryPoint = "keybd_event")]
    static extern void keybd_event(
      byte bVk,
      byte bScan,
      int dwFlags,
      int dwExtraInfo
    );
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        string compositionString = Input.compositionString;

        if (!string.IsNullOrEmpty(compositionString))
        {
            // The user is currently typing a character or composing a string
            // You can access the current input key using compositionString[0]
            char currentInputKey = compositionString[0];

            foreach ( char c in compositionString )
            {
                // Do something with the current input key
                Debug.Log("Current Input Key: " + c);
            }
        }
    }

    public void OnVirtualButtonClicked(string inputKey)
    {
        // Perform any desired action with the input key
        Debug.Log("Virtual Button Clicked: " + Input.compositionString);

    }

}
