using System.Collections;
using UnityEngine;

public class IconPanel : MonoBehaviour
{
    [SerializeField] private Transform _panelStatistic;

    public bool ShowSmallStats;

    public void PointerEnterIcon()
    {
        if (!ShowSmallStats) ShowHideSomePanel(1.5f);
    }

    public void PointerExitIcon()
    {
        if (!ShowSmallStats) StopAllCoroutines();
    }

    public void ShowHideSomePanel(float delay)
    {
        StopAllCoroutines();

        var scale = _panelStatistic.localScale;
        scale.x = 0.3f;

        StartCoroutine(PanelAnim(_panelStatistic.localScale, scale, delay));
    }

    IEnumerator PanelAnim(Vector3 scale, Vector3 scaleTo, float delay)
    {
        yield return new WaitForSeconds(delay);

        ShowSmallStats = true;

        var time = 0f;
        var period = 0.2f;

        while (time < period)
        {
            time += Time.deltaTime;

            var nTime = time / period;

            var lVector = Vector3.Lerp(scale, scaleTo, nTime);
            _panelStatistic.localScale = lVector;

            yield return null;
        }
    }
}
