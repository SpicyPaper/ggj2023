using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int currentRamUsage;

    [SerializeField]
    private int maxRamUsage = 25;

    [SerializeField]
    private TerminalData terminalData;

    private List<TMP_Text> listRamUsageText;

    private int startRamUsage;

    private char ramUsageCharacter = '|';

    private int ramUsageCharacterPerStep;

    // create a singleton to access this class from other classes
    public static GameHandler instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("GameHandler instance created");
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

        // calculate the ramUsageCharacterPerStep
        ramUsageCharacterPerStep = maxRamUsage / listRamUsageText.Count;

        // adapt the RamFullLife text based on the maxRamUsage
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

        // loop through the list of ramUsageText
        for (int i = 0; i < listRamUsageText.Count; i++)
        {
            int ramUsageDisplayInStep = Mathf.Max(
                (this.currentRamUsage - i * ramUsageCharacterPerStep) % ramUsageCharacterPerStep
                    + 1,
                0
            );

            Debug.Log("for i = " + i + " ramUsageDisplayInStep = " + ramUsageDisplayInStep);

            string ramUsageString = new string(ramUsageCharacter, ramUsageDisplayInStep);
            listRamUsageText[i].text = ramUsageString;
        }
    }

    void Update()
    {
        updateRamUsageText();
    }
}
