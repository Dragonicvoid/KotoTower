using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorBehaviour : MonoBehaviour
{
    // Question and Answer property for testing (change the text if the answer is correct or wrong)
    [SerializeField] QuestionManager questionManager = null;

    // timer for how long the text showing the player if the answer is correct or not
    float waitTimer = 3f;

    // the actual variable to hold the timer
    float timer;
    // the flag to start timer
    bool isStartTiming;

    // Baloon Text
    Text questionText;

    // Generator and koto Tower click
    ClickOnGenerator generatorClick;
    ClickOnKotoTower kotoTowerClick;

    // Initialization
    private void Start()
    {
        questionText = this.gameObject.GetComponentInChildren<Text>();
        generatorClick = this.gameObject.GetComponent<ClickOnGenerator>();
        kotoTowerClick = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<ClickOnKotoTower>();
        isStartTiming = false;
        timer = 0;
    }

    // Countdown until timer reach waitTimer to give nextQuestion
    private void Update()
    {
        if(!GameManager.instance.isPaused)
            generatorBehave();
    }

    void generatorBehave()
    {
        if (isStartTiming)
            timer += Time.deltaTime;

        if (timer >= waitTimer)
        {
            // Reset Timer
            timer = 0;
            isStartTiming = false;

            if (GameManager.instance.totalAnsweredQuestion < GameManager.instance.maxCharged) // Change the question
                GameEvents.current.TruckDestroyedEnter(false);

            generatorClick.StartCoroutine(generatorClick.openGenerator());
        }
    }

    // Check if the answer is correct or not then change the question text to tell player if its right or wrong, and update the gameManager
    public void checkAnswer(Answer answer)
    {
        generatorClick.StartCoroutine(generatorClick.openGenerator());
        if (answer.isRightAnswer)
        {
            // if its not practice ignore the total answered question
            if (!GameManager.instance.isPractice)
            {
                GameManager.instance.totalAnsweredQuestion++;
                GameManager.instance.scoreboard.consecutiveAnswer++;
            }
            
            // if Player answer maxCharged amount of answers
            if (GameManager.instance.totalAnsweredQuestion >= GameManager.instance.maxCharged)
            {
                kotoTowerClick.StartCoroutine(kotoTowerClick.closeKotoTower());
                questionText.text = "MENANG!!!";
                GameEvents.current.GameWonEnter();
            }
            else
            {
                GameManager.instance.isChangedDifficulty = true;
                GameManager.instance.money += 20 * GameManager.instance.totalAnsweredQuestion;
                GameManager.instance.moneyChanged = true;
                questionText.text = "JAWABAN BENAR!!!";
                isStartTiming = true; // start the timer
            }
        }
        else
        {
            GameManager.instance.scoreboard.consecutiveAnswer = 0;
            questionText.text = "JAWABAN SALAH!!!";
            isStartTiming = true; // start the timer
            GameManager.instance.isChangedEnemyGroupSize = true;
        }
        
        // Tell question manager that the truck has arrived and charged the generator
        kotoTowerClick.StartCoroutine(kotoTowerClick.closeKotoTower());
    }
}
