using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class RotateCircleIndicator : MonoBehaviour
{
    [field: SerializeField,
            Required]
    private GameObject indicator;

    [field: SerializeField] private bool canActivate = true;

    [field: SerializeField,
            Range(0f, 0.01f)]
    private float acceptableDeviation = 0.005f;

    [field: SerializeField] private float indicatorRotationDuration = 0.5f;

    [field: SerializeField,
            Range(0f, 360f)]
    private float indicatorRotationZ = 10f;

    [field: SerializeField,
            Range(0f, 1f)]
    private float activationPosition;

    [field: SerializeField,
            Required]
    private CircleMenuController circleController;


    private void FixedUpdate()
    {
        if (canActivate)
            ActivationCheck();
    }

    public float AcceptableDeviation
    {
        get => acceptableDeviation;
        set => acceptableDeviation = value;
    }

    public float ActivationPosition
    {
        get => activationPosition;
        set => activationPosition = value;
    }

    // Update is called once per frame
    [Button]
    public void RotateIndicator()
    {
        indicator.transform.DORotate(new Vector3(0f, 0f, indicatorRotationZ), indicatorRotationDuration);
        Debug.LogError("Rotate indicator");
    }

    private void ActivationCheck()
    {
        if (Mathf.Abs(circleController.RadialSlider.SliderValueRaw - activationPosition) <= acceptableDeviation)
        {
            canActivate = false;
//            Debug.LogError("повернул на "+ (indicatorRotationZ));
            StartCoroutine(ResetActivation());
            RotateIndicator();
        }
    }

    IEnumerator ResetActivation()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(acceptableDeviation * 2);
        Debug.LogError("Change CanActivate");
        canActivate = true;
    }
}