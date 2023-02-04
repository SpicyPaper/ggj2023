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

    private int maxRamUsage = 25;

    [SerializeField]
    private TerminalData terminalData;

    private List<TMP_Text> listRamUsageText;
    private int startRamUsage;

    private char ramUsageCharacter = '|';

    private List<int> listCharacterPerStep;

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

        terminalData.RamFullLife.text = new string(ramUsageCharacter, maxRamUsage);

        SetGameStatus(GameStatus.InGame);
    }

    private void InGame()
    {
        updateRamUsageText();
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
            updateRamUsageText();
            SetGameStatus(GameStatus.Lose);
        }
    }

    private void updateRamUsageText()
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
