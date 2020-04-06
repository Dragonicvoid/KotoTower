using System.Collections;
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
    // singleton
    public static GameManager instance;

    // Level selection attribute
    public int currentSceneIndex;

    // attribute for game
    public bool isSelectTrap;
    public short selectedTrap;
    public bool isDoneMakingTrapLines;
    public bool isPressedButtonTrap;

    public bool isSelectTower;
    public short selectedTower;
    public bool isDoneMakingTowerLines;
    public bool isPressedButtonTower;

    public int enemyVariation;

    // attribute for question and answer
    public bool isNewQuestion;

    public GameStatus currentStatus;

    public float money;
    public bool moneyChanged;

    public List<TowerPrice> towerPrices;
    public List<TrapPrice> trapPrices;

    public int totalAnsweredQuestion;
    public int maxCharged;

    public Camera mainCamera;

    // attribute for level selection
    public int difficultyIdx;

    private void Awake()
    {
        instance = this;
        SceneManager.LoadSceneAsync((int)Levels.MAIN_MENU, LoadSceneMode.Additive);
        currentSceneIndex = (int)Levels.MAIN_MENU;
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
        resetOnPlay();
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

        moneyChanged = false;

        totalAnsweredQuestion = 0;
        enemyVariation = 0;
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

    public void setDifficulty(int difficultyIdx)
    {
        this.difficultyIdx = difficultyIdx;
    }

    // delete scene manager event
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public void loadGame(int levelIdx)
    {
        Debug.Log(currentSceneIndex + " -> " + levelIdx);
        SceneManager.UnloadSceneAsync(currentSceneIndex);
        SceneManager.LoadSceneAsync(levelIdx, LoadSceneMode.Additive);
        currentSceneIndex = levelIdx;
    }
}
