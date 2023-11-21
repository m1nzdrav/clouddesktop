using UnityEngine;


public class FrontierDrag : MonoBehaviour
{
    [SerializeField] private Renderer current;
    // Start is called before the first frame update
    void Start()
    {
        // current.bounds.Contains();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.LogError(current.bounds.SqrDistance(Input.mousePosition));
            Debug.LogError(current.bounds.Contains(Input.mousePosition));
        }
    }
}
