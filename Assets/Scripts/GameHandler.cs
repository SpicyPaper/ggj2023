using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.Mathematics;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int currentRamUsage;

    [SerializeField] private TerminalData terminalData;

    [SerializeField] private GameObject spawnerParent;

    [SerializeField] private GameObject spawner;

    [SerializeField] private GameObject fallDownElementModel;

    private int maxRamUsage = 25;

    private List<TMP_Text> listRamUsageText;
    private int startRamUsage;

    private char ramUsageCharacter = '|';

    private List<int> listCharacterPerStep;

    private Level currentLevel = new Level1();
    private Stage currentStage;
    private int maxStageIndex = 3; // Define the last stage that will be played
    private int currentStageIndex = 1;

    private float neededDurationSpawnMin = 0.2f;
    private float neededDurationSpawnMax = 1.2f;
    private float neededDurationSpawnCurrent= 1.2f;
    private float elapsedDurationSpawn;

    private List<GameObject> fallDownElements;

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
            Debug.LogError("Another instance of GameHandler already exists!");
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

        fallDownElements = new List<GameObject>();

        terminalData.RamFullLife.text = new string(ramUsageCharacter, maxRamUsage);

        currentStage = currentLevel.CreateStage();
    }

    void Update()
    {
        UpdateRamUsageText();
        UpdateSpawner();
    }

    private void UpdateSpawner()
    {
        elapsedDurationSpawn += Time.deltaTime;

        if (elapsedDurationSpawn < neededDurationSpawnCurrent)
        {
            return;
        }
        elapsedDurationSpawn = 0;
        neededDurationSpawnCurrent = UnityEngine.Random.Range(neededDurationSpawnMin, neededDurationSpawnMax);

        // Select a spawner
        int spawnerIndex = UnityEngine.Random.Range(0, spawner.transform.childCount - 1);
        Transform selectedSpawner = spawner.transform.GetChild(spawnerIndex);

        // Spawn element on spawner
        GameObject fallDownElement = Instantiate(fallDownElementModel);
        fallDownElement.transform.parent = spawnerParent.transform;
        fallDownElement.transform.localPosition = selectedSpawner.localPosition;
        fallDownElement.transform.localScale = Vector3.one;
        fallDownElements.Add(fallDownElement);
    }

    private void UpdateRamUsageText()
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

    public void AddReduceRameUsage(int value)
    {
        currentRamUsage += value;
    }
}
