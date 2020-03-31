using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPropertyTesting : MonoBehaviour
{
    //property
    [SerializeField] float startMoney = 0f;
    [SerializeField] int maxCharged = 0;
    [SerializeField] int levelIndex = 0;
    [SerializeField] List<TowerPrice> startTowerPrices = null;
    [SerializeField] List<TrapPrice> startTrapPrices = null;

    // On Start change value on GameManager
    private void Awake()
    {
        GameManager.setMoney(startMoney);
        GameManager.setPrices(startTowerPrices, startTrapPrices);

        switch (GameManager.difficultyIdx)
        {
            case 0:
                GameManager.maxCharged = 5;
                maxCharged = 5;
                break;
            case 1:
                GameManager.maxCharged = 8;
                maxCharged = 8;
                break;
            case 2:
                GameManager.maxCharged = 10;
                maxCharged = 10;
                break;
            default:
                GameManager.maxCharged = 5;
                maxCharged = 5;
                break;
        }
    }

    // event initialization
    private void Start()
    {
        GameEventsTesting.current.onGameWonEnter += OnGameWon;
        GameEventsTesting.current.onKotoTowerDestroyedEnter += OnGameWon;
    }

    // when the game won just reset(for debug)
    void OnGameWon()
    {
        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    // delete the event
    private void OnDestroy()
    {
        GameEventsTesting.current.onGameWonEnter -= OnGameWon;
        GameEventsTesting.current.onKotoTowerDestroyedEnter -= OnGameWon;
    }
}
