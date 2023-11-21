using System;
using _Game._Scripts.Panel;
using _Game._Scripts.PrefabeScript;
using DG.Tweening;
using UnityEngine;

namespace _Game._Scripts.Button.Rotate
{
    public class HeadRotate : MonoBehaviour, ISingleton
    { 

        public string NameComponent
        {
            get => name;
        }
        [SerializeField] private RotateButton currentRotateButton;

        public void CheckRegister()
        {
        }
        private void Awake()
        {
            if (!RegisterSingleton._instance.AddNewSingleton(this)) Destroy(this.gameObject);
            else
            {
                transform.SetParent(RegisterSingleton._instance.transform);
                CheckRegister();
            }
        }
        private void Start()
        {
            SettingButton();
        }


        public void SettingAfterStart(RotateButton currentRotateButton)
        {
            this.currentRotateButton = currentRotateButton;

            SettingButton();
        }

        public RotateButton CurrentRotateButton
        {
            get => currentRotateButton;
        }

        // public void SetCurrentRotate(RotateButton currentRotateButton)
        // {
        //     this.currentRotateButton = currentRotateButton;
        //     ChangePrefabSettingButton();
        // }

        public void RotateAll()
        {
            foreach (var VARIABLE in (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?.StockOpenedPrefab)
            {
                if (VARIABLE.Prefab != Prefab.Button && VARIABLE.Prefab != Prefab.MYTPromo1)
                {
                    VARIABLE.GetComponent<Transform>().DOLocalRotate(new Vector3(0, 0, 0), .5f)
                        .OnComplete(currentRotateButton.VideoRotateBasePosition);
                }
            }
        }

        public void ChangePrefabSettingButton()
        {
            SettingButton();
            currentRotateButton.AfterChangeListener();
        }

        private void SettingButton()
        {
            GameObject lastPrefab = (RegisterSingleton._instance.GetSingleton(typeof(OpenedStock)) as OpenedStock)?
                .GetLastPrefab();

            if (lastPrefab == null) return;

            Transform area = lastPrefab?.transform;

            if (area.GetComponent<PrefabName>().Prefab != Prefab.Button)
                currentRotateButton?.SetWithoutNotifyVideoPosition(area
                    .localEulerAngles.y);

            if (area.TryGetComponent(out MytPrefabColorTypes result))
                currentRotateButton?.ChangeColor(result.ParentBorder.color);
        }
    }
}