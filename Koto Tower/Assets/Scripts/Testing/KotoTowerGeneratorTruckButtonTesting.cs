using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotoTowerGeneratorTruckButtonTesting : MonoBehaviour
{
    GameObject generatorButton;
    GameObject kotoTowerButton;
    GameObject truckButton;
    bool showKotoTower;
    bool showGenerator;
    bool showtruck;
    bool isChanges;

    // initialize event
    private void Start()
    {
        generatorButton = this.gameObject.transform.GetChild(0).gameObject;
        kotoTowerButton = this.gameObject.transform.GetChild(1).gameObject;

        Debug.Log(generatorButton.name);
        Debug.Log(kotoTowerButton.name);

        showKotoTower = false;
        showGenerator = true;
        showtruck = false;
        isChanges = true;

        GameEventsTesting.current.onGeneratorOffScreenEnter += GeneratorOffScreen;
        GameEventsTesting.current.onGeneratorOnScreenEnter += GeneratorOnScreen;
        GameEventsTesting.current.onKotoTowerOffScreenEnter += KotoTowerOffScreen;
        GameEventsTesting.current.onKotoTowerOnScreenEnter += KotoTowerOnScreen;
    }

    // check every frame if there is a new state
    private void Update()
    {
        generatorButton.SetActive(showGenerator);
        kotoTowerButton.SetActive(showKotoTower);

        if (GameManager.instance.isSelectTower || GameManager.instance.isSelectTrap || QuestionManagerTesting.isSendingTruck)
        {
            generatorButton.SetActive(false);
            kotoTowerButton.SetActive(false);
        }
    }

    //when koto tower is on screen
    void KotoTowerOnScreen()
    {
        showKotoTower = false;
        isChanges = true;
    }

    //when koto tower is off screen
    void KotoTowerOffScreen()
    {
        showKotoTower = true;
        isChanges = true;
    }

    //when generator is on screen
    void GeneratorOnScreen()
    {
        showGenerator = false;
        isChanges = true;
    }

    //when generator is off screen
    void GeneratorOffScreen()
    {
        showGenerator = true;
        isChanges = true;
    }

    //delete the event
    private void OnDestroy()
    {
        GameEventsTesting.current.onGeneratorOffScreenEnter -= GeneratorOffScreen;
        GameEventsTesting.current.onGeneratorOnScreenEnter -= GeneratorOnScreen;
        GameEventsTesting.current.onKotoTowerOffScreenEnter -= KotoTowerOffScreen;
        GameEventsTesting.current.onKotoTowerOnScreenEnter -= KotoTowerOnScreen;
    }
}
