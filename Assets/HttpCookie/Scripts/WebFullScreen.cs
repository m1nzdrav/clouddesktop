using System.Runtime.InteropServices;
using UnityEngine;

namespace Project.HttpCookie.Scripts
{
    public class WebFullScreen : MonoBehaviour
    {
        [DllImport("__Internal")]
        private static extern string ScreenOpenerCaptureClick();

        public static void SetFullScreen()
        {
            ScreenOpenerCaptureClick();
        }
    }
}