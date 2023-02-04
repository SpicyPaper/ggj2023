using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class FileWeightInitializerVisual : MonoBehaviour
{
    [SerializeField]
    private FallDownFileData fallDownFileData;

    private char ramUsageCharacter = '|';

    void Start()
    {
        int fileRamWeight = fallDownFileData.File.GetRamWeight();

        // get the text component of the current game object
        TMP_Text text = GetComponent<TMP_Text>();

        // set the text to the file ram weight
        text.text = new string(ramUsageCharacter, fileRamWeight);
    }
}
