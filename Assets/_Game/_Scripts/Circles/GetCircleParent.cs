using UnityEngine;

public class GetCircleParent : MonoBehaviour
{
    private static GetCircleParent _instance;

    public static GetCircleParent Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GetCircleParent>();
            }

            return _instance;
        }
    }

    [SerializeField] private Transform _circleParent;

    public Transform GetParent()
    {
        return _circleParent;
    }
}
