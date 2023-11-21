using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CursorAnimation : MonoBehaviour
{
    [SerializeField] private Image _circle;
    [SerializeField] private Image _cursor;

    [SerializeField] private float _startDelay;

    [SerializeField] private Lines _lines;

    public int CircleCount;
    public float Delay;


    void Start()
    {
        StartCoroutine(SpawnCircleCursor());
    }

    IEnumerator SpawnCircleCursor()
    {
        _circle.color = new Color(0f, 1f, 0.2f);

        yield return new WaitForSeconds(_startDelay);

        for (int i = 0; i < CircleCount; i++)
        { 
            var circle = Instantiate(_circle, transform, false);
            circle.transform.localPosition = Vector3.zero;
            //circle.GetComponent<Animation>().Play("ShowCursor");
        }

        yield return new WaitForSeconds(Delay);

        _cursor.enabled = true;
    }

    public IEnumerator SpawnCircleMouse()
    {
        _circle.color = new Color(1f, 1f, 1f);

        var lineDelay = _lines.AnimationDurationLeft;
        yield return new WaitForSeconds(lineDelay);

        for (int i = 0; i < CircleCount; i++)
        {
            var circle = Instantiate(_circle, transform.parent);
            //circle.transform.position = Vector3.zero;
            circle.GetComponent<Animation>().Play("ShowMouse");
        }

        yield return 0;
    }
}
