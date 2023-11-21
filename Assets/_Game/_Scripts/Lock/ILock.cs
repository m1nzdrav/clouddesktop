using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILock
{
    bool IsLock { get; set; }
    void Lock();
    void Unlock();
}
