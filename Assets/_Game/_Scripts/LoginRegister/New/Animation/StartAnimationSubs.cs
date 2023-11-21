using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartAnimationSubs : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;

    void Start()
    {
        // StartCoroutine(AnimLogin());
    }

    IEnumerator AnimLogin()
    {
        var pos = Vector3.zero;
        pos.z = 100f;

        var time = 0f;
        var period = 0.5f;

        // точка появления
        while (time < period)
        {
            time += Time.deltaTime;
            var nTime = time / period;
            var lValue = Mathf.Lerp(0f, 0.1f, nTime);

            transform.localScale = new Vector3(0.05f, lValue, 1f);

            yield return null;
        }

        //yield return new WaitForSeconds(0.5f);

        time = 0f;
        period = 2f;

        // точка раскрытия
        while (time < period)
        {
            time += Time.deltaTime;
            var nTime = time / period;
            var lValue = Mathf.Lerp(0.05f, 1f, Easing(nTime));

            transform.localScale = new Vector3(lValue, 0.1f, 1f);

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        time = 0f;
        period = 1f;

        // точка возврата позиции
        while (time < period)
        {
            time += Time.deltaTime;
            var nTime = time / period;
            var lValueScale = Mathf.Lerp(0.1f, 1f, Easing(nTime));

            transform.localScale = new Vector2(1f, lValueScale);

            yield return null;
        }

        if (_audio != null)
        {
            _audio.Play();
        }
    }

    private float Easing(float x)
    {
        return x * x;
    }
}
