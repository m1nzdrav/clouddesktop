using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class FileManager
{
    protected abstract void AddAsIcon(string path = null, byte[] data = null);

    public void LoadFileData(string path)
    {
        var data = File.ReadAllBytes(path);
        AddAsIcon(path, data);
    }
}
