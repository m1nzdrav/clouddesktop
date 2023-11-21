
using System.Runtime.InteropServices;

public static class ClipboardTest 
{
    [DllImport("__Internal")]
    private static extern string PastClipboard();
    
    /// <summary>
    /// получение из буфера
    /// </summary>
    /// <returns></returns>
    public static string GetClipboard()
    {
        return PastClipboard();
    }
}