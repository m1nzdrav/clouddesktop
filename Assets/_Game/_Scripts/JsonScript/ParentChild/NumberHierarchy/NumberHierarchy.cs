using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Game._Scripts.JsonScript.ParentChild.NumberHierarchy
{
    public class NumberHierarchy : MonoBehaviour
    {
        [SerializeField] private Text number;

        public void ChangeNumber()
        {
            try
            {
                number.text =
                    (Int32.Parse(GetComponent<FolderModel>().MyParent.My.GetComponent<NumberHierarchy>().number.text) +
                     1).ToString();
            }
            catch (Exception e)
            {
                number.text = "0";
            }
        }
    }
}