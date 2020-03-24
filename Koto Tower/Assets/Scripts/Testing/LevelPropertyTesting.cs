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
        GameManager.maxCharged = 3;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // delete the event
    private void OnDestroy()
    {
        GameEventsTesting.current.onGameWonEnter -= OnGameWon;
        GameEventsTesting.current.onKotoTowerDestroyedEnter -= OnGameWon;
    }
}
