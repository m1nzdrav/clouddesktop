using System;
using UnityEngine;

[Serializable]
public abstract class AInstruction : MonoBehaviour
{
    public AnimationInstruction animation;
    public abstract Action OnAction();
    public abstract Action OffAction();
    public abstract Action GetPause();
    public abstract Action Restart();
    public abstract Action Prepare();
    public abstract void SetTime(float time);
    public abstract float GetMaxTime();
}