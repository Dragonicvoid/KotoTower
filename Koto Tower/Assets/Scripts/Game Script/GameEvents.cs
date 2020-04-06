using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    // Action and event caller
    private void Awake()
    {
        current = this;
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

    // Event that plays when the truck is answering question
    public event Action onTruckAnswerEnter;
    public void TruckAnswerEnter()
    {
        onTruckAnswerEnter();
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
    public event Action<TowerBehaviour> onTowerSelected;
    public void TowerSelected(TowerBehaviour obj)
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

    // Event that plays to close the Koto Tower balloon box
    public event Action onCloseKotoTowerBalloonBox;
    public void CloseKotoTowerBalloonBox()
    {
        onCloseKotoTowerBalloonBox();
    }

    // Event that plays to open the Koto Tower balloon box
    public event Action onOpenKotoTowerBalloonBox;
    public void OpenKotoTowerBalloonBox()
    {
        onOpenKotoTowerBalloonBox();
    }

    // Event that plays to close the generator balloon box
    public event Action onCloseGeneratorBalloonBox;
    public void CloseGeneratorBalloonBox()
    {
        onCloseGeneratorBalloonBox();
    }

    // Event that plays to open the generator balloon box
    public event Action onOpenGeneratorBalloonBox;
    public void OpenGeneratorBalloonBox()
    {
        onOpenGeneratorBalloonBox();
    }

    // event that plays when koto tower is on screen
    public event Action onKotoTowerOnScreenEnter;
    public void KotoTowerOnScreenEnter()
    {
        onKotoTowerOnScreenEnter();
    }

    // event that plays when generator is on screen
    public event Action onGeneratorOnScreenEnter;
    public void GeneratorOnScreenEnter()
    {
        onGeneratorOnScreenEnter();
    }

    // event that plays when koto tower is off screen
    public event Action onKotoTowerOffScreenEnter;
    public void KotoTowerOffScreenEnter()
    {
        onKotoTowerOffScreenEnter();
    }

    // event that plays when generator is off screen
    public event Action onGeneratorOffScreenEnter;
    public void GeneratorOffScreenEnter()
    {
        onGeneratorOffScreenEnter();
    }
}
