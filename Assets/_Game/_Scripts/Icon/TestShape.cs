using Shapes2D;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game._Scripts.Icon
{
    public class TestShape : MonoBehaviour
    {
        [Button]
        public void test()
        {
            // GetComponent<Shape>().enabled = false;
            Debug.LogError("test");

            transform.rotation = new Quaternion(90, 0, 0, 0);
            // GetComponent<Shape>().enabled = true;

            // transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        [Button]
        public void ShapeOn()
        {
            Debug.LogError("test");

            GetComponent<Shape>().ComputeAndApply();
        }

        [Button]
        public void testRotate()
        {
            Debug.LogError("test");

            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        [Button]
        public void AllTest()
        {
            test();
            ShapeOn();
            testRotate();
        }
    }
}