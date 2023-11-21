using System.Collections;
using _Game._Scripts.FolderScript.Name;
using _Game._Scripts.Panel;
using Doozy.Engine.Extensions;
using Shapes2D;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game._Scripts.JsonScript
{
    public class LoginSceneArea : Parent
    {
        [SerializeField] private Transform grid;
        [SerializeField] private Transform iconContainer;
        [SerializeField] private RotatePlanes _rotatePlanes;
        [SerializeField] private Parent menuFolder;

        [Button]
        public void New()
        {
            // RegisterSingleton._instance.ManagerJson.SetNew(Prefab.TwoVideos1,
            //     menuFolder);
            
            // RegisterSingleton._instance.ManagerJson.SetNew(Prefab.MYTFolderNewDesign,
            //     menuFolder);
            
            // for (int i = 0; i < 2; i++)
            // {
            //     RegisterSingleton._instance.ManagerJson.SetNew(Prefab.MYTFolderNewDesign,
            //         menuFolder);
            // }

            // RegisterSingleton._instance.ManagerJson.SetNew(Prefab.TwoVideos1, this);
            // RegisterSingleton._instance.ManagerJson.SetNew(Prefab.MYTCalendar, this);
        }

        public override void SpawnChild(GameObject child)
        {
            child.GetComponent<Parent>().MyParent = this;
            child.SetActive(true);

            if (child.GetComponent<PrefabName>().Prefab == Prefab.MYTIcon60_60)
                SetChildIcon(child);
            else
                SetChildFolder(child);
        }

        public void SetChildIcon(GameObject icon)
        {
            _rotatePlanes?._folders.Add(icon.GetComponent<EventName>());

            icon.GetComponent<RectTransform>().FullScreen(true);

            icon.SetActive(true);
            icon.transform.SetParent(iconContainer, false);
            icon.GetComponent<WaitPress>().IsLockDrag = true;
            // Rotate90(icon);
            // Shape(icon);
            // Rotate0(icon);
            StartCoroutine(FixShape(icon));

            // icon.transform.rotation = rotation;
            // icon.transform.localScale = Vector3.one * 3;             // :(
        }

        private IEnumerator FixShape(GameObject icon)
        {
            yield return new WaitForSeconds(.5f);

            Rotate90(icon);
            Shape(icon);
            Rotate0(icon);
        }

        private static void Rotate90(GameObject icon)
        {
            icon.transform.rotation = new Quaternion(90, 0, 0, 0);
        }

        private void Shape(GameObject icon)
        {
            icon.GetComponent<Shape>().ComputeAndApply();
        }

        private static void Rotate0(GameObject icon)
        {
            icon.transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        public void SetChildFolder(GameObject folder)
        {
            folder.transform.SetParent(grid, false);
        }
    }
}