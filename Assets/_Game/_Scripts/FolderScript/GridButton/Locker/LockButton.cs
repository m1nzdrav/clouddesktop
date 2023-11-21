using UnityEngine;

public abstract class LockButton : MonoBehaviour
{
    [SerializeField] protected bool locked = true;

    public bool Locked
    {
        get => locked;
    }

    public abstract void TryOffLocker();
}