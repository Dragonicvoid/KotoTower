using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseWinController : MonoBehaviour
{
    [SerializeField] Text lostWinText = null;
    [SerializeField] Text timeScoreText = null;
    [SerializeField] Text consecutiveAnswerScoreText = null;
    [SerializeField] Text totalAnsweredQuestionScoreText = null;
    [SerializeField] Text totalScoreText = null;

    // change Text to Lose or Win
    public void changeText(bool isWin, float timeScore = 0f, int consecutiveAnswer = 0)
    {
        //calculate the score based on how the players perform
        float consecutiveScore = consecutiveAnswer * 1000f;
        float totalAnsweredScore = GameManager.instance.totalAnsweredQuestion * 1000f;
        float totalScore = timeScore + consecutiveScore + totalAnsweredScore;

        if (isWin)
            lostWinText.text = "MENANG!"; 
        else
            lostWinText.text = "KALAH!";

        timeScoreText.text = timeScore.ToString();
        consecutiveAnswerScoreText.text = consecutiveAnswer.ToString() + " x " + 1000;
        totalAnsweredQuestionScoreText.text = GameManager.instance.totalAnsweredQuestion.ToString() + " x " + 1000;
        totalScoreText.text = totalScore.ToString();
    }
}
