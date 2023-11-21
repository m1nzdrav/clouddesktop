using System.Collections;
using UnityEngine;

public class ShowElements : MonoBehaviour
{
    [SerializeField] private Transform _stats;
    [SerializeField] private Transform _number;

    private Vector3 _hideStatsVector = new Vector3(0f, 1f, 1f);
    private Vector3 _hideNumberVector = new Vector3(0f, 0f, 0f);
    private Vector3 _showVector = Vector3.one;

    private void Awake()
    {
        transform.parent.localPosition = Vector3.zero;
    }

    public void StartShowStatsAnimation()
    {
        StopAllCoroutines();

        StartCoroutine(StatsAnimation(_showVector, 0f, _stats));
        StartCoroutine(StatsAnimation(_showVector, 0f, _number));
    }

    public void StartHideStatsAnimation()
    {
        StartCoroutine(StatsAnimation(_hideStatsVector, 1f, _stats));
        StartCoroutine(StatsAnimation(_hideNumberVector, 1f, _number));
    }

    IEnumerator StatsAnimation(Vector3 endScale, float delay, Transform obj)
    {
        yield return new WaitForSeconds(delay);

        var time = 0f;
        const float period = 0.5f;
        var startScale = obj.localScale;

        while (time < period)
        {
            time += Time.deltaTime;

            var lTime = time / period;
            var lVector = Vector3.Lerp(startScale, endScale, lTime);
            obj.localScale = lVector;

            yield return null;
        }
    }
}
