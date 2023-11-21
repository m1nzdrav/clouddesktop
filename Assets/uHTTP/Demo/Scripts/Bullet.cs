using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    public float timeToLive = 1;

    private bool isExploding = false;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void FixedUpdate()
    {
        if(isExploding) {
            return;
        }
        foreach(Collider collider in Physics.OverlapSphere(transform.position, 0.25f)){
            Player player = collider.GetComponent<Player>();
            if(player != null) {
                player.Damage();
            }
            StartCoroutine(OnImpact());
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }

    private IEnumerator OnImpact() {
        isExploding = true;
        speed = 0;
        GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(explosion.GetComponent<Collider>());
        explosion.name = "Explosion";
        explosion.transform.localScale = Vector3.one * 0;
        explosion.GetComponent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.red, 0.35f);
        explosion.transform.localPosition = transform.position;
        float explosionStartTime = Time.time;
        while(Time.time - explosionStartTime < 0.25){
            yield return new WaitForEndOfFrame();
            explosion.transform.localScale = Vector3.one * (Time.time - explosionStartTime) * 10;
        }
        Destroy(explosion);
        Destroy(gameObject);
    }
}
