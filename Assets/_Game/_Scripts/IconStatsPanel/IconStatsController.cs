using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconStatsController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform _panelStatistic;

    [SerializeField] private IconPanel _iconPanel;

    private bool _showAllStats;
    private bool _activeAnim;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (_showAllStats)
        {
            HideStats();
            return;
        }

        ShowAllStats();
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (!_activeAnim)
        {
            StopAllCoroutines();
        }
    }

    //public void PointerEnterStats()
    //{
    //    StartShowAllStats();
    //}

    //public void PointerExit()
    //{
    //    if (!_showAllStats)
    //    {
    //        if (_showAllStatsCoroutine != null) StopCoroutine(_showAllStatsCoroutine);
    //        if (_closeSmallStats && _hideAllStatsCoroutine != null) StopCoroutine(_hideAllStatsCoroutine);
    //    }
    //}

    private void ShowAllStats()
    {
        var scale = _panelStatistic.localScale;
        scale.x = 1f;

        StartCoroutine(StatsAnimation(_panelStatistic.localScale, scale, 1f, true));
    }

    private void HideStats()
    {
        var scale = _panelStatistic.localScale;
        scale.x = 0f;

        StartCoroutine(StatsAnimation(_panelStatistic.localScale, scale, 1f, false));
    }

    IEnumerator StatsAnimation(Vector3 scale, Vector3 scaleTo, float delay, bool active)
    {
        yield return new WaitForSeconds(delay);

        _activeAnim = true;

        _showAllStats = true;

        var time = 0f;
        const float period = 0.2f;

        while (time < period)
        {
            time += Time.deltaTime;

            var nTime = time / period;

            var lVector = Vector3.Lerp(scale, scaleTo, nTime);
            _panelStatistic.localScale = lVector;

            yield return null;
        }

        _activeAnim = false;

        if (active) yield break;

        _showAllStats = false;
        _iconPanel.ShowSmallStats = false;
    }
}
