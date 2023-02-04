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
        PY,
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

    public void IncOrDecCurrentCountByOne(bool increment)
    {
        CurrentCount += increment ? 1 : -1;
    }

    public bool CheckIfMaxCountIsReached()
    {
        return CurrentCount >= Count;
    }

    public void Reset()
    {
        CurrentCount = 0;
    }
}
