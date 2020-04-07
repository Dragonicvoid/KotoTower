using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProperty : MonoBehaviour
{
    //property
    [SerializeField] float startMoney = 0f;
    [SerializeField] int maxCharged = 0;
    [SerializeField] int levelIndex = 0;
    [SerializeField] List<TowerPrice> startTowerPrices = null;
    [SerializeField] List<TrapPrice> startTrapPrices = null;
    [SerializeField] int enemyVariation = 0;

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
        GameEvents.current.onKotoTowerDestroyedEnter += OnGameWon;
    }

    // when the game won just reset(for debug)
    void OnGameWon()
    {
        GameManager.instance.loadGame((int)Levels.CHOOSE_LEVEL);
    }

    // delete the event
    private void OnDestroy()
    {
        GameEvents.current.onGameWonEnter -= OnGameWon;
        GameEvents.current.onKotoTowerDestroyedEnter -= OnGameWon;
    }
}
