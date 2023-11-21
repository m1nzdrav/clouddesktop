using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetSettings : MonoBehaviour
{
    private static GetSettings _instance;

    public static GetSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GetSettings>();
            }

            return _instance;
        }
    }

    public VerticalLayoutGroup VerticalLayoutGroup;
    public RectTransform RectTransform;
    public ScrollRect ScrollRect;
}
