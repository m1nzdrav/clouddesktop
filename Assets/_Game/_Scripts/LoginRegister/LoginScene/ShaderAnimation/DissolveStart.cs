using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;


public class DissolveStart : MonoBehaviour
{
    [SerializeField] private GraphicMaterial[] graphicMaterials;

    [Button]
    public void ChangeAnimation()
    {
        Parallel.For(0, graphicMaterials.Length, x => graphicMaterials[x].ChangeStateDissolve());
    }
}