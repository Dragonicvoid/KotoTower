using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperty : MonoBehaviour
{
    //property
    [SerializeField] float startMoney = 0f;
    [SerializeField] int maxCharged = 0;
    [SerializeField] int levelIndex = 0;
    [SerializeField] int enemyVariation = 0;
    [SerializeField] float timeEfficient = 0f; // minimal time to get maximum time score
    [SerializeField] LoseWinController loseWinController = null;
    [SerializeField] GameObject exitConfirmationBox = null;

    // song
    AudioSource audioSource;

    // On Start change value on GameManager
    private void Awake()
    {
        int currLvl = GameManager.instance.currentLevelIndex;

        string json;
        List<string> listOfString = new List<string>();
        AssetManager.current.assetsText.TryGetValue(ASSET_KEY.LEVEL_CONFIG_JSON, out json);

        LevelConfig conf = null;
        List<TowerPrice> startTowerPrices = new List<TowerPrice>();
        List<TrapPrice> startTrapPrices = new List<TrapPrice>();
        if (json != "")
        {
            LevelData levelData = JsonUtility.FromJson<LevelData>(json);
            conf = levelData.data.Find((c) => { return c.levelId == currLvl; });
            conf.towers.ForEach((c) =>
            {
                startTowerPrices.Add(new TowerPrice { price = c.price, type = (TowerType)c.id });
            });
            conf.traps.ForEach((c) =>
            {
                startTrapPrices.Add(new TrapPrice { price = c.price, type = (TrapType)c.id });
            });
            enemyVariation = conf.enemyVariance;
            timeEfficient = conf.timeEfficient;
        }

        if (conf == null)
        {
            exitLevel();
            return;
        }

        GameManager.instance.setMoney(conf.startingCoin);
        GameManager.instance.setPrices(startTowerPrices, startTrapPrices);
        GameManager.instance.setEnemyVariation(enemyVariation);

        switch (GameManager.instance.difficultyIdx)
        {
            case 0:
                GameManager.instance.maxCharged = 5;
                maxCharged = 5;
                break;
            case 1:
                GameManager.instance.maxCharged = 8;
                maxCharged = 8;
                break;
            case 2:
                GameManager.instance.maxCharged = 10;
                maxCharged = 10;
                break;
            default:
                GameManager.instance.maxCharged = 5;
                maxCharged = 5;
                break;
        }
    }

    // event initialization
    private void Start()
    {
        GameEvents.current.onGameWonEnter += OnGameWon;
        GameEvents.current.onKotoTowerDestroyedEnter += OnGameLost;

        if (!GameManager.instance.isPractice && !GameManager.instance.isTutorial)
            GameManager.instance.gameStart = false;
        else
            GameManager.instance.gameStart = true;
        audioSource = this.gameObject.GetComponent<AudioSource>();
        if (GameManager.instance.isPractice)
            audioSource.Stop();
    }

    // update the timer as long the game is not paused and not reached 15 minutes
    private void Update()
    {
        if (!GameManager.instance.isPaused && GameManager.instance.scoreboard.time <= 900 && GameManager.instance.gameStart)
            GameManager.instance.scoreboard.time += Time.deltaTime;
    }

    // when the game won calculate the score and change text to win
    void OnGameWon()
    {
        GameManager.instance.isPaused = true;

        // save game when game won, and update unlockable level when player was playing hard
        if (GameManager.instance.saveFile.levelDone <= GameManager.instance.currentLevelIndex && GameManager.instance.difficultyIdx == 2)
            GameManager.instance.saveFile.levelDone = GameManager.instance.currentLevelIndex + 1;

        SaveManager.instance.saveAndUpdate();

        loseWinController.changeText(true, calculateTimeScore(GameManager.instance.scoreboard.time), GameManager.instance.scoreboard.consecutiveAnswer);
        loseWinController.gameObject.SetActive(true);
        loseWinController.sendDataToLeaderboards();
    }

    // when the game lose just change text to lose
    void OnGameLost()
    {
        GameManager.instance.isPaused = true;
        loseWinController.changeText(false);
        loseWinController.gameObject.SetActive(true);
        loseWinController.sendDataToLeaderboards();
    }

    // for pausing
    public void pause()
    {
        if (!GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && GameManager.instance.currentStatus != GameStatus.SELECTING_TOWER)
        {
            GameManager.instance.isPaused = true;
            exitConfirmationBox.SetActive(true);
        }
    }

    // for pausing
    public void unpause()
    {
        GameManager.instance.isPaused = false;
        exitConfirmationBox.SetActive(false);
    }

    // For exiting the game
    public void exitLevel()
    {
        GameManager.instance.startMainMenuMusic();
        GameManager.instance.loadGame((int)Levels.CHOOSE_LEVEL);
    }

    // For replaying the game
    public void playAgain()
    {
        GameManager.instance.loadGame(GameManager.instance.currentSceneIndex);
    }

    // calculate the score for time score
    private float calculateTimeScore(float time)
    {
        int valueBasedDiff = 0;
        switch (GameManager.instance.difficultyIdx)
        {
            case 0:
                valueBasedDiff = 10000;
                break;
            case 1:
                valueBasedDiff = 15000;
                break;
            case 2:
                valueBasedDiff = 20000;
                break;
            default:
                valueBasedDiff = 10000;
                break;
        }

        if (time <= timeEfficient)
            return valueBasedDiff;
        else
            return Mathf.Floor(Mathf.Pow((1 - deltaTime(time - timeEfficient, 900 - timeEfficient)), 2) * valueBasedDiff);
    }

    // calculate the delta time
    private float deltaTime(float currTime, float divTime)
    {
        if (currTime >= divTime)
            return 1f;
        else
            return currTime / divTime;
    }

    // delete the event
    private void OnDestroy()
    {
        GameEvents.current.onGameWonEnter -= OnGameWon;
        GameEvents.current.onKotoTowerDestroyedEnter -= OnGameLost;
    }
}
