using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickMask : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0f, 1f)] //1
    public float AlphaLevel = 1f; //2
    private Image bt; //3
   
    void Start()
    {
        bt = gameObject.GetComponent<Image>();
        bt.alphaHitTestMinimumThreshold = AlphaLevel; //4
    }
}
