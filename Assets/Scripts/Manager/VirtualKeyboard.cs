using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using UnityEngine.XR;
using Microsoft.Win32;

public class VirtualKeyboard: MonoBehaviour
{
    public static VirtualKeyboard Instance = null;

    [DllImport("user32")]
    static extern IntPtr FindWindow(String sClassName, String sAppName);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        private const int SWP_NOSIZE = 0x0001;
    private const int SWP_NOZORDER = 0x0004;
    [DllImport("user32")]
    static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

    private static Process _onScreenKeyboardProcess = null;

    public void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        getKey();
    }

    //Show the touch keyboard (tabtip.exe).
    public void ShowTouchKeyboard()
    {
        try
        {
            UnityEngine.Debug.Log("Environment.OSVersion.Version.Major:" + Environment.OSVersion.Version.Major);
            var isWin7 = Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;
            var isWin8OrWin10 =
                Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2;
            var isWin10 = Environment.OSVersion.Version.Major == 10 && Environment.OSVersion.Version.Minor == 0;


            if (isWin7)
            {
                //win7
                UnityEngine.Debug.Log("Window 7");
                ShowOnScreenKeyboard();
            }
            else if (isWin8OrWin10 || isWin10)
            {
                //win10 
                UnityEngine.Debug.Log("Window 10");
                //HideTouchKeyboard();
                ExternalCall("C:\\Program Files\\Common Files\\Microsoft Shared\\ink\\Tabtip.exe", null, false);
                //callTabtip();
                //ExternalCall("TABTIP", null, false);
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log(e.Message);
            // ignored
        }
    }


    //Hide the touch keyboard (tabtip.exe).
    public void HideTouchKeyboard()
    {
        uint WM_SYSCOMMAND = 274;
        int SC_CLOSE = 61536;
        IntPtr TabTipHandle = FindWindow("IPTip_Main_Window", null);
        PostMessage(TabTipHandle, WM_SYSCOMMAND, SC_CLOSE, 0);
    }

    //Show the on screen keyboard (osk.exe).
    public void ShowOnScreenKeyboard()
    {
        //ExternalCall("C:\\Windows\\system32\\osk.exe", null, false);

        if (_onScreenKeyboardProcess == null || _onScreenKeyboardProcess.HasExited)
            _onScreenKeyboardProcess = ExternalCall("OSK", null, false);
    }

    // Hide the on screen keyboard (osk.exe).
    public void HideOnScreenKeyboard()
    {
        if (_onScreenKeyboardProcess != null && !_onScreenKeyboardProcess.HasExited)
            _onScreenKeyboardProcess.Kill();
    }

    /// <summary>
    /// Set size and location of the OSK.exe keyboard, via registry changes.  Messy, but only known method.
    /// </summary>
    /// <param name='rect'>
    /// Rect.
    /// </param>
    public void RepositionOnScreenKeyboard(Rect rect)
    {
        ExternalCall("REG", @"ADD HKCU\Software\Microsoft\Osk /v WindowLeft /t REG_DWORD /d " + (int)rect.x + " /f", true);
        ExternalCall("REG", @"ADD HKCU\Software\Microsoft\Osk /v WindowTop /t REG_DWORD /d " + (int)rect.y + " /f", true);
        ExternalCall("REG", @"ADD HKCU\Software\Microsoft\Osk /v WindowWidth /t REG_DWORD /d " + (int)rect.width + " /f", true);
        ExternalCall("REG", @"ADD HKCU\Software\Microsoft\Osk /v WindowHeight /t REG_DWORD /d " + (int)rect.height + " /f", true);
    }


    void callTabtip()
    {
        HideTouchKeyboard();
        ExternalCall("C:\\Program Files\\Common Files\\Microsoft Shared\\ink\\Tabtip.exe", null, false);
    
    }


    void getKey()
    {
        string keyPath = @"HKEY_CURRENT_USER\Software\Microsoft\TabletTip\1.7";
        string valueName = "OptimizedKeyboardRelativeXPositionOnScreen";

        // Retrieve the registry value
        object value = Registry.GetValue(keyPath, valueName, null);
        Console.WriteLine(value);

        if (value != null)
        {
            // Cast or convert the value to the appropriate type
            string stringValue = value.ToString();
            // Use the value as needed
            Console.WriteLine(stringValue);
        }
        else
        {
            // Handle case where the value does not exist
            Console.WriteLine("Registry value does not exist.");
        }
    }

    private static Process ExternalCall(string filename, string arguments, bool hideWindow)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = filename;
        startInfo.Arguments = arguments;

        // if just command, we do not want to see the console displayed
        if (hideWindow)
        {
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
        }

        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        return process;
    }


    private void OnApplicationQuit()
    {
        this.HideOnScreenKeyboard();
        this.HideTouchKeyboard();
    }
}