using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StringPrint : MonoBehaviour
{
    [SerializeField] 
    private Text _text;

    [SerializeField]
    private float _delay;

    void Start()
    {
        StartCoroutine(Print());
    }

    IEnumerator Print()
    {
        var len = _text.text.Length;
        var prewText = _text.text;

        for (int i = 0; i <= len; i++)
        {
            _text.text = prewText.Substring(0, i);

            yield return new WaitForSeconds(_delay);
        }
    }
}
