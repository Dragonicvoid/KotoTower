using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorBehaviourTesting : MonoBehaviour
{
    // Question and Answer property for testing (change the text if the answer is correct or wrong)
    [SerializeField] QuestionManagerTesting questionManager = null;

    // timer for how long the text showing the player if the answer is correct or not
    [SerializeField] float waitTimer = 5f;

    // the actual variable to hold the timer
    float timer;
    // the flag to start timer
    bool isStartTiming;

    // Baloon Text
    Text questionText;

    // Generator and koto Tower click
    ClickOnGeneratorTesting generatorClick;
    ClickOnKotoTowerTesting kotoTowerClick;

    // Initialization
    private void Start()
    {
        questionText = this.gameObject.GetComponentInChildren<Text>();
        generatorClick = this.gameObject.GetComponent<ClickOnGeneratorTesting>();
        kotoTowerClick = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<ClickOnKotoTowerTesting>();
        isStartTiming = false;
        timer = 0;
    }

    // Countdown until timer reach waitTimer to give nextQuestion
    private void Update()
    {
        if(isStartTiming)
            timer += Time.deltaTime;

        if (timer >= waitTimer)
        {
            // Reset Timer
            timer = 0;
            isStartTiming = false;

            // if Player answer maxCharged amount of answers
            if (GameManager.totalAnsweredQuestion >= GameManager.maxCharged)
            {
                kotoTowerClick.StartCoroutine(kotoTowerClick.closeKotoTower());
                questionText.text = "MENANG!!!";
            }
            else // Change the question
                questionManager.getNewQuestion();

            generatorClick.StartCoroutine(generatorClick.openGenerator());
        }
    }

    // Check if the answer is correct or not then change the question text to tell player if its right or wrong
    public void checkAnswer(AnswerTesting answer)
    {
        generatorClick.StartCoroutine(generatorClick.openGenerator());
        if (answer.isRightAnswer)
        {
            GameManager.totalAnsweredQuestion++;
            questionText.text = "JAWABAN BENAR!!!";
        }
        else
            questionText.text = "JAWABAN SALAH!!!";

        // Start timer
        isStartTiming = true;
        // Tell question manager that the truck has arrived and charged the generator
        QuestionManagerTesting.isSendingTruck = false;
        kotoTowerClick.StartCoroutine(kotoTowerClick.closeKotoTower());
    }
}
