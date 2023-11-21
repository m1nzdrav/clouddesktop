using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnlimitedScrollUI.Example {
    public class ScrollerTest : MonoBehaviour {
        public GameObject cell;
        public bool autoGenerate;
        public int totalCount = 33;

        private IUnlimitedScroller unlimitedScroller;

        [Button]
        public void Generate() {
            unlimitedScroller.Generate(cell, totalCount);
        }

        private void Start() {
            unlimitedScroller = GetComponent<IUnlimitedScroller>();
            // Wait until the scroller size was set by other layout controllers.
            if (autoGenerate) {
                StartCoroutine(DelayGenerate());
            }
        }

        private IEnumerator DelayGenerate() {
            yield return new WaitForEndOfFrame();
            unlimitedScroller.Generate(cell, totalCount);
        }
    }
}