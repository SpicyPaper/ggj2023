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
}
