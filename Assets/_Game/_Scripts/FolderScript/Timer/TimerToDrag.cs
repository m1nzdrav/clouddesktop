using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerToDrag : MonoBehaviour
{
    public static TimerToDrag instance;


    [Header("GameObject component links")] [SerializeField]
    
    private Image background;

    [SerializeField] private Text text;

    [Header("Current value")] [SerializeField]
    private float timer;

    public float Timer
    {
        get => timer;
        set
        {
            timer = value;
            text.text = timer.ToString("f2");
        }
    }


    public void SetValue(float timer,Color newColor)
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Уже создан таймер");
            Destroy(gameObject);
        }

        // text.color = newColor;
        StartCoroutine(StartTimer(timer));
    }

    public IEnumerator StartTimer(float timer)
    {
//        Debug.LogError(timer);
        if (timer <= 0)
        {
            DeleteTimer();
            yield break;
        }

        Timer = timer;
        yield return new WaitForSeconds(0.1f);
        timer -= 0.1f;
        StartCoroutine(StartTimer(timer));
    }

    public void DeleteTimer()
    {
//        Debug.LogError("Удалил");
        instance = null;
        Destroy(gameObject);
    }
}