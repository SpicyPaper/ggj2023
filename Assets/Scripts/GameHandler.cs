using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    [SerializeField]
    private int currentRamUsage;

    [SerializeField]
    private TerminalData terminalData;

    [SerializeField]
    private GameObject spawnerParent;

    [SerializeField]
    private GameObject spawner;

    [SerializeField]
    private GameObject fallDownFileModel;

    [SerializeField]
    private GameObject fallDownFolderModel;

    [SerializeField]
    private GameObject finalFallDownFolderModel;

    [SerializeField]
    private GameZoneData gameZoneData;

    public int CounterClearedStage { get; private set; }

    [SerializeField]
    private List<Color> CpuColors;

    public int FilesCountMult = 5;

    public float FallDownElementMultiplier = 1.35f;

    private int maxRamUsage;

    private List<TMP_Text> listRamUsageText;
    private int startRamUsage;

    private char ramUsageCharacter = '|';

    private List<int> listCharacterPerStep;

    private int currentRamUsageStep = 0;

    private Level currentLevel = new Level1();
    private Stage currentStage;
    private int initStageIndex = 3; // Define the stage from which the player starts playing
    private int maxStageIndex = 5;

    private float neededDurationSpawnMin = 0.1f;
    private float neededDurationSpawnMax = 0.8f;
    private float neededDurationSpawnCurrent = 1.2f;
    private float elapsedDurationSpawn;

    private List<GameObject> fallDownFiles;

    // create a singleton to access this class from other classes
    public static GameHandler Instance;

    private GameStatus gameStatus = GameStatus.PrepareGame;

    private bool finalFolderHasSpawned = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        maxRamUsage = terminalData.RamFullLife.text.Length;

        // the list of character per step is based on the text size of each ramUsageText
        listCharacterPerStep = new List<int>();
        for (int i = 0; i < listRamUsageText.Count; i++)
        {
            listCharacterPerStep.Add(listRamUsageText[i].text.Length);
        }

        fallDownFiles = new List<GameObject>();

        terminalData.RamFullLife.text = new string(ramUsageCharacter, maxRamUsage);

        // Init stage
        currentStage = currentLevel.CreateStage();
        for (int i = 1; i < initStageIndex; i++)
        {
            currentStage.Reset();
            currentStage = currentStage.Previous;
        }
        currentStage.Init();
        terminalData.Path.text = currentStage.GetPath();

        CounterClearedStage = 0;

        SetGameStatus(GameStatus.InGame);
    }

    private void InGame()
    {
        UpdateCpuUsageText();
        UpdateRamUsageText();
        ChangeFileRamWeightColor();
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
        neededDurationSpawnCurrent = UnityEngine.Random.Range(
            neededDurationSpawnMin,
            neededDurationSpawnMax
        );

        // Select file
        if (currentStage.CheckIfNextStageCanSpawn())
        {
            if (finalFolderHasSpawned)
            {
                return;
            }

            // check if all element of the listFallText are null
            bool allNull = true;
            for (int i = 0; i < fallDownFiles.Count; i++)
            {
                if (fallDownFiles[i] != null)
                {
                    allNull = false;
                    break;
                }
            }

            if (!allNull)
            {
                return;
            }

            // clear the list
            fallDownFiles.Clear();

            // instantiate the final folder
            GameObject fallDownFolder = Instantiate(finalFallDownFolderModel);

            Transform firstSpawer = spawner.transform.GetChild(0);

            // set the position of the folder
            fallDownFolder.transform.parent = spawnerParent.transform;
            fallDownFolder.transform.localPosition = firstSpawer.localPosition;
            fallDownFolder.transform.localScale = Vector3.one;

            // from the new fallDownFolder, get the FallDownFolderMovement component
            // and set the player as target
            FallDownFinalFolderMovement fallDownFolderMovement =
                fallDownFolder.GetComponent<FallDownFinalFolderMovement>();

            fallDownFolderMovement.SetPlayerFollow(gameZoneData.Player);

            finalFolderHasSpawned = true;

            return;
        }
        File selectedFile = currentStage.CurrentFolder.SelectFile();

        // Select a spawner
        int spawnerIndex = Random.Range(0, spawner.transform.childCount);
        Transform selectedSpawner = spawner.transform.GetChild(spawnerIndex);

        // Spawn element on spawner
        GameObject fallDownFile = Instantiate(fallDownFileModel);
        fallDownFile.transform.parent = spawnerParent.transform;
        fallDownFile.transform.localPosition = selectedSpawner.localPosition;
        fallDownFile.transform.localScale = Vector3.zero;

        FallDownFileData fileData = fallDownFile.GetComponent<FallDownFileData>();
        fileData.fileName.text = "." + selectedFile.FileType.ToString().ToUpper();
        fileData.File = selectedFile;
        fallDownFiles.Add(fallDownFile);
    }

    public void GoToNextStage()
    {
        CounterClearedStage++;
        currentStage = currentStage.Next;
        if (currentStage == null)
        {
            terminalData.Path.text = "/";
            SetGameStatus(GameStatus.Win);
            Debug.Log("YOU WIN");
        }
        terminalData.Path.text = currentStage.GetPath();

        // TODO: Remove next line when the animation to change stage is done
        neededDurationSpawnCurrent = 2;

        finalFolderHasSpawned = false;
    }

    private void UpdateCpuUsageText()
    {
        float percentage =
            (maxStageIndex - initStageIndex + CounterClearedStage) / ((float)maxStageIndex);

        terminalData.CpuUsage.text = (percentage * 100).ToString() + "%";

        terminalData.CpuUsage.transform.localScale =
            Vector3.one * Mathf.Lerp(0.8f, 1.5f, percentage);

        int colorIndex;
        if (percentage >= 0.8f)
        {
            colorIndex = 4;
        }
        else if (percentage >= 0.6f)
        {
            colorIndex = 3;
        }
        else if (percentage >= 0.4f)
        {
            colorIndex = 2;
        }
        else if (percentage >= 0.2f)
        {
            colorIndex = 1;
        }
        else
        {
            colorIndex = 0;
        }

        terminalData.CpuUsage.color = CpuColors[colorIndex];
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

            if (ramUsageDisplayInStep > 0)
            {
                currentRamUsageStep = i;
            }

            string ramUsageString = new string(ramUsageCharacter, ramUsageDisplayInStep);
            listRamUsageText[i].text = ramUsageString;

            ramUsageCounter += listCharacterPerStep[i];
        }
    }

    private void ChangeFileRamWeightColor()
    {
        // get the color of the current ramUsageStep
        Color32 color = listRamUsageText[currentRamUsageStep].color;

        // loop through all the fallDownElements
        for (int i = fallDownFiles.Count - 1; i >= 0; i--)
        {
            if (fallDownFiles[i] == null)
            {
                continue;
            }
            // try to access the FallDownFileData component of the fallDownElement
            // if it doesn't have the component, continue to the next element
            FallDownFileData fallDownFileData = fallDownFiles[i].GetComponent<FallDownFileData>();
            if (fallDownFileData == null)
            {
                continue;
            }

            fallDownFileData.fileRamWeight.color = color;
        }
    }
}
