using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformExtensionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.TryFindAmongParents(out Canvas canvas));
        Debug.Log(canvas.gameObject.name);
    }
}
