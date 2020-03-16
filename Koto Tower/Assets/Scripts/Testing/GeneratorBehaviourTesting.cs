using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorBehaviourTesting : MonoBehaviour
{
    // Question and Answer property for testing (change the text if the answer is correct or wrong)
    [SerializeField] QuestionManagerTesting questionManager = null;

    // how many time it shoud be charge before player win the game
    [SerializeField] int maxCharged = 3;
    // timer for how long the text showing the player if the answer is correct or not
    [SerializeField] float waitTimer = 5f;
    int answeredQuestion;

    // the actual variable to hold the timer
    float timer;
    // the flag to start timer
    bool isStartTiming;

    // Baloon Text
    Text questionText;

    // Initialization
    private void Start()
    {
        questionText = this.gameObject.GetComponentInChildren<Text>();
        isStartTiming = false;
        answeredQuestion = 0;
        timer = 0;
    }

    // Countdown until timer reach waitTimer to give nextQuestion
    private void Update()
    {
        if(isStartTiming)
            timer += Time.deltaTime;

        if (timer >= waitTimer)
        {
            // Tell question manager that the truck has arrived and charged the generator
            QuestionManagerTesting.isSendingTruck = false;
            // Reset Timer
            timer = 0;
            isStartTiming = false;

            // if Player answer maxCharged amount of answers
            if (answeredQuestion >= maxCharged)
                questionText.text = "MENANG!!!";
            else // Change the question
                questionManager.getNewQuestion();
        }
    }

    // Check if the answer is correct or not then change the question text to tell player if its right or wrong
    public void checkAnswer(AnswerTesting answer)
    {
        if (answer.isRightAnswer)
        {
            answeredQuestion++;
            questionText.text = "JAWABAN BENAR!!!";
        }
        else
            questionText.text = "JAWABAN SALAH!!!";

        // Start timer
        isStartTiming = true;
    }
}
