using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class File
{
    public enum Type
    {
        C,
        H,
        JS,
        SH,
        CS, // Replaced py by cs because our font has an issue with capital Y
        PDF,
        PNG,
        TXT,
        ZIP,
        MP3,
        CSS,
        AVI,
        HTML,
        DOCX,
        PPTX,
        XLSX,
        JPEG,
        TORRENT,
        GITIGNORE
    }

    public Type FileType;
    public int Count;

    public int CurrentCount { get; private set; }

    public int GetRamWeight()
    {
        switch (FileType)
        {
            case Type.C:
                return 2;
            case Type.H:
                return 2;
            case Type.JS:
                return 3;
            case Type.SH:
                return 4;
            case Type.CS:
                return 5;
            case Type.PDF:
                return 10;
            case Type.PNG:
                return 8;
            case Type.TXT:
                return 3;
            case Type.ZIP:
                return 13;
            case Type.MP3:
                return 7;
            case Type.CSS:
                return 3;
            case Type.AVI:
                return 15;
            case Type.HTML:
                return 5;
            case Type.DOCX:
                return 12;
            case Type.PPTX:
                return 11;
            case Type.XLSX:
                return 10;
            case Type.JPEG:
                return 9;
            case Type.TORRENT:
                return 28;
            case Type.GITIGNORE:
                return 1;
            default:
                return -0;
        }
    }

    public float GetSpeed()
    {
        switch (FileType)
        {
            case Type.C:
                return 1.0f;
            case Type.H:
                return 1.0f;
            case Type.JS:
                return 0.8f;
            case Type.SH:
                return 0.65f;
            case Type.CS:
                return 0.38f;
            case Type.PDF:
                return 0.6f;
            case Type.PNG:
                return 0.59957f;
            case Type.TXT:
                return 0.55f;
            case Type.ZIP:
                return 0.65f;
            case Type.MP3:
                return 0.58665f;
            case Type.CSS:
                return 0.56f;
            case Type.AVI:
                return 0.27f;
            case Type.HTML:
                return 0.51f;
            case Type.DOCX:
                return 0.546f;
            case Type.PPTX:
                return 0.7123f;
            case Type.XLSX:
                return 0.76555123f;
            case Type.JPEG:
                return 0.5722f;
            case Type.TORRENT:
                return 0.2f;
            case Type.GITIGNORE:
                return 1;
            default:
                return -0.715489525f;
        }
    }

    public void IncOrDecCurrentCountByOne(bool increment)
    {
        CurrentCount += increment ? 1 : -1;
    }

    public bool CheckIfMaxCountIsReached()
    {
        return CurrentCount >= Count * GameHandler.Instance.FilesCountMult;
    }

    public void Reset()
    {
        CurrentCount = 0;
    }
}
