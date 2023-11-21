using System.Threading.Tasks;
using UnityEngine;

public class AnimationEngine : MonoBehaviour
{
    [SerializeField] private SpriteAnimation[] graphicMaterials;
    int i = 0;

    private void Update()
    {
        // Parallel.For(i, graphicMaterials.Length, x => graphicMaterials[x].Dissolve());
        for (int i = 0; i < graphicMaterials.Length; i++)
        {
            // graphicMaterials[i].Dissolve();
        }
    }
}
