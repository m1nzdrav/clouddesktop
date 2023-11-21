using UnityEngine;


    public class ReloadDropDown : MonoBehaviour
    {
        public void Reload()
        {
            (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)?.ReloadScene();
        }
        
    }