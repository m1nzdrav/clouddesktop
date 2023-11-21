using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Game._Scripts.FolderScript.Change_Sprite_Border
{
    public class ColorSync : MonoBehaviour
    {
        [SerializeField] private List<ChildBorder> myColors;
        [SerializeField] private List<ColorSync> syncWithMe;


        public void SetColor(Color color)
        {
            SetMyColors(color);

            foreach (var VARIABLE in syncWithMe)
            {
                VARIABLE.SetColor(color);
            }
        }

        private void SetMyColors(Color color)
        {
            foreach (var VARIABLE in myColors)
            {
                try
                {
                    VARIABLE.SetColor(color);
                }
                catch (Exception e)
                {
                    // Debug.LogError(e);
                    // throw;
                }
            }
        }
    }
}