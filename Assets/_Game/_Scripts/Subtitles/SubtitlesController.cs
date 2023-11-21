using Sirenix.OdinInspector;
using System.Collections;
using System.IO;
using System.Text;
using _Game._Scripts;
using UnityEngine;
using DG.Tweening;
using Michsky.UI.ModernUIPack;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SubtitlesController : MonoBehaviour
{
    #region (Parametrs)

    private Text _text;

    [field: SerializeField,
            Tooltip("Position of the player window to return to on Show()")
    ]
    private Transform subtitlesActivePosition;

    [SerializeField] private GameObject subtitles;

    [field: SerializeField,
            Tooltip("This wondow's CanvasGroup reference"),
            ReadOnly]
    private CanvasGroup playerCanvas;


    [field: SerializeField,
            Tooltip("Duration of hiding animation")]
    private float hideAnimationDuration;

    [field: SerializeField,
            Tooltip("Location to shrink to on hiding")]
    private Transform hideLocation;

    [field: SerializeField,
            Required]
    private CircleMenuController circleController;

    [SerializeField] private Text subtitlesPanel;

    [field: SerializeField,
            Range(0f, 0.01f)]
    private float acceptableDeviation = 0.005f;

    // private List<LanguageSub> obj;
    string path;
    string jsonString;

    private ListaRecords listaRecords;

    private DropdownRecord _dropdownRecord = new DropdownRecord();
    //public GameObject sub;

    private LanguageSubContainer prevSubContainer;

    [FormerlySerializedAs("languageObject")] [SerializeField]
    private GameObject dropdownGameObject;

    private LanguageSubContainer[] _languageSubContainers;

    #endregion

    private void Awake()
    {
        Hide();
        path = Application.dataPath + "/Resources/" + "DROPDOWN" + ".json";

        jsonString = File.ReadAllText(path);
        _dropdownRecord = JsonUtility.FromJson<DropdownRecord>(jsonString);
//        Debug.LogError(_dropdownRecord.Records.Count);

        _text = dropdownGameObject.GetComponent<CustomDropdown>().selectedText;
        // ЗАписываем Путь к json файлу
        path = Application.dataPath + "/Resources/" + "/ENGLISH.json";
        // Записываем в строку все содержимое json файла
        jsonString = File.ReadAllText(path);
        listaRecords = JsonUtility.FromJson<ListaRecords>(jsonString);

    }

    public void ChangeLanguage()
    {
        Debug.LogError(_text.text);
        path = Application.dataPath + "/" + _text.text + ".json";
//        Debug.LogError(_text.text);

        if (_text.text == "ARABIC")

            subtitlesPanel.alignment = TextAnchor.MiddleRight;

        else
            subtitlesPanel.alignment = TextAnchor.MiddleLeft;

        // Записываем в строку все содержимое json файла
        jsonString = File.ReadAllText(path);

        listaRecords = JsonUtility.FromJson<ListaRecords>(jsonString);

        for (int i = 0; i < listaRecords.Records.Count - 1; i++)
        {
            if (!(circleController.RadialSlider.SliderValueRaw < listaRecords.Records[i + 1].TimeCode)) continue;

            subtitlesPanel.text = listaRecords.Records[i].Subtitles1;
//                Debug.LogError("Текущее время видео = " + circleController.RadialSlider.SliderValueRaw + "" +
//                               "Текущие субтитры = " + listaRecords.Records[i].Subtitles1 + "" +
//                               "Текущее время = " + listaRecords.Records[i].TimeCode);
            return;
        }
    }

    /// <summary>
    /// Show this video window
    /// </summary>
    [ButtonGroup("VisibilityToggle")]
    public void Hide()
    {
        if (hideLocation != null)
        {
            subtitles.transform.DOMove(hideLocation.position, hideAnimationDuration);
            subtitles.transform.DOScale(Vector3.zero, hideAnimationDuration);
        }

        playerCanvas.DOFade(0, hideAnimationDuration);
//        Invoke("Deactivate", hideAnimationDuration);
    }

    /// <summary>
    /// Show this video window
    /// </summary>
    [ButtonGroup("VisibilityToggle")]
    public void Show()
    {
//        Debug.LogError("Вызвал шоу");
        playerCanvas.DOFade(1, hideAnimationDuration);
        subtitles.transform.DOMove(subtitlesActivePosition.position, hideAnimationDuration);
        subtitles.transform.DOScale(Vector3.one, hideAnimationDuration);
    }


    private void FixedUpdate()
    {
        ActivationCheck();
    }

    private void ActivationCheck()
    {
        for (int i = 0; i < listaRecords.Records.Count - 1; i++)
        {
            if (circleController.RadialSlider.SliderValueRaw <
                listaRecords.Records[i + 1].TimeCode - acceptableDeviation)
            {
                StartCoroutine(ResetActivation(listaRecords.Records[i].Subtitles1));

                return;
            }
        }
    }

    IEnumerator ResetActivation(string subs)
    {
        yield return new WaitForSeconds(acceptableDeviation * 2);

        subtitlesPanel.text = subs;
    }
}