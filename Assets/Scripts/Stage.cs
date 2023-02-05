using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stage
{
    public Stage Previous;
    public Stage Next;
    public List<Folder> Folders;

    public Folder CurrentFolder { get; private set; }

    public bool CheckIfNextStageCanSpawn()
    {
        return CurrentFolder.CheckIfMaxCountIsReachedForEachFiles();
    }

    public string GetPath()
    {
        List<string> names = new List<string>();
        Stage currentStage = this;

        while (currentStage != null)
        {
            names.Add(currentStage.CurrentFolder.Name);
            currentStage = currentStage.Next;
        }

        string path = "/";
        for (int i = names.Count - 1; i >= 0; i--)
        {
            string name = names[i];
            path += name + "/";
        }

        return path;
    }

    public void Reset()
    {
        foreach (Folder folder in Folders)
        {
            folder.Reset();
        }
    }

    public void Init()
    {

        Stage currentStage = this;

        while (currentStage != null)
        {
            currentStage.SelectCurrentFolder();
            currentStage = currentStage.Next;
        }
    }

    private void SelectCurrentFolder()
    {
        int selectedFolder = UnityEngine.Random.Range(0, Folders.Count);
        CurrentFolder = Folders[selectedFolder];
    }
}
