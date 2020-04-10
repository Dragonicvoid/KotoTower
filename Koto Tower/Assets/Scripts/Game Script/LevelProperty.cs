using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperty : MonoBehaviour
{
    //property
    [SerializeField] float startMoney = 0f;
    [SerializeField] int maxCharged = 0;
    [SerializeField] int levelIndex = 0;
    [SerializeField] List<TowerPrice> startTowerPrices = null;
    [SerializeField] List<TrapPrice> startTrapPrices = null;
    [SerializeField] int enemyVariation = 0;
    [SerializeField] LoseWinController loseWinController = null;

    // On Start change value on GameManager
    private void Awake()
    {
        GameManager.instance.setMoney(startMoney);
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
    }

    // when the game won just reset(for debug)
    void OnGameWon()
    {
        GameManager.instance.isPaused = true;
        loseWinController.changeText(true);
        loseWinController.gameObject.SetActive(true);
    }

    // when the game lost just reset(for debug)
    void OnGameLost()
    {
        GameManager.instance.isPaused = true;
        loseWinController.changeText(false);
        loseWinController.gameObject.SetActive(true);
    }

    // for pausing
    public void pause()
    {
        if(!GameManager.instance.isSelectTower && !GameManager.instance.isSelectTrap && GameManager.instance.currentStatus != GameStatus.SELECTING_TOWER)
            GameManager.instance.isPaused = true;
    }

    // for pausing
    public void unpause()
    {
        GameManager.instance.isPaused = false;
    }

    // For exiting the game
    public void exitLevel()
    {
        GameManager.instance.loadGame((int)Levels.CHOOSE_LEVEL);
    }

    // For replaying the game
    public void playAgain()
    {
        GameManager.instance.loadGame(GameManager.instance.currentSceneIndex);
    }

    // delete the event
    private void OnDestroy()
    {
        GameEvents.current.onGameWonEnter -= OnGameWon;
        GameEvents.current.onKotoTowerDestroyedEnter -= OnGameLost;
    }
}
