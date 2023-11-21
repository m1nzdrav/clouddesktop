using System.Collections;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.Subtitles
{
    public class SubtitlesStarter : MonoBehaviour
    {
        [SerializeField] private ListaRecords listaRecords;
        [SerializeField] private MultiPrefabInstruction _multiPrefabInstruction;
        [SerializeField] private string folderSubs;
        [SerializeField] private CustomDropdown _customDropdown;

        [field: SerializeField,
                Range(0f, 0.01f)]
        private float acceptableDeviation = 0.005f;

        [SerializeField] private Text subtitlesPanel;

        private void Start()
        {
            listaRecords = Config.LoadJsonFromLanguage<ListaRecords>(folderSubs + Config.ENGLISH);
        }

        public void ChangeList()
        {
            listaRecords = Config.LoadJsonFromLanguage<ListaRecords>(folderSubs + _customDropdown.selectedText.text);
            subtitlesPanel.font = (RegisterSingleton._instance.GetSingleton(typeof(FontSingleton)) as FontSingleton)?.GetFont(listaRecords.font);
            subtitlesPanel.alignment =
                _customDropdown.selectedIsArabic ? TextAnchor.MiddleRight : TextAnchor.MiddleLeft;

            ActivationCheck();
        }

        public void ActivationCheck()
        {
            for (int i = 0; i < listaRecords.Records.Count - 1; i++)
            {
                if (_multiPrefabInstruction.CurrentTime < listaRecords.Records[i + 1].TimeCode - acceptableDeviation)
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
}