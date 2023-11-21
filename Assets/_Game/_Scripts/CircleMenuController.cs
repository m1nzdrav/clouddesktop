using DG.Tweening;
using Shapes2D;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;
using Michsky.UI.ModernUIPack;

public class CircleMenuController : MonoBehaviour
{
    [SerializeField]
    private float innerRadious = 65f;

    [SerializeField]
    private float outerRadious = 100f;

    [SerializeField]
    private int innerCircleCapacity = 10;

    private float innerCircleButtonWidthAngle { get { return 360f / (float)innerCircleCapacity; } }
    private float innerCircleButtonTotalButtonsWidthAngle { get { return innerCircleButtonWidthAngle * (float)buttons.Count; } }

    [SerializeField]
    private int outerCircleCapacity = 20;

    private float outerCircleButtonWidthAngle { get { return 360f / (float)outerCircleCapacity; } }

    private float outerCircleButtonTotalButtonsWidthAngle { get { return outerCircleButtonWidthAngle * (float)buttons.Count; } }

    [SerializeField]
    private float circleRotationDuration = 0.5f;

    [SerializeField]
    private Ease circleEaseType = Ease.OutElastic;

    [SerializeField]
    private float buttonAppearanceDuration = 0.5f;

    [SerializeField]
    private Ease buttonAppearanceEaseType = Ease.OutElastic;

    [SerializeField]
    private List<GameObject> buttons;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private Transform innerCircle;

    [SerializeField]
    private Transform outerCircle;

    [SerializeField]
    private Vector3 newButtonPosition;

    [SerializeField]
    private List<Vector3> buttonLocations;

    //[field: SerializeField,
    //        Required]
    //private Shape radialSlider;

    [field: SerializeField,
            Required]
    private RadialSlider radialSlider;
    public RadialSlider RadialSlider => radialSlider;

    [field: SerializeField,
            Required]
    private VideoController videoController;

    private VideoPlayer videoPlayer { get { return videoController.player; } }

    private float videoProgress { get { return (float)videoPlayer.frame / (float)videoPlayer.frameCount; } }

    private float videoCurrentTime { get { return (int)videoPlayer.frame / (int)videoPlayer.frameRate; } }

    private float videoTotalTime { get { return (int)videoPlayer.frameCount / (int)videoPlayer.frameRate; } }

    private bool videoIsPlaying { get { return videoPlayer.isPlaying; } }

    [field: SerializeField,
            Required]
    private TextMeshProUGUI currentTimeText;

    [field: SerializeField,
            Required]
    private TextMeshProUGUI totalTimeText;

    [field: SerializeField]
    private bool showTotalVideoTime = true;

    private void Start()
    {
        //Disable total video time text if it is not supposed to be shown
        if (!showTotalVideoTime)
            totalTimeText.gameObject.SetActive(false);

        RefreshVideoInfo();
    }

    private void Update()
    {
        RefreshVideoInfo();
    }

    private GameObject CreateButton()
    {
        //Define which circle the button will go to
        Transform parent;
        if (buttons.Count < innerCircleCapacity)
            parent = innerCircle;
        else
            parent = outerCircle;
        //Create the new button
        GameObject newButton = Instantiate(buttonPrefab, parent, false);
        newButton.transform.localPosition = new Vector3(0, -innerRadious, 0);
        newButton.transform.RotateAround(this.transform.position, new Vector3(0, 0, -1), innerCircleButtonTotalButtonsWidthAngle);
        //Button appearance animation
        newButton.transform.localScale = Vector3.zero;
        newButton.transform.DOScale(Vector3.one, buttonAppearanceDuration).SetEase(buttonAppearanceEaseType);

        //Rotate inner circle
        if (buttons.Count != 0)
        {
            //innerCircle.transform.Rotate(new Vector3(0, 0, innerCircleButtonWidthAngle / 2), Space.Self);
            float deltaAngle = innerCircle.transform.localRotation.z + newButton.transform.localRotation.z;
            Debug.Log("innerCircle.transform.localRotation.z " + innerCircle.transform.localRotation.z);
            Debug.Log("newButton.transform.localRotation.z " + newButton.transform.localRotation.z);
            Debug.Log(" deltaAngle " + deltaAngle);
            innerCircle.transform.DORotate(new Vector3(0, 0, innerCircleButtonWidthAngle / 2), circleRotationDuration, RotateMode.LocalAxisAdd).SetEase(circleEaseType);
            

            //newButton.transform.DORotate(new Vector3(0, 0, -deltaAngle), circleRotationDuration, RotateMode.WorldAxisAdd).SetEase(circleEaseType);
            //newButton.transform.DORotate(new Vector3(0, 0, 3 * newButton.transform.localRotation.z), circleRotationDuration, RotateMode.LocalAxisAdd).SetEase(circleEaseType);
            //newButton.transform.rotation = Quaternion.identity;
        }
            


        //Reset individual button rotation
        //foreach (GameObject button in buttons)
        //{
        //    button.transform.rotation = Quaternion.identity;
        //}
        //newButton.transform.rotation = Quaternion.identity;
        return newButton;
    }

    [Button]
    private void AddButton()
    {
        buttons.Add(CreateButton());
    }

    private Vector3 GetNextButtonPosition()
    {
        Vector3 position = Vector3.zero;
        switch (buttons.Count)
        {
            case 0: //first button
                position.y = -innerRadious;
                break;
            default:
                Debug.Log("Error");
                break;
        }
        return position;
    }

    private void FillButtonLocations()
    {
        float innerCircleSingleButtonAngle = 360f/(float)innerCircleCapacity;

        Vector3 startingPoint = new Vector3(0, -innerRadious, 0);

    }

    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return Quaternion.Euler(angles) * (point - pivot) + pivot;
    }

    //[Button]
    //public void SetRadialSliderValue(float value = 0)
    //{
    //    float minValue = 0f;
    //    float maxValue = 1f;
    //    if (value < minValue)
    //    {
    //        Debug.LogWarning("Value below mininmum");
    //        return;
    //    }
    //    if(value > maxValue)
    //    {
    //        Debug.LogWarning("Value above maximum");
    //        return;
    //    }
    //    radialSlider.settings.startAngle = Mathf.Lerp(359f, 0f, value);
    //}

    [Button]
    public void SetRadialSliderValue(float value = 0)
    {
        float minValue = 0f;
        float maxValue = 1f;
        if (value < minValue)
        {
//            Debug.LogWarning("Value below mininmum");
            return;
        }
        if (value > maxValue)
        {
//            Debug.LogWarning("Value above maximum");
            return;
        }
        radialSlider.SliderValueRaw = value;
        radialSlider.UpdateUI();
    }

    [Button]
    public void SetVideoPosition(float value)
    {
        float minValue = 0f;
        float maxValue = 1f;
        if (value < minValue)
        {
            Debug.LogWarning("Value below mininmum");
            return;
        }
        if (value > maxValue)
        {
            Debug.LogWarning("Value above maximum");
            return;
        }
        //videoPlayer.frame = (long)Mathf.Lerp(0f, (float)videoPlayer.frameCount, value);
        VideoManager.Instance.SetVideosPosition(value);
    }

    public void RefreshVideoInfo()
    {
        if(showTotalVideoTime)
            totalTimeText.text = SecondsToTime(videoTotalTime);
        currentTimeText.text = SecondsToTime(videoCurrentTime);
        SetRadialSliderValue(videoProgress);
    }

    private string SecondsToTime(float seconds)
    {
        //int secondsFloored = (int)Mathf.Floor(time);
        //int secondsInMinute = secondsFloored % (60 * 60);
        //int minutes = secondsFloored % 60;
        //int hours = minutes / 60;
        //string secondsString;
        //string minutesString;
        //if()
        //return "";

        TimeSpan time = TimeSpan.FromSeconds(seconds);

        //here backslash is must to tell that colon is
        //not the part of format, it just a character that we want in output
        string str = time.ToString(@"%m\:ss");
        return str;
    }
}
