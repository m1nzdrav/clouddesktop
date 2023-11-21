using System.Collections;
using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RadialFullScreen : MonoBehaviour
{
    [SerializeField] private Image currentImage;
    [SerializeField] private Sprite pauseImage, playImage;
    [SerializeField] private bool isFull;
    [SerializeField] private StartLoginScene _startLoginScene;
    [SerializeField] private UnityEvent action;

    [SerializeField, Header("For Scale")] private Vector3 defaultScale;
    [SerializeField] Vector3 playScale;

    public void ResetLoginScene()
    {
        _startLoginScene = null;
    }

    public void ChangeFullSize()
    {
        ActionFull();

        var currentWindow =
            (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.StockOpenedPrefab[
                (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock).StockOpenedPrefab.Count - 1];

        switch (currentWindow.Prefab)
        {
            case Prefab.MYTSubtitles:
            case Prefab.Button:
            // {
            // RegisterSingleton._instance.ApplicationManager.FullScreen();
            // _startLoginScene?.StartLogin();
            // break;
            // }
            case Prefab.MYTPromo1:

            default:
            {
                (RegisterSingleton._instance.GetSingleton(typeof(ApplicationManager)) as ApplicationManager)?.FullScreen(!Screen.fullScreen);
                StartCoroutine(WaitStart());
                break;
            }
        }
    }

    private void ActionFull()
    {
        isFull = !isFull;
        ChangeScale();
        ChangeSprite();
        action?.Invoke();
    }

    private void ChangeSprite()
    {
        currentImage.sprite = isFull ? pauseImage : playImage;
    }

    private void ChangeScale()
    {
        transform.localScale = isFull ? playScale : defaultScale;
    }

    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(.5f);
        _startLoginScene?.StartLogin();
    }
}