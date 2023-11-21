using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DissolveController : MonoBehaviour
{
    [SerializeField] private Image sprite;
    [SerializeField] private Image glow;
    [SerializeField] private Color glowColorOn;
    [SerializeField] private Color glowColorOff;
    [SerializeField] private float waitGlow = .5f;

    public float dissolveAmount;
    public float dissolveSpeed;
    public bool isDissolving;


    public bool IsDissolving
    {
        get => isDissolving;
        set
        {
            isDissolving = value;
            if (value)
            {
                OffGlow();
            }
            else
                OpenGlow();
        }
    }

    [ColorUsageAttribute(true, true)] public Color outColor;

    [ColorUsageAttribute(true, true)] public Color inColor;

    private Material mat;


    // Start is called before the first frame update

    void Start()
    {
        try
        {
            mat = GetComponent<SpriteRenderer>().material;
        }
        catch (Exception e)
        {
            mat = sprite.material;
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            IsDissolving = true;

        if (Input.GetKeyDown(KeyCode.S))
            IsDissolving = false;

        if (isDissolving)
        {
            DissolveOut(dissolveSpeed, outColor);
        }

        if (!isDissolving)
        {
            DissolveIn(dissolveSpeed, inColor);
        }

        mat.SetFloat("_DissolveAmount", dissolveAmount);
    }


    public void DissolveOut(float speed, Color color)
    {
        mat.SetColor("_DissolveColor", color);
        if (dissolveAmount > -0.1)
            dissolveAmount -= Time.deltaTime * speed;
    }

    public void DissolveIn(float speed, Color color)
    {
        mat.SetColor("_DissolveColor", color);
        if (dissolveAmount < 1)
            dissolveAmount += Time.deltaTime * speed;
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
}