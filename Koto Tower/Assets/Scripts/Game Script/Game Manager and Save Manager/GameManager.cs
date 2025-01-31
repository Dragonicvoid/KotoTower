﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStatus
{
    PAUSED,
    PLAY,
    SELECTING_TOWER
}

public class GameManager : MonoBehaviour
{
    // private method
    [SerializeField] GameObject loadingScreen = null;
    [SerializeField] MainMenuAudio mainMenuAudio = null;

    bool isLoading;
    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();

    // singleton
    public static GameManager instance;

    // player save state
    public SaveState saveFile;

    // player login attribute
    public bool hasLogin;
    public int userId;

    // Level selection attribute
    public int currentSceneIndex;
    public int currentLevelIndex;

    // attribute for game
    public bool isSelectTrap;
    public short selectedTrap;
    public bool isDoneMakingTrapLines;
    public bool isPressedButtonTrap;

    public bool isSelectTower;
    public short selectedTower;
    public bool isDoneMakingTowerLines;
    public bool isPressedButtonTower;

    public bool isPaused;
    public bool isPractice;
    public bool isTutorial;

    public int enemyVariation;

    // for leaderboards
    public Scoreboard scoreboard;

    // for "Rangkuman" menu
    public int selectedSummaryIndex;

    // attribute for question and answer
    public bool isNewQuestion;

    public GameStatus currentStatus;

    public float money;
    public bool moneyChanged;

    public List<TowerPrice> towerPrices;
    public List<TrapPrice> trapPrices;

    public int totalAnsweredQuestion;
    public int maxCharged;
    public bool isChangedDifficulty;
    public bool isChangedEnemyGroupSize;
    public bool gameStart;

    public Camera mainCamera;

    // attribute for level selection
    public int difficultyIdx;

    // Audio Source
    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        hasLogin = false;
        userId = -1;
        SceneManager.LoadSceneAsync((int)Levels.PRELOAD, LoadSceneMode.Additive);
        currentSceneIndex = (int)Levels.PRELOAD;
        selectedSummaryIndex = -1;
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // get the camera variable at start of level load
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        scoreboard = new Scoreboard();
        resetOnPlay();
        isLoading = false;
    }

    // initialization
    public void resetOnPlay()
    {
        isSelectTrap = false;
        isSelectTower = false;

        currentStatus = GameStatus.PLAY;

        isDoneMakingTowerLines = false;
        isDoneMakingTrapLines = false;

        isNewQuestion = true;

        selectedTower = -1;
        selectedTrap = -1;

        isPressedButtonTower = false;
        isPressedButtonTrap = false;

        isChangedDifficulty = false;
        isChangedEnemyGroupSize = false;

        moneyChanged = false;

        isPaused = false;

        totalAnsweredQuestion = 0;

        scoreboard.reset();
    }

    // set the money and says that the money has changed
    public void setMoney(float money)
    {
        this.money = money;
        moneyChanged = true;
    }

    // add the money and says that the money has changed
    public void addMoney(float money)
    {
        this.money += money;
        moneyChanged = true;
    }

    // set the Tower and Trap prices
    public void setPrices(List<TowerPrice> towerP, List<TrapPrice> trapP)
    {
        towerPrices = new List<TowerPrice>();
        trapPrices = new List<TrapPrice>();

        towerPrices.AddRange(towerP);
        trapPrices.AddRange(trapP);
    }

    public void setEnemyVariation(int enemyVariation)
    {
        this.enemyVariation = enemyVariation;
    }

    // pay for tower
    public void pay(TowerType type)
    {
        switch (type)
        {
            case TowerType.MACHINE_GUN:
                money -= towerPrices[0].price;
                break;
            case TowerType.SNIPER:
                money -= towerPrices[1].price;
                break;
            case TowerType.ELECTRIC:
                money -= towerPrices[2].price;
                break;
            default:
                break;
        }
        moneyChanged = true;
    }

    // get money value
    public float getPrice(TowerType type)
    {
        switch (type)
        {
            case TowerType.MACHINE_GUN:
                return towerPrices[0].price;
            case TowerType.SNIPER:
                return towerPrices[1].price;
            case TowerType.ELECTRIC:
                return towerPrices[2].price;
            default:
                return towerPrices[0].price;
        }
    }

    // pay for trap
    public void pay(TrapType type)
    {
        switch (type)
        {
            case TrapType.BOMB_TRAP:
                money -= trapPrices[0].price;
                break;
            case TrapType.TIME_TRAP:
                money -= trapPrices[1].price;
                break;
            case TrapType.FREEZE_TRAP:
                money -= trapPrices[2].price;
                break;
            default:
                break;
        }
        moneyChanged = true;
    }

    // get money value
    public float getPrice(TrapType type)
    {
        switch (type)
        {
            case TrapType.BOMB_TRAP:
                return trapPrices[0].price;
            case TrapType.TIME_TRAP:
                return trapPrices[1].price;
            case TrapType.FREEZE_TRAP:
                return trapPrices[2].price;
            default:
                return trapPrices[0].price;
        }
    }

    // refund for tower for quarter of the price
    public void refund(TowerType type)
    {
        switch (type)
        {
            case TowerType.MACHINE_GUN:
                money += Mathf.Floor(towerPrices[0].price / 4f);
                break;
            case TowerType.SNIPER:
                money += Mathf.Floor(towerPrices[1].price / 4f);
                break;
            case TowerType.ELECTRIC:
                money += Mathf.Floor(towerPrices[2].price / 4f);
                break;
            default:
                break;
        }
        moneyChanged = true;
    }

    // get Refund Tower Money
    public float getRefundMoney(TowerType type)
    {
        switch (type)
        {
            case TowerType.MACHINE_GUN:
                return Mathf.Floor(towerPrices[0].price / 4f);
            case TowerType.SNIPER:
                return Mathf.Floor(towerPrices[1].price / 4f);
            case TowerType.ELECTRIC:
                return Mathf.Floor(towerPrices[2].price / 4f);
            default:
                return Mathf.Floor(towerPrices[0].price / 4f);
        }
    }

    public void setDifficulty(int difficultyIdx)
    {
        this.difficultyIdx = difficultyIdx;
    }

    // delete scene manager event
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    // load a game based on scene index / level index, also if it has rangkuman idx open that rangkuman
    public void loadGame(int levelIdx, int rangkumanIdx = -1)
    {
        if (!isLoading)
        {
            isLoading = true;
            loadingScreen.gameObject.SetActive(true);
            scenesLoading.Add(SceneManager.UnloadSceneAsync(currentSceneIndex));
            scenesLoading.Add(SceneManager.LoadSceneAsync(levelIdx, LoadSceneMode.Additive));
            StartCoroutine(GetSceneLoadProgress());
            currentSceneIndex = levelIdx;
            selectedSummaryIndex = rangkumanIdx;
        }
    }

    // load progress
    public IEnumerator GetSceneLoadProgress()
    {
        for (int i = 0; i < scenesLoading.Count; i++)
            while (!scenesLoading[i].isDone)
                yield return null;

        isLoading = false;
        loadingScreen.SetActive(false);
    }

    // get user id
    public int getUserId()
    {
        return userId;
    }

    // make sound (for buttons)
    public void makeButtonPressSound()
    {
        audioSource.Stop();
        audioSource.Play();
    }

    // stop main menu music
    public void stopMainMenuMusic()
    {
        mainMenuAudio.stop();
    }

    // start main menu music
    public void startMainMenuMusic()
    {
        if (!mainMenuAudio.isNowPlaying())
            mainMenuAudio.start();
    }

    // password for MySql Database
    public string getMySQLPassword()
    {
        return "scg8sfxxpmu7lq8e";
    }
}
