using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class RadialFinger : MonoBehaviour
{
    [SerializeField] private CanvasGroup radialFinger;
    [SerializeField] private float animationCanvas = .5f;

    public void OpenFinger()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?
            .StockOpenedPrefab[
                (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock).StockOpenedPrefab.Count -
                1]
            .GetComponent<FolderModel>().TopPanelSetParametrs
            .BtnAction.GetComponent<Button>().onClick.Invoke();
    }

    public void ActivateCanvas()
    {
        if ((RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?
            .StockOpenedPrefab[
                (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock).StockOpenedPrefab.Count -
                1].Prefab !=
            Prefab.MYTSubtitles)
        {
            radialFinger.DOFade(1, animationCanvas);
        }
    }

    public void DeActivateCanvas()
    {
        radialFinger.DOFade(0, animationCanvas);
    }
}