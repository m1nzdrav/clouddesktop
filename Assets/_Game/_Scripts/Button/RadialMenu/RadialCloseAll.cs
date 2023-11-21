using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RadialCloseAll : MonoBehaviour
{
    [SerializeField] private CanvasGroup radialCloseAll;
    [SerializeField] private float animationCanvas = .5f;

    public void CloseAll()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.StockOpenedPrefab.ForEach(x =>
        {
            x.GetComponent<FolderModel>().Icon.GetComponent<Button>().onClick.Invoke();
            x.GetComponent<FolderModel>().Icon.GetComponent<CanvasGroup>().interactable = true;
        });
    }

    public void ActivateCanvas()
    {
        if ((RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?
            .StockOpenedPrefab[
                (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock).StockOpenedPrefab.Count -
                1].Prefab != Prefab.MYTSubtitles)
        {
            radialCloseAll.DOFade(1, animationCanvas);
        }
    }

    public void DeActivateCanvas()
    {
        radialCloseAll.DOFade(0, animationCanvas);
    }
}