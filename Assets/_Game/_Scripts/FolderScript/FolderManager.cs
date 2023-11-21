using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class FolderManager : MonoBehaviour
{
    public FileInFolder _fileInFolder;

    public Folder _folder;

    // Start is called before the first frame update
    void Start()
    {
        String path = Application.dataPath + "/" + "DROPDOWN" + ".json";
        String jsonString = File.ReadAllText(path);
        _folder = JsonUtility.FromJson<Folder>(jsonString);
    }

    public void CreateFileInFolder()
    {
        
    }
    // Update is called once per frame
    
}

[Serializable]
public class FileInFolder
{
    public String Name;
    public bool existBranch;
    public String pathBranch;
}

[Serializable]
public class Folder
{
    public String name;
    public int countFolder;
    public List<FileInFolder> fileInFolders;
}