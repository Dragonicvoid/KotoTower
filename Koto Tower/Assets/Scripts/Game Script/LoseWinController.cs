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

    float totalScore;

    // change Text to Lose or Win
    public void changeText(bool isWin, float timeScore = 0f, int consecutiveAnswer = 0)
    {
        //calculate the score based on how the players perform
        float consecutiveScore = consecutiveAnswer * 1000f;
        float totalAnsweredScore = GameManager.instance.totalAnsweredQuestion * 1000f;
        totalScore = timeScore + consecutiveScore + totalAnsweredScore;

        if (isWin)
            lostWinText.text = "MENANG!"; 
        else
            lostWinText.text = "KALAH!";

        timeScoreText.text = timeScore.ToString();
        consecutiveAnswerScoreText.text = consecutiveAnswer.ToString() + " x " + 1000;
        totalAnsweredQuestionScoreText.text = GameManager.instance.totalAnsweredQuestion.ToString() + " x " + 1000;
        totalScoreText.text = totalScore.ToString();
    }

    // send data to Leaderboards
    public void sendDataToLeaderboards()
    {
        if (GameManager.instance.hasLogin)
            StartCoroutine(sendLeaderboardsData());
    }

    // test to send data
    IEnumerator sendLeaderboardsData()
    {
        yield return null;
        // if the requirement is valid
        WWWForm form = new WWWForm();
        form.AddField("userId", GameManager.instance.userId);
        form.AddField("levelId", GameManager.instance.currentLevelIndex);
        form.AddField("score", totalScore.ToString());

        WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/getData/leaderboardsOnWin.php", form);
        yield return www;

        if (www.text == null || "".Equals(www.text))
            Debug.Log("Error ditemukan : " + "server sedang mati, silahkan coba beberapa saat lagi");
        else if (www.text[0] == '0')
            Debug.Log(www.text);
        else
            Debug.Log("Error ditemukan : " + www.text.Substring(3, www.text.Length - 3));
    }
}
