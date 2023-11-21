using System;
using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;


public class CircleMenuButtonController : MonoBehaviour, ILock
{
    private int i = 0;
    [SerializeField] private bool isPressed = false;
    [SerializeField] private bool waitState;
    [SerializeField] private GameObject innerCircle;

    [SerializeField] private Image border, center;
    [SerializeField] private Transform _glow;
    [SerializeField] private Color _currentColor;

    [SerializeField] private bool needBorder = true;

    [SerializeField] private float animInner;


    public bool ClickButton
    {
        get => isPressed;
        set
        {
            if (value)
            {
                isPressed = value;
                ChangeImageForPressed();
                OnInnerCircle();
            }
            else
            {
                if (!WaitState)
                {
                    isPressed = value;
                    ChangeImageForOutPressed();
                    OffInnerCircle();
                }
            }
        }
    }

    public bool WaitState
    {
        get => waitState;
        set => waitState = value;
    }

    private void Awake()
    {
        if (!GetComponent<Button>().IsInteractable())
        {
            // ButtonDisabled();

            if (_glow != null)
            {
                _glow.localScale = Vector3.zero;
            }
        }
        else
        {
            if (needBorder)
            {
                EnterPointer();
                ExitPointer();
            }
        }


        if (WaitState)
        {
            ChangeImageForPressed();
        }

        // if (GetComponent<IShowHide>() == null)
        // {
        //     needBorder = false;
        //     try
        //     {
        //
        //         border.gameObject.SetActive(false);
        //     }
        //     catch (Exception e)
        //     {
        //         Debug.LogError(gameObject.name);
        //     }
        // }
    }


    [Button]
    public void ClickButtonMetod()
    {
        if (ClickButton)
        {
            ClickButton = false;
        }
        else
        {
            ClickButton = true;
        }
    }

    #region InnerCircle

    [Button]
    public void OnInnerCircle()
    {
        if (innerCircle != null)
        {
            StartCoroutine(OnInner());
        }
    }

    IEnumerator OnInner()
    {
        innerCircle.SetActive(true);
        yield return innerCircle.transform.DOScale(Vector3.one, animInner);
    }

    [Button]
    public void OffInnerCircle()
    {
        if (innerCircle != null && !isPressed)
        {
            StartCoroutine(OffInner());
        }
    }

    IEnumerator OffInner()
    {
        yield return innerCircle.transform.DOScale(new Vector3(1, 0, 1), animInner);
        innerCircle.SetActive(false);
    }

    #endregion

    #region PressedUnpressed

    [Button]
    public void ChangeImageForPressed()
    {
        border.gameObject.SetActive(false);

        // if (pressedButtonImage != null)
        // {
        //     gameObject.GetComponent<Image>().sprite = pressedButtonImage;
        //     _currentStateImage.currentImage = pressedButtonImage;
        //     GetComponent<Image>().color = _currentColor;
        // }
    }

    [Button]
    public void ChangeImageForOutPressed()
    {
        if (needBorder)
        {
            border.gameObject.SetActive(true);
            border.color = _currentColor;
        }

        // if (pressedButtonImage != null)
        // {
        //     gameObject.GetComponent<Image>().sprite = outButtonImage;
        //     GetComponent<Image>().color = _currentColor;
        //     _currentStateImage.currentImage = outButtonImage;
        // }
        //
        // if (border != null)
        // {
        //     border.SetActive(true);
        //     _currentStateImage.isBorder = true;
        // }
    }

    #endregion

    #region EnterExitPoint

    [Button]
    public void EnterPointer()
    {
        if (GetComponent<Button>().IsInteractable())
        {
            if (needBorder && border!= null)
            {
                border.gameObject.SetActive(true);
                _currentColor = border.color;
            }
            // border.color = Config.COLOR_POINTER_ENTER;

            if (_glow != null)
            {
                _glow.localScale = Vector3.one;
            }
        }
    }

    [Button]
    public void ExitPointer()
    {
        if (GetComponent<Button>().IsInteractable())
            if (ClickButton)
            {
                // border.color = _currentColor;
                border.gameObject.SetActive(false);

                // gameObject.GetComponent<Image>().sprite = _currentStateImage.currentImage;
                // border.SetActive(_currentStateImage.isBorder);
                // GetComponent<Image>().color = _currentColor;
            }
            else
            {
                // Debug.LogError(_currentColor);
                // border.color = _currentColor;
                // gameObject.GetComponent<Image>().sprite = _currentStateImage.currentImage;
                // border.SetActive(_currentStateImage.isBorder);
                // GetComponent<Image>().color = _currentColor;
            }
    }

    #endregion

    #region EnabledDisabled

    [Button]
    public void ButtonEnabled()
    {
        if (needBorder)
        {
            border.gameObject.SetActive(true);
        }

        gameObject.GetComponent<Button>().interactable = true;

        innerCircle.SetActive(false);
    }

    [Button]
    public void ButtonDisabled()
    {
        Debug.LogError("disabled");
        border.gameObject.SetActive(false);
        gameObject.GetComponent<Button>().interactable = false;

        innerCircle.SetActive(true);
    }

    #endregion

    #region EnabledDisabledBorder

    [Button]
    public void BorderEnabled()
    {
        needBorder = true;

        border.gameObject.SetActive(true);
    }

    [Button]
    public void BorderDisabled()
    {
        needBorder = false;
        border.gameObject.SetActive(false);
        // gameObject.GetComponent<Button>().interactable = false;
        //
        // innerCircle.SetActive(true);
    }

    #endregion

    #region Locker

    public bool IsLock { get; set; }

    public void Lock()
    {
        ButtonDisabled();
        IsLock = true;
    }

    public void Unlock()
    {
        ButtonEnabled();
        IsLock = false;
    }

    #endregion
}