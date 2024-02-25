using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyboardControl;

public class GetKeyCode : MonoBehaviour
{
    public Keyboard keyboard;
    public Keyboard.ScanCodeShort key = Keyboard.ScanCodeShort.NONAME;

    public void setKeyCode()
    {
        keyboard.Send(this.key);
    }
}
