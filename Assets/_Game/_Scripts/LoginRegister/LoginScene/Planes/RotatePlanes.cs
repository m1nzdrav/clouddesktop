using System;
using System.Collections;
using System.Collections.Generic;
using _Game._Scripts.FolderScript.Name;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DynamicPanels;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class RotatePlanes : MonoBehaviour
{
    [SerializeField] private List<SettingsRotate> _stateRotates;
    [SerializeField] private SettingsRotate currentSettingsRotate;

    [SerializeField] private RotateVertical home, enter;
    public SettingsRotate CurrentSettingsRotate => currentSettingsRotate;

    [SerializeField] private GameObject _subs;

    [SerializeField] private Transform _planeSubs;
    [SerializeField] private Image _planeGrid;
    [SerializeField] private float _animationDuration;

    [SerializeField] public List<EventName> _folders;
    [SerializeField] Vector3 rotateVector = new Vector3(-90f, 0f, 0f);
    [SerializeField] Vector3 endPos = new Vector3(0f, -1523f, -2053);

    [SerializeField] private bool _rotate;

    public bool Rotate => _rotate;

    private Vector3 _savePos;
    private Quaternion _saveRotate;
    private Quaternion _saveGridRotate;

    private TweenerCore<Quaternion, Quaternion, NoOptions> _animationRotateQ;
    TweenerCore<Quaternion, Vector3, QuaternionOptions> _animationRotate;
    private TweenerCore<Vector3, Vector3, VectorOptions> _animationMove;
    private TweenerCore<Color, Color, ColorOptions> _animationColor;

    [Header("Anim LockArea")] [SerializeField]
    private StartAnimation dropDown;

    [SerializeField] private StartAnimation lockLogin;
    [SerializeField] private ActivateCanvas loginActivateCanvas;
    [SerializeField] private ActivateCanvas dropDownActivateCanvas;

    [SerializeField] private RectTransform loginTransform;
    private Vector3 oldTransformLogin;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float waitFix = 2f;

    [Button]
    public void ButtonRotate()
    {
        SetRotate();
    }


    public void SetRotate(Action action = null)
    {
        if (_rotate) return;

        _rotate = true;

        if (_animationMove != null)
        {
            if (_animationMove.IsPlaying())
            {
                _animationMove.Pause();
                _animationMove = null;
            }
        }

        if (_animationRotateQ != null)
        {
            if (_animationRotateQ.IsPlaying())
            {
                _animationRotateQ.Pause();
                _animationRotateQ = null;
            }
        }

        if (_animationColor != null)
        {
            if (_animationColor.IsPlaying())
            {
                _animationColor.Pause();
                _animationColor = null;
            }
        }

        Vector3 forRotate = new Vector3(90 * currentSettingsRotate.vectorRotateVertical.x,
            _planeSubs.localRotation.eulerAngles.y, 90 * currentSettingsRotate.vectorRotateVertical.z);
        
        EnterState();
        _savePos = _planeGrid.transform.localPosition;
        _saveRotate = _planeSubs.rotation;
        _animationColor = _planeGrid.DOFade(0, _animationDuration + 4);
        _animationRotate = _planeSubs.DORotate(forRotate, _animationDuration).OnComplete(() =>
        {
            _planeSubs.rotation = Quaternion.Euler(forRotate);
        });
        
        //Включаем в StartAnimation

        GetComponent<CanvasGroup>().interactable = false;
        _animationMove = _planeGrid.transform.DOLocalMove(currentSettingsRotate.vectorPosition, _animationDuration)
            .OnComplete(() =>
            {
                StartCoroutine(FixRotateWithTime());
                // FixRotate();
                loginActivateCanvas.DeActivate();
                dropDownActivateCanvas.DeActivate();
                // currentSettingsRotate.transform.GetChild(0).Rotate(0,-5,0); 
                OnCompleteRotate(action);
                GetComponent<CanvasGroup>().interactable = true;
            });

        Invoke("Music", _animationDuration / 2);
    }

    private void FixRotate()
    {
        currentSettingsRotate.transform.GetChild(0).DORotate(new Vector3(0, 5), 0).OnComplete(() =>
            currentSettingsRotate.transform.GetChild(0).DORotate(new Vector3(0, 0), 0));
    }

    private IEnumerator FixRotateWithTime()
    {
        yield return new WaitForSeconds(waitFix);
        FixRotate();
    }

    private void Music()
    {
        (RegisterSingleton._instance.GetSingleton(typeof(MusicStock)) as MusicStock)?.PlayMusic();
    }

    public void ReturnRotate()
    {
        if (!_rotate) return;

        _rotate = false;

        if (_animationMove != null)
        {
            if (_animationMove.IsPlaying())
            {
                _animationMove.Pause();
                _animationMove = null;
            }
        }

        if (_animationRotate != null)
        {
            if (_animationRotate.IsPlaying())
            {
                _animationRotate.Pause();
                _animationRotate = null;
            }
        }

        if (_animationColor != null)
        {
            if (_animationColor.IsPlaying())
            {
                _animationColor.Pause();
                _animationColor = null;
            }
        }

        var count = _folders.Count;

        for (int i = 0; i < count; i++)
        {
            _folders[i].name_textField.gameObject.SetActive(false);
        }

        _animationColor = _planeGrid.DOFade(1, 0.1f);
        _subs.SetActive(true);
        HomeState();
        _animationRotateQ = _planeSubs.DORotateQuaternion(_saveRotate, _animationDuration);
        _animationMove = _planeGrid.transform.DOLocalMove(_savePos, _animationDuration).OnComplete((() =>
        {
            loginActivateCanvas.Activate();
            dropDownActivateCanvas.Activate();
            // AnimLockArea();
        }));
    }

    public void ChangeRotateVertical()
    {
        if (_rotate)
            ReturnRotate();
        else
            SetRotate();
    }

    private void EnterState()
    {
        home.OffGlow();
        enter.OnGlow();
    }

    private void HomeState()
    {
        home.OnGlow();
        enter.OffGlow();
    }

    private void OnCompleteRotate(Action action)
    {
        if (action != null)
        {
            action.Invoke();
        }

        _subs.SetActive(false);
    }

    private IEnumerator DisableSubs()
    {
        yield return new WaitForSeconds(_animationDuration);
    }

    [Button
    ]
    public void AnimLockArea()
    {
        dropDown.ReturnToOpen();
        lockLogin.ReturnToOpen(() => { lockLogin.ReturnLoginLeft(); });
    }

    [Button]
    public void SetOnCenter()
    {
        if (LoginAndLoadScene.IsLogin)
            return;

        dropDown.ReturnToClose(18);
        lockLogin.ReturnToClose(-18, lockLogin.SetLoginCenter);
    }


    public void RotateToFrontier(StateRotate state, Action action, bool needVerticalRotate = true)
    {
        if (_rotate)
            StartCoroutine(RotateAfterReturn(state, action, needVerticalRotate));
        else
            RotateSubs(state, action, needVerticalRotate);
    }

    public void RotateToFrontier(float angle, Action action)
    {
        if (_rotate)
            StartCoroutine(RotateAfterReturn(GetSettingsRotate(angle).StateRotate, action));
        else
            RotateSubs(GetSettingsRotate(angle).StateRotate, action);
    }

    IEnumerator RotateAfterReturn(StateRotate state, Action action, bool needVerticalRotate = true)
    {
        ReturnRotate();
        yield return new WaitForSeconds(_animationDuration);
        RotateSubs(state, action, needVerticalRotate);
    }

    private void RotateSubs(StateRotate state, Action action, bool needVerticalRotate = true)
    {
        if (state == StateRotate.Front)
            Invoke("Music", 1.2f);

        currentSettingsRotate = GetSettingsRotate(state);
        //todo закоментирован отступ к нулевым координатам при повороте между областями
        // _planeGrid.transform
        //     .DOLocalMove(new Vector3(0, currentSettingsRotate.vectorPositionPlace.y, 0), _animationDuration).OnComplete(
        //         () =>
        //         {
        _planeGrid.transform.DOLocalMove(currentSettingsRotate.vectorPositionPlace, _animationDuration);
        // });

        Vector3 vector = new Vector3(0, 0 - currentSettingsRotate.angleRotateHorizontal, 0);
        _planeSubs.DORotate(vector
                ,
                _animationDuration)
            .OnComplete(() =>
            {
                // _planeSubs.rotation = Quaternion.Euler(vector);
                if (needVerticalRotate)
                {
                    SetRotate(action);
                }
            })
            ;
    }

    private SettingsRotate GetSettingsRotate(StateRotate stateRotate)
    {
        return _stateRotates.Find(x => x.StateRotate == stateRotate);
    }

    private SettingsRotate GetSettingsRotate(float angle)
    {
        return _stateRotates.Find(x => x.angleRotateHorizontal == angle);
    }

    [Button]
    public void RotateHome()
    {
        RotateToFrontier(StateRotate.Front, null, false);
    }
}