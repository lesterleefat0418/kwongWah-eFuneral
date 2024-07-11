﻿using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class FocusWindow : Singleton<FocusWindow>
{
    public float reFocusTime = 0.1f;
    private Coroutine m_task;

    private bool m_isOn = true;
    public bool isOn {
        get {
            return m_isOn;
        }

        set {
            if (m_isOn != value)
            {
                m_isOn = value;

                if (m_isOn) {
                    StartCoroutine(RefocusWindow(2f));
                }
                else{
                    StopAllCoroutines();
                }
            }
        }
    }

#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    private IntPtr unityWindow;

    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    const int ALT = 0xA4;
    const int EXTENDEDKEY = 0x1;
    const int KEYUP = 0x2;

    public const uint SWP_NOMOVE = 0x2;
    public const uint SWP_NOSIZE = 0x1;
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    private static readonly IntPtr HWND_NOTTOPMOST = new IntPtr(-2);
    /*public bool isTopMostEnabled = true;

    private void SetTopMost()
    {
        if(isTopMostEnabled)
           SetWindowPos(unityWindow, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
    }*/

    private IEnumerator RefocusWindow(float waitSeconds)
    {
        while(m_isOn /*&& isTopMostEnabled*/) { 
            // wait for new window to appear
            yield return new WaitWhile(() => unityWindow == GetActiveWindow());

            yield return new WaitForSeconds(waitSeconds);

            // Simulate alt press
            keybd_event((byte)ALT, 0x45, EXTENDEDKEY | 0, 0);

            // Simulate alt release
            keybd_event((byte)ALT, 0x45, EXTENDEDKEY | KEYUP, 0);

            SetForegroundWindow(unityWindow);
        }
    }
#else
    private IEnumerator RefocusWindow(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
    }
#endif
    // Start is called before the first frame update
    protected virtual void Start()
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        unityWindow = GetActiveWindow();
        //SetTopMost();
        StartCoroutine(RefocusWindow(this.reFocusTime));
#endif
    }

    public void EnableScreenFoucs()
    {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
        unityWindow = GetActiveWindow();
        //SetTopMost();
        StartCoroutine(RefocusWindow(this.reFocusTime));
#endif
    }

   /* private void OnApplicationQuit()
    {
        this.isTopMostEnabled = false;
    }*/
}
