using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCode : MonoBehaviour
{
    [field: SerializeField,
            Range(0f, 1f)] private float timeCode;

    public float TimeCode1
    {
        get => timeCode;
        set => timeCode = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
