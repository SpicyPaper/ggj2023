using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int currentRamUsage;

    private int maxRamUsage = 25;

    [SerializeField]
    private TerminalData terminalData;

    private List<TMP_Text> listRamUsageText;
    private int startRamUsage;

    private char ramUsageCharacter = '|';

    private List<int> listCharacterPerStep;

    // create a singleton to access this class from other classes
    public static GameHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startRamUsage = currentRamUsage;
        listRamUsageText = terminalData.RamStepsLife;

        // the list of character per step is based on the text size of each ramUsageText
        listCharacterPerStep = new List<int>();
        for (int i = 0; i < listRamUsageText.Count; i++)
        {
            listCharacterPerStep.Add(listRamUsageText[i].text.Length);
        }

        terminalData.RamFullLife.text = new string(ramUsageCharacter, maxRamUsage);
    }

    public void AddReduceRameUsage(int value)
    {
        currentRamUsage += value;
    }

    public void updateRamUsageText()
    {
        // if ramUsageText is null, return
        if (listRamUsageText == null)
        {
            return;
        }

        int ramUsageCounter = 0;
        // loop through the list of ramUsageText
        for (int i = 0; i < listRamUsageText.Count; i++)
        {
            int ramUsageDisplayInStep = Mathf.Min(
                Mathf.Max((this.currentRamUsage - ramUsageCounter), 0),
                listCharacterPerStep[i]
            );

            string ramUsageString = new string(ramUsageCharacter, ramUsageDisplayInStep);
            listRamUsageText[i].text = ramUsageString;

            ramUsageCounter += listCharacterPerStep[i];
        }
    }

    void Update()
    {
        updateRamUsageText();
    }
}
