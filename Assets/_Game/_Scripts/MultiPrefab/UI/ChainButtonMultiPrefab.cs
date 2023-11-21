using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(StartButton))]
public class ChainButtonMultiPrefab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private MainStart _main;
    [SerializeField] private StartButton startButton;
    

    private bool IsStart => startButton.IsStart;
    // private bool IsLock => startButton.IsLock;
[Button]
    public void SendChangeToMain()
    {
        _main.SetState(!IsStart);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _main.EnterTrigger();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _main.ExitTrigger();
    }

    
}