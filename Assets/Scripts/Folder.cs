using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Folder
{
    public string Name;
    public List<File> Files;

    public bool CheckIfMaxCountIsReachedForEachFiles()
    {
        foreach (File file in Files)
        {
            if (!file.CheckIfMaxCountIsReached())
            {
                return false;
            }
        }

        return true;
    }

    public File SelectFile()
    {
        if (CheckIfMaxCountIsReachedForEachFiles())
        {
            Debug.LogError("This should not happen, add this and treat it before calling this method");
            return null;
        }

        bool isValid = false;
        File file = null;

        while (!isValid)
        {
            int selectedFile = UnityEngine.Random.Range(0, Files.Count);
            file = Files[selectedFile];

            isValid = !file.CheckIfMaxCountIsReached();
        }

        file.IncOrDecCurrentCountByOne(true);
        return file;
    }

    public void Reset()
    {
        foreach (File file in Files)
        {
            file.Reset();
        }
    }
}
