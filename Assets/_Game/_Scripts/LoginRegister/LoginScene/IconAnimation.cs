using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _rect;
    [SerializeField] private float _animationPeriod = 1f;

    [Button]
    public void StartAnimation()
    {
        //StartCoroutine(Animation());
        _rect.DORotate(new Vector3(0f, 180f, 90), _animationPeriod);
    }

    IEnumerator Animation()
    {
        var time = 0f;
        var rotation = _rect.rotation;
        var rotationTo = new Quaternion(rotation.x + 3.5f, rotation.y + 3.5f, rotation.z, rotation.w);

        while (time < _animationPeriod)
        {
            time += Time.deltaTime;

            var lTime = time / _animationPeriod;
            var lQuaternion = Quaternion.Lerp(rotation, rotationTo, lTime);

            _rect.rotation = lQuaternion;

            yield return null;
        }

    }
}
