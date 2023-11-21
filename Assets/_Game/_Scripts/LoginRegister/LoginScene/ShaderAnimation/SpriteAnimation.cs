using System.Collections;
using _Game._Scripts;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


public class SpriteAnimation : GraphicMaterial
{
    [SerializeField] private float dissolveAmount;
    [SerializeField] private bool flag;

    public bool Flag => flag;

    [SerializeField] private bool isDissolving;
    private Check _checkIn;
    private Check _checkOut;

    public Check CheckIn => _checkIn;

    public Check CheckOut => _checkOut;

    public bool IsDissolving
    {
        get => isDissolving;
        set
        {
            isDissolving = value;
            if (value)
            {
                OffGlow();
                StartCoroutine(ChangeDissolve(_checkOut));
            }
            else
            {
                OpenGlow();
                StartCoroutine(ChangeDissolve(_checkIn));
            }
        }
    }

    [SerializeField, Header("Setting")] private Image _spriteRenderer;
    [SerializeField] private float dissolveSpeed;

    public float DissolveSpeed
    {
        get => dissolveSpeed;
        set => dissolveSpeed = value;
    }

    [SerializeField, ColorUsageAttribute(true, true)]
    private Color outColor;

    [SerializeField, ColorUsageAttribute(true, true)]
    private Color inColor;

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material _material;

    [Header("GlowSetting")] [SerializeField]
    private Image glow;

    [SerializeField] private Color glowColorOn;
    [SerializeField] private Color glowColorOff;
    [SerializeField] private float alphaEndGlow = 0;
    [SerializeField] private float waitGlow = 2.3f;

    private void Start()
    {
        _material = GetMaterial();
        _checkOut = DissolveOut;
        _checkIn = DissolveIn;
        IsDissolving = isDissolving;
    }

    private Material GetMaterial()
    {
        return _spriteRenderer.material;
    }

    private void ChangeMaterial(Material material)
    {
        _spriteRenderer.material = material;
    }

    private bool DissolveOut()
    {
        _material.SetColor(Config.KEY_DISSOLVE_COLOR, outColor);
        if (dissolveAmount > -0.1)
        {
            dissolveAmount -= Time.deltaTime * dissolveSpeed;
            return true;
        }

        flag = false;
        return false;
    }

    private bool DissolveIn()
    {
        _material.SetColor(Config.KEY_DISSOLVE_COLOR, inColor);

        if (!(dissolveAmount < 1))
        {
            flag = true;
            return false;
        }

        dissolveAmount += Time.deltaTime * dissolveSpeed;

        return true;
    }

    public void Test()
    {
        IsDissolving = !isDissolving;
    }

    public override void Dissolve()
    {
        if (isDissolving)
        {
            DissolveOut();
        }

        if (!isDissolving)
        {
            DissolveIn();
        }

        _material.SetFloat(Config.KEY_DISSOLVE_AMOUNT, dissolveAmount);
    }

    [Button]
    public override void ChangeStateDissolve()
    {
        // if (isDissolving)
        // {
        ChangeMaterial(_material);
        _spriteRenderer.DOFade(1, 0);
        // смена на диссолв спрайт
        // }


        IsDissolving = !IsDissolving;
    }

    [Button]
    public void FastChangeState()
    {
        ChangeMaterial(_material);
        _spriteRenderer.DOFade(1, 0);
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

        ChangeMaterial(defaultMaterial);
        if (isDissolving)
        {
            _spriteRenderer.DOFade(0, 0);
        }
    }

    private void OpenGlow()
    {
        glow.DOFade(1, .5f);
        glow?.DOColor(glowColorOn, waitGlow).OnComplete((() => glow.DOFade(0, .5f)));
    }

    private void OffGlow()
    {
        glow?.DOColor(glowColorOff, waitGlow).OnComplete((() => glow.DOFade(0, .5f)));
    }

    private IEnumerator ChangeDissolve(Check test)
    {
        yield return null;

        if (test.Invoke())
        {
            StartCoroutine(ChangeDissolve(test));
            _material.SetFloat(Config.KEY_DISSOLVE_AMOUNT, dissolveAmount);
        }
        else
        {
            ChangeMaterial(defaultMaterial);
            if (isDissolving)
            {
                _spriteRenderer.DOFade(0, 0);
            }
        }
    }

    public delegate bool Check();
}