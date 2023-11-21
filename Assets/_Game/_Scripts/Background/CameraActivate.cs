
using UnityEngine;

namespace Project._Game._Scripts.Background
{
    public class CameraActivate : MonoBehaviour
    {
        private void Start()
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BackgroundSingleton)) as BackgroundSingleton)?.DeActivateBackCamera();
        }
    }
}