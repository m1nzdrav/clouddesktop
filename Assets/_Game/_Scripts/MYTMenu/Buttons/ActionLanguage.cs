using System;
using Michsky.UI.ModernUIPack;
using Sirenix.Utilities;
using UnityEngine;

namespace _Game._Scripts.MYTMenu.Buttons
{
    public class ActionLanguage : MonoBehaviour
    {
        [SerializeField] private ChangeLanguageLoginScene _changeLanguageLoginScene;
        [SerializeField] private CustomDropdown _dropdown;
        [SerializeField] private bool isFirst = true;

        [SerializeField, Header("FirstAction")]
        private ActivateCanvas grid;

        // private void Start()
        // {
        //     if (isFirst)
        //     {
        //         UpdateLanguageIcon();
        //         // _changeLanguageLoginScene.
        //     }
        // }

        public void UpdateLanguageIcon()
        {
            if (ChangeLanguageLoginScene.currentLanguage.IsNullOrWhitespace())
                ChangeLanguageLoginScene.currentLanguage = "Russian";
            
            _dropdown.ChangeDropdownInfo(_dropdown.dropdownItems
                .Find(x => x.itemName.ToUpper() == ChangeLanguageLoginScene.currentLanguage.ToUpper()).number);
        }

        public void AnimationGrid()
        {
            _changeLanguageLoginScene.RabbitSub();
            grid.Activate();
            // 
        }

        public void AnimationStarting()
        {
            _changeLanguageLoginScene.StartSub();

            // grid.DOFade(1, animationFade);
        }

        public void AnimationObjectController()
        {
            _changeLanguageLoginScene.ObjectControlSub();
            // grid.DOFade(1, animationFade);
        }
    }
}