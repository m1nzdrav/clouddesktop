using System;
using System.Collections.Generic;
using UnityEngine;
//
// [Serializable]
// public class FamilyTree : MonoBehaviour
// {
//     [SerializeField] private FamilyTree parent;
//     [SerializeField] private List<FamilyTree> childs;
//
//
//     public FamilyTree MyParent => parent;
//
//
//     public void SetChild(GameObject child)
//     {
//         if (childs == null)
//         {
//             childs = new List<FamilyTree>();
//         }
//
//         if (childs.Find(x => x.gameObject.name == child.gameObject.name)) return;
//
//         childs.Add(child);
// Debug.LogError(child);
//         if (child.parent == null)
//         {
//             child.parent = new FamilyTree();
//         }
//
//         child.parent = this;
//     }
// }