using UnityEngine;

namespace _Game._Scripts.PromoActivity
{
    public class LocalChangeBreadcrumbs : MonoBehaviour
    {
        public void ChangeBread(int number)
        {
            (RegisterSingleton._instance.GetSingleton(typeof(BreadcrumbsOnLoad)) as BreadcrumbsOnLoad)?.SetBreadValue(number);
            
#if  UNITY_WEBGL && !UNITY_EDITOR
            (RegisterSingleton._instance.GetSingleton(typeof(User)) as User)?.UpdateUserData(bread: number.ToString());
#endif
        }
    }
}