using System;
using UnityEngine;

[Serializable]
public abstract class GraphicMaterial : MonoBehaviour
{
    public abstract void Dissolve();
    public abstract void ChangeStateDissolve();
}