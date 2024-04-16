using System;
using System.Runtime.InteropServices;

public class ChineseConvertTool
{
    private const int LOCALE_SYSTEM_DEFAULT = 0x0800;
    private const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;
    private const int LCMAP_TRADITIONAL_CHINESE = 0x04000000;

    [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern int LCMapString(int Locale, int dwMapFlags, string lpSrcStr, int cchSrc, [Out] string lpDestStr, int cchDest);
    /// <summary>
    /// 把字符串??到繁体中文
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    static public string ToTraditional(string str)
    {
        String target = new String(' ', str.Length);
        int ret = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_TRADITIONAL_CHINESE, str, str.Length, target, str.Length);
        return target;
    }
    /// <summary>
    /// 把字符串??到?体中文
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    static public string ToSimplified(string str)
    {
        String target = new String(' ', str.Length);
        int ret = LCMapString(LOCALE_SYSTEM_DEFAULT, LCMAP_SIMPLIFIED_CHINESE, str, str.Length, target, str.Length);
        return target;
    }
}
