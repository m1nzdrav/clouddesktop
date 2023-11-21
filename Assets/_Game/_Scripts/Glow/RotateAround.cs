using UnityEngine;
using System.Collections;
using DG.Tweening;

public class RotateAround : MonoBehaviour
{
    [SerializeField] Transform target; // the object to rotate around
    [SerializeField] int speed; // the speed of rotation
    [SerializeField] private float waitTime;
    private MeshCollider _collider;

    void Start()
    {
        
        if (target != null) return;
        target = this.gameObject.transform;
        // StartCoroutine(Rotate());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // RotateAround takes three arguments, first is the Vector to rotate around
        // second is a vector that axis to rotate around
        // third is the degrees to rotate, in this case the speed per second
    
        transform.RotateAround(target.transform.position, target.transform.up, speed * Time.deltaTime);
    }

    IEnumerator Rotate()
    {
        transform.DOLocalRotate(transform.localEulerAngles + Vector3.up, waitTime);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(Rotate());
    }
}