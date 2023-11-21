using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using Michsky.UI.ModernUIPack;
using UnityEngine;

namespace _Game._Scripts.Button.RadialMenu
{
    public class RadialSliderPath : RadialListener
    {
        [SerializeField] private RadialSlider _radialSlider;

        public override void SetNewSetting()
        {
            PrefabName prefabName = (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.GetLastPrefab().GetComponent<PrefabName>();
            
            switch (prefabName.Prefab)
            {
                case Prefab.TwoVideos1:
                {
                    prefabName.GetComponent<MultiPrefabElement>().MultiPrefab.MultiPrefabInstruction
                        .EventTimeChange.AddListener(() => { _radialSlider.currentValue += Time.deltaTime; });
                    break;
                }
            }
        }
    }
}