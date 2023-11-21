using System.Collections;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;


public class AnimationLoading : MonoBehaviour
{
    [SerializeField] private SpriteAnimation targetSpriteAnimation;
    [SerializeField] private Transform targetTransform;

    [SerializeField, FoldoutGroup("Callback")]
    private UnityEvent callbackAfterRotate;

    [SerializeField, FoldoutGroup("Callback")]
    private UnityEvent callbackAfterLoad;

    [SerializeField] private float speedDissolve;
    [SerializeField] private float speedRotate;
    [SerializeField] private Vector3 vectorToRotate;

    private void StartAnimation(SpriteAnimation.Check check)
    {
        targetSpriteAnimation.DissolveSpeed = speedDissolve;
        StartCoroutine(EngineAnimation(check));
    }

    public void WaitScene()
    {
        StartAnimation((() =>
            (RegisterSingleton._instance.GetSingleton(typeof(LanguageBundleSaver)) as LanguageBundleSaver).SceneAb !=
            null));
    }

    IEnumerator EngineAnimation(SpriteAnimation.Check check)
    {
        targetSpriteAnimation.ChangeStateDissolve();

        yield return new WaitWhile((() => targetSpriteAnimation.Flag));

        targetSpriteAnimation.ChangeStateDissolve();

        yield return new WaitWhile((() => !targetSpriteAnimation.Flag));

        targetTransform.DORotate(
            vectorToRotate, speedRotate, RotateMode.FastBeyond360);

        yield return new WaitForSeconds(speedRotate);


        if (check.Invoke())
            callbackAfterLoad.Invoke();
        else
        {
            callbackAfterRotate?.Invoke();
            StartCoroutine(EngineAnimation(check));
        }
    }
}