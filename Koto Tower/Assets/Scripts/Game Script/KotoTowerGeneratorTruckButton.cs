using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotoTowerGeneratorTruckButton : MonoBehaviour
{
    // variables
    KotoTowerBehaviour kotoTower;
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
        // get all needed component
        kotoTower = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<KotoTowerBehaviour>();

        generatorButton = this.gameObject.transform.GetChild(0).gameObject;
        kotoTowerButton = this.gameObject.transform.GetChild(1).gameObject;

        showKotoTower = false;
        showGenerator = true;
        showtruck = false;
        isChanges = true;

        GameEvents.current.onGeneratorOffScreenEnter += GeneratorOffScreen;
        GameEvents.current.onGeneratorOnScreenEnter += GeneratorOnScreen;
        GameEvents.current.onKotoTowerOffScreenEnter += KotoTowerOffScreen;
        GameEvents.current.onKotoTowerOnScreenEnter += KotoTowerOnScreen;
    }

    // check every frame if there is a new state
    private void Update()
    {
        generatorButton.SetActive(showGenerator);
        kotoTowerButton.SetActive(showKotoTower && kotoTower.getIsHit());

        if (GameManager.instance.isSelectTower || GameManager.instance.isSelectTrap || QuestionManager.isSendingTruck)
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
        GameEvents.current.onGeneratorOffScreenEnter -= GeneratorOffScreen;
        GameEvents.current.onGeneratorOnScreenEnter -= GeneratorOnScreen;
        GameEvents.current.onKotoTowerOffScreenEnter -= KotoTowerOffScreen;
        GameEvents.current.onKotoTowerOnScreenEnter -= KotoTowerOnScreen;
    }
}
