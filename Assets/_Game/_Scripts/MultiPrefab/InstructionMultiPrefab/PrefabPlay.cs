using System;
using UnityEngine;

[Serializable]
public abstract class PrefabPlay : MonoBehaviour
{
    public abstract void Play();
    public abstract void Prepare();
    public abstract void Off();
    public abstract void Pause();
    public abstract void SetTime(float time);
    public abstract float GetMaxTime();
}