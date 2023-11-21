using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using UnityEngine;

namespace _Game._Scripts.Button.RadialMenu
{
    public class RadialSubtitles : RadialListener
    {
        [SerializeField] private ActivateCanvas _canvasGroup;
        [SerializeField] private bool flagActive;

        public override void SetNewSetting()
        {
            PrefabName prefabName = (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.GetLastPrefab().GetComponent<PrefabName>();

            switch (prefabName.Prefab)
            {
                case Prefab.TwoVideos1:
                {
                    prefabName.GetComponent<MultiPrefabElement>().MultiPrefab.MultiPrefabInstruction
                        .EventTimeChange.AddListener(() =>
                        {
                            // _radialSlider.currentValue += Time.deltaTime;
                        });
                    break;
                }
            }
        }

        public void ChangeActive()
        {
            if (flagActive)
            {
                _canvasGroup.DeActivate();
                flagActive = false;
            }
            else
            {
                _canvasGroup.Activate();
                flagActive = true;
            }
        }
    }
}