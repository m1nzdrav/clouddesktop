using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Core.PathCore;
using DG.Tweening.Plugins.Options;
using Project._Game._Scripts.SameSequence;
using UnityEngine;

public class ActiveFolderAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _animGlow;
    [SerializeField] private RectTransform _rotateGlow;

    private TweenerCore<Vector3, Path, PathOptions> _anim;
    private SequenceController _sequenceController;
    private Vector3 _startPos;
    [SerializeField] private bool isActiv;

    public bool Active
    {
        get => isActiv;
        set
        {
            isActiv = value;
            foreach (var VARIABLE in showOnPanels)
            {
                // if (VARIABLE != null)
                VARIABLE.ChangeValueBlocker(typeof(ActiveFolderAnimation).ToString(), value);
            }
        }
    }


    [Header("Folder")] [SerializeField] private bool _isFolder;

    // [SerializeField] private Transform _topPanel;
    [SerializeField] private Transform _buttonPanel;

    [SerializeField] private Transform _name;
    // [SerializeField] private Transform _footer;
    // [SerializeField] private CanvasGroup _canvasGroup;

    [Header("EnterPointer")] [SerializeField]
    private ShowOn[] showOnPanels;

    public void StartAnimation(float delay = 0f)
    {
        Active = true;

        if (_isFolder)
        {
            // _topPanel.localScale = Vector3.one;
            _buttonPanel.localScale = Vector3.one;
            _name.localScale = Vector3.one;
            // _footer.localScale = Vector3.one;
            // _canvasGroup.alpha = 1f;
        }

        StartCoroutine(ShowAnimation(delay));
    }

    IEnumerator ShowAnimation(float delay = 0f)
    {
        // if (_animGlow.gameObject.activeSelf) yield break;

        yield return new WaitForSeconds(delay);

        _animGlow.gameObject.SetActive(true);

        Rect rect = _rotateGlow.rect;
        var x = -rect.width / 2;
        var y = rect.height / 2;

        Transform glowTransform = _animGlow.transform;
        _startPos = glowTransform.localPosition;

        var path = new Vector3[5]
        {
            new Vector3(x, y),
            new Vector3(-x, y),
            new Vector3(-x, -y),
            new Vector3(x, -y),
            new Vector3(x, _startPos.y)
        };


        _anim = _animGlow.transform.DOLocalPath(path, 15f);
        _anim.Pause();
        // _anim.SetLoops(-1);
        _anim.OnComplete(() => { StartCoroutine(ShowAnimation(delay)); });
        _anim.Play();
    }

    public void StopAnimation()
    {
        if (!_animGlow.gameObject.activeSelf) return;

        if (_anim != null)
        {
            Active = false;

            if (_isFolder)
            {
                // _footer.localScale = Vector3.zero;
                // _canvasGroup.alpha = 0f;
            }

            _animGlow.gameObject.SetActive(false);
            _anim.Pause();
            _anim = null;
            _animGlow.transform.localPosition = _startPos;
        }
    }
}