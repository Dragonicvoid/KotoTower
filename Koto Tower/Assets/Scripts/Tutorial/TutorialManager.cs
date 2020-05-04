using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] List<Tutorial> tutorialList = null;
    [SerializeField] GameObject filter = null;
    [SerializeField] KotoTowerGeneratorTruckButton kotoTowerGeneratorTruckButton = null;
    [SerializeField] Transform tower, trap;
    [SerializeField] GameObject answer, question;
    TruckBehaviour truck;
    int tutorialIdx;
    float timer = 0;
    bool forTutor = false;
    bool changeMoney = true;

    // initialization
    private void Awake()
    {
        truck = GameObject.FindGameObjectWithTag("Truck").GetComponent<TruckBehaviour>();
    }

    // start the tutorial at one
    private void Start()
    {
        tutorialIdx = 0;
        timer = 0;
        foreach (GameObject obj in tutorialList[tutorialIdx].activateGameObject)
            obj.SetActive(true);

        GameManager.instance.isPaused = true;
        filter.SetActive(true);
        tutorialList[tutorialIdx].tutorialPanel.SetActive(true);
        forTutor = false;
    }

    // check situation by tutorial index
    private void Update()
    {
        if (GameManager.instance.isTutorial)
        {
            // tutorial for Koto Tower
            if (tutorialIdx == 1)
            {
                if (!kotoTowerGeneratorTruckButton.getShowKotoTower())
                    timer += Time.deltaTime;
                else
                    timer = 0;

                if (timer >= 0.5f)
                {
                    nextAndOpen();
                    timer = 0;
                }
            }

            // tutorial for generator
            if (tutorialIdx == 3)
            {
                if (!kotoTowerGeneratorTruckButton.getShowGenerator())
                    timer += Time.deltaTime;
                else
                    timer = 0;

                if (timer >= 0.5f)
                {
                    nextAndOpen();
                    timer = 0;
                }
            }

            // tutorial for char charge
            if (tutorialIdx == 5)
            {
                if (QuestionManager.isSendingTruck)
                    timer += Time.deltaTime;

                if (timer >= 1f)
                {
                    nextAndOpen();
                    timer = 0;
                }
            }

            // tutorial for path selection
            if (tutorialIdx == 7)
            {
                if (truck.getTruckStatus() == TruckStatus.WAITING)
                    nextAndOpen();
            }

            // tutorial for answer the right one
            if (tutorialIdx == 9)
            {
                if (GameManager.instance.totalAnsweredQuestion == 1)
                    timer += Time.deltaTime;

                if (timer >= 0.3f)
                {
                    nextAndOpen();
                    timer = 0;
                }
            }

            // tutorial for char charge power
            if (tutorialIdx == 11)
            {
                if (QuestionManager.isSendingTruck)
                    timer += Time.deltaTime;

                if (timer >= 0.3f)
                {
                    nextAndOpen();
                    timer = 0;
                }
            }

            // tutorial for char Tower
            if (tutorialIdx == 13)
            {
                if (GameManager.instance.totalAnsweredQuestion == 2)
                    timer += Time.deltaTime;

                if (timer >= 0.2f)
                {
                    nextAndOpen();
                    timer = 0;
                }
            }

            // tutorial for enemy and Koto Tower
            if (tutorialIdx == 15)
            {
                answer.SetActive(false);
                question.SetActive(false);

                // show this tutorial when in building mode
                tutorialList[tutorialIdx].activateGameObject[0].SetActive(false);
                tutorialList[tutorialIdx].activateGameObject[1].SetActive(false);
                tutorialList[tutorialIdx].activateGameObject[2].SetActive(true);

                // check if the player has spawned 2 towers
                if (tower.childCount > 1 && !forTutor)
                {
                    TowerBehaviour towerBehaveOne = tower.GetChild(0).GetComponent<TowerBehaviour>();
                    TowerBehaviour towerBehaveTwo = tower.GetChild(1).GetComponent<TowerBehaviour>();
                    if (towerBehaveOne.getIsActive() && towerBehaveTwo.getIsActive())
                        forTutor = true;
                }

                if (GameManager.instance.isSelectTower)
                {
                    tutorialList[tutorialIdx].activateGameObject[0].SetActive(true);
                    tutorialList[tutorialIdx].activateGameObject[1].SetActive(true);
                    tutorialList[tutorialIdx].activateGameObject[2].SetActive(false);
                }

                if (forTutor)
                    timer += Time.deltaTime;

                if (timer >= 1f)
                {
                    nextAndOpen();
                    answer.SetActive(true);
                    question.SetActive(true);
                    forTutor = false;
                    timer = 0;
                }
            }

            // tutorial for enemy
            if (tutorialIdx == 17)
            {
                if (GameManager.instance.totalAnsweredQuestion == 3)
                    timer += Time.deltaTime;

                if (timer >= 0.3f)
                {
                    nextAndOpen();
                    timer = 0;
                }
            }

            // tutorial for trap
            if (tutorialIdx == 19)
            {
                if(changeMoney)
                {
                    GameManager.instance.money += 100f;
                    GameManager.instance.moneyChanged = true;
                    changeMoney = false;
                }
                answer.SetActive(false);
                question.SetActive(false);
                tutorialList[tutorialIdx].activateGameObject[2].SetActive(false);
                tutorialList[tutorialIdx].activateGameObject[3].SetActive(false);
                tutorialList[tutorialIdx].activateGameObject[4].SetActive(true);

                // check if the player has spawned a trap
                if (trap.childCount > 0 && !forTutor)
                {
                    TrapsBehaviour trapBehave = trap.GetChild(0).GetComponent<TrapsBehaviour>();
                    if (trapBehave.getIsActive())
                        forTutor = true;
                }
                    
                if (forTutor)
                    timer += Time.deltaTime;

                if (GameManager.instance.isSelectTrap)
                {
                    tutorialList[tutorialIdx].activateGameObject[2].SetActive(true);
                    tutorialList[tutorialIdx].activateGameObject[3].SetActive(true);
                    tutorialList[tutorialIdx].activateGameObject[4].SetActive(false);
                }

                if (timer >= 4f)
                {
                    nextAndOpen();
                    answer.SetActive(true);
                    question.SetActive(true);
                    forTutor = false;
                    timer = 0;
                }
            }

            // tutorial for end tutorial
            if (tutorialIdx == 21)
            {
                timer += Time.deltaTime;

                if(timer > 4f)
                    nextAndWait();
            }
        }
    }

    // next tutorial and open the next one
    public void nextAndOpen()
    {
        StartCoroutine(nextAndOpenEnumerator());
    }
    

    // next tutorial and open the next one
    IEnumerator nextAndOpenEnumerator()
    {
        // close the current panel
        filter.SetActive(false);
        GameManager.instance.isPaused = false;
        if (tutorialList[tutorialIdx].tutorialPanel != null)
            tutorialList[tutorialIdx].tutorialPanel.SetActive(false);

        // open and close necessary obj for tutorial
        tutorialIdx++;
        foreach (GameObject obj in tutorialList[tutorialIdx].activateGameObject)
            obj.SetActive(true);

        foreach (GameObject obj in tutorialList[tutorialIdx].nonActivateGameObject)
            obj.SetActive(false);

        // open the next tutorial panel
        GameManager.instance.isPaused = true;
        filter.SetActive(true);
        if (tutorialList[tutorialIdx].tutorialPanel != null)
            tutorialList[tutorialIdx].tutorialPanel.SetActive(true);

        yield return null;
    }

    // next tutorial but do not open the next one
    public void nextAndWait()
    {
        StartCoroutine(nextAndWaitEnumerator());
    }

    // next tutorial but do not open the next one
    IEnumerator nextAndWaitEnumerator()
    {
        // close the current panel
        filter.SetActive(false);
        GameManager.instance.isPaused = false;
        if (tutorialList[tutorialIdx].tutorialPanel != null)
            tutorialList[tutorialIdx].tutorialPanel.SetActive(false);

        // open and close necessary obj for tutorial
        tutorialIdx++;
        foreach (GameObject obj in tutorialList[tutorialIdx].activateGameObject)
            obj.SetActive(true);

        foreach (GameObject obj in tutorialList[tutorialIdx].nonActivateGameObject)
            obj.SetActive(false);

        yield return null;
    }
}
