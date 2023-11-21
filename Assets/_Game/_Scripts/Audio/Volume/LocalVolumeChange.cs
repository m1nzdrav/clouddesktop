using _Game._Scripts.Volume;
using UnityEngine;

public class LocalVolumeChange : MonoBehaviour
{
    
    public void VolumeDown()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(VolumeController)) as VolumeController)?.VolumeDown();
    }


    public void VolumeUp()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(VolumeController)) as VolumeController)?.VolumeUp();
    }
}