using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsTesting : MonoBehaviour
{
    public static GameEventsTesting current;

    // Action and event caller
    private void Awake()
    {
        current = this;
    }

    // Event that plays when Koto Tower or Generator is off screen
    // 0 is for Koto Tower and 1 is for Generator
    public event Action<int> onObjectOffScreenEnter;
    public void ObjectOffScreenEnter(int id)
    {
        if (onObjectOffScreenEnter != null)
        {
            onObjectOffScreenEnter(id);
        }
    }

    // Event that plays when Koto Tower or Generator is off screen
    // 0 is for Koto Tower and 1 is for Generator
    public event Action<int> onObjectOnScreenEnter;
    public void ObjectOnScreenEnter(int id)
    {
        if (onObjectOnScreenEnter != null)
        {
            onObjectOnScreenEnter(id);
        }
    }

    // Event that plays when the truck is destroyed
    public event Action<bool> onTruckDestroyedEnter;
    public void TruckDestroyedEnter(bool isExplode)
    {
        onTruckDestroyedEnter(isExplode);
    }

    // Event that plays when the truck is sent
    public event Action onTruckSentEnter;
    public void TruckSentEnter()
    {
        onTruckSentEnter();
    }

    // Event that plays when the player won
    public event Action onGameWonEnter;
    public void GameWonEnter()
    {
        onGameWonEnter();
    }

    // Event that plays when the Koto Tower is destroyed
    public event Action onKotoTowerDestroyedEnter;
    public void KotoTowerDestroyed()
    {
        onKotoTowerDestroyedEnter();
    }

    // Event that plays when the tower is selected
    public event Action<TowerBehaviourTesting> onTowerSelected;
    public void TowerSelected(TowerBehaviourTesting obj)
    {
        onTowerSelected(obj);
    }

    // Event that plays when the tower is selected
    public event Action onTowerUnselected;
    public void TowerUnselected()
    {
        onTowerUnselected();
    }

    // Event that plays when the tower is build
    public event Action onTowerOrTrapBuild;
    public void TowerOrTrapBuild()
    {
        onTowerOrTrapBuild();
    }
}
