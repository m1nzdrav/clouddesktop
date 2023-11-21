using UnityEngine;

public class ActivityFinder : MonoBehaviour, ISingleton
{
    [SerializeField] private AbstractPromo currentPromoActivity;

    public AbstractPromo CurrentPromoActivity => currentPromoActivity;

    

    public string NameComponent
    {
        get => name;
    }

    public void CheckRegister()
    {
    }

    private void Awake()
    {
        if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
        
        transform.SetParent(RegisterSingleton._instance.transform);
        CheckRegister();
    }
    public void SetPromoActivity(PromoActivity target)
    {
        currentPromoActivity = target;
    }
}