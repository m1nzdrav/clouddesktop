 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SizeController : MonoBehaviour
{
    [SerializeField] RectTransform PatternObject;
    [SerializeField] float sizeDelta = 10;

    LayoutElement element;
    
    // Start is called before the first frame update
    void Start()
    {
        element = this.GetComponent<LayoutElement>();
        //element.minWidth = PatternObject.rect.width - sizeDelta;
    }

    // Update is called once per frame
    // void Update()
    // {
    //     element.minWidth = PatternObject.rect.width - sizeDelta;
    // }
}
