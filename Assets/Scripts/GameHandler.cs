using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.Mathematics;

public enum GameStatus
{
    PrepareGame,
    InGame,
    Win,
    Lose,
    Pause
}

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

    private GameStatus gameStatus = GameStatus.PrepareGame;

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

    void Update()
    {
        switch (gameStatus)
        {
            case GameStatus.PrepareGame:
                PrepareGame();
                break;
            case GameStatus.InGame:
                InGame();
                break;
            case GameStatus.Win:
                break;
            case GameStatus.Lose:
                LoseGame();
                break;
            case GameStatus.Pause:
                PauseGame();
                break;
            default:
                break;
        }
    }

    private void PrepareGame()
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

        SetGameStatus(GameStatus.InGame);
    }

    private void InGame()
    {
        UpdateRamUsageText();
        UpdateSpawner();

        // if escape key is pressed, pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetGameStatus(GameStatus.Pause);
        }
    }

    private void PauseGame()
    {
        // if escape key is pressed, resume the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetGameStatus(GameStatus.InGame);
        }
    }

    private void LoseGame()
    {
        Debug.Log("You lose");
    }

    public void SetGameStatus(GameStatus gameStatus)
    {
        this.gameStatus = gameStatus;
    }

    public GameStatus GetGameStatus()
    {
        return gameStatus;
    }

    public void AddReduceRameUsage(int value)
    {
        currentRamUsage += value;
        if (currentRamUsage >= maxRamUsage)
        {
            UpdateRamUsageText();
            SetGameStatus(GameStatus.Lose);
        }
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
}
