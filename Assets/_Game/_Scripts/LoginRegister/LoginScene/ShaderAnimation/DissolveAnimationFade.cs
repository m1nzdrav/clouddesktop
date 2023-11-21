using System.Collections;
using _Game._Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

public class DissolveAnimationFade : MonoBehaviour
{
    [SerializeField] private float dissolveAmount;
    [SerializeField] private bool isDissolving;
    private Check _checkIn;
    private Check _checkOut;


    public bool IsDissolving
    {
        get => isDissolving;
        set
        {
            isDissolving = value;

            if (value)
                StartCoroutine(ChangeDissolve(_checkOut));
            else
                StartCoroutine(ChangeDissolve(_checkIn));
        }
    }

    [SerializeField] private Renderer test;

    [SerializeField] private float dissolveSpeed;

    [SerializeField, ColorUsageAttribute(true, true)]
    private Color outColor;

    [SerializeField, ColorUsageAttribute(true, true)]
    private Color inColor;

    private Material _material;


    private void Start()
    {
        _material = GetMaterial();
        _checkOut = DissolveOut;
        _checkIn = DissolveIn;
        IsDissolving = isDissolving;
    }

    private Material GetMaterial()
    {
        return test.material;
    }


    private bool DissolveOut()
    {
        _material.SetColor(Config.KEY_DISSOLVE_COLOR, outColor);

        if (dissolveAmount > -0.1)
        {
            dissolveAmount -= Time.deltaTime * dissolveSpeed;
            return true;
        }

        return false;
    }

    private bool DissolveIn()
    {
        _material.SetColor(Config.KEY_DISSOLVE_COLOR, inColor);

        if (!(dissolveAmount < 1)) return false;

        dissolveAmount += Time.deltaTime * dissolveSpeed;

        return true;
    }

    [Button]
    public void Test()
    {
        IsDissolving = !isDissolving;
    }

    [Button]
    public void ChangeStateDissolve()
    {
        // if (isDissolving)
        // {

        // смена на диссолв спрайт
        // }


        IsDissolving = !IsDissolving;
    }

    [Button]
    public void ResetDissolve()
    {
        _material.SetFloat(Config.KEY_DISSOLVE_AMOUNT, 0);
        dissolveAmount = 0;
        isDissolving = true;
    }

    private IEnumerator ChangeDissolve(Check test)
    {
        yield return null;

        if (!test.Invoke()) yield break;

        StartCoroutine(ChangeDissolve(test));
        _material.SetFloat(Config.KEY_DISSOLVE_AMOUNT, dissolveAmount);
    }
    [Button]
    public void FastChangeState()
    {
        if (isDissolving)
        {
            isDissolving = false;
            _material.SetColor(Config.KEY_DISSOLVE_COLOR, outColor);
            _material.SetFloat(Config.KEY_DISSOLVE_AMOUNT, 0);
        }
        else
        {
            isDissolving = true;
            _material.SetColor(Config.KEY_DISSOLVE_COLOR, inColor);
            _material.SetFloat(Config.KEY_DISSOLVE_AMOUNT, 1);
        }

       
    }
    delegate bool Check();
}