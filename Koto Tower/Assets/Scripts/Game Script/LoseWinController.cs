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
    [SerializeField] Text errorText = null;
    [SerializeField] Button mainLagiButton = null;
    [SerializeField] Button levelSelectButton = null;
    [SerializeField] GameObject loading = null;
    [SerializeField] GameObject pleaseLogin = null;
    [SerializeField] List<GameObject> leaderboardText = null;

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

        if (GameManager.instance.isTutorial)
            if (isWin) // if the player win the tutorial just make it that player can only go to level select, do the opposite if lose
                mainLagiButton.gameObject.SetActive(false);
            else
                levelSelectButton.gameObject.SetActive(false);

        timeScoreText.text = timeScore.ToString();
        consecutiveAnswerScoreText.text = consecutiveAnswer.ToString() + " x " + 1000;
        totalAnsweredQuestionScoreText.text = GameManager.instance.totalAnsweredQuestion.ToString() + " x " + 1000;
        totalScoreText.text = totalScore.ToString();
    }

    // send data to Leaderboards
    public void sendDataToLeaderboards()
    {
        if (GameManager.instance.hasLogin && !GameManager.instance.isTutorial)
            StartCoroutine(sendLeaderboardsData());
        else if (!GameManager.instance.hasLogin)
            pleaseLogin.SetActive(true);
    }

    // stop the send data
    public void stopSendData()
    {
        StopAllCoroutines();
        mainLagiButton.interactable = true;
        levelSelectButton.interactable = true;
        loading.SetActive(false);
        errorText.text = "Pembaharuan dibatalkan";
        errorText.gameObject.SetActive(true);
    }

    // test to send data
    IEnumerator sendLeaderboardsData()
    {
        yield return null;
        mainLagiButton.interactable = false;
        levelSelectButton.interactable = false;
        errorText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        loading.SetActive(true);
        // if the requirement is valid
        WWWForm form = new WWWForm();
        form.AddField("userId", GameManager.instance.userId);
        form.AddField("levelId", GameManager.instance.currentLevelIndex);
        form.AddField("score", totalScore.ToString());
        form.AddField("mySQLPassword", GameManager.instance.getMySQLPassword());

        // WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/getData/leaderboardsOnWin.php", form);
        // yield return www;

        // if (www.text == null || "".Equals(www.text))
        // {
        //     Debug.Log(www.text);
        //     errorText.text = "Error ditemukan : " + "server sedang mati, silahkan coba beberapa saat lagi";
        //     errorText.gameObject.SetActive(true);
        // } 
        if (true)
        {
            string text = "0\ntest\n1\tOffline1\t2000\n2\tOffline2\t1000\n3\tOffline3\t500";
            changeLeaderboardsData(text);
            errorText.text = "";
            errorText.gameObject.SetActive(false);
        }

        mainLagiButton.interactable = true;
        levelSelectButton.interactable = true;
        loading.SetActive(false);
    }

    // update the leaderboards data
    void changeLeaderboardsData(string text)
    {
        resetLeaderboardField();
        List<string> splitText = new List<string>(text.Split('\n'));

        // stop if already have 5 data or there is no more data, starts at 2 because the first data is 2
        for (int i = 2; i < splitText.Count - 1 && i < 7; i++)
        {
            string[] splitData = splitText[i].Split('\t');
            Text[] texts = leaderboardText[i - 2].GetComponentsInChildren<Text>(true);
            // text 0 is ranking number, 1 is username, 2 is score
            // for splitData 0 is the username, and 1 is the score
            texts[1].text = splitData[0];
            texts[2].text = splitData[1];
            leaderboardText[i - 2].gameObject.SetActive(true);
        }

        // Change player data on the UI
        // string[] splitPlayerData = splitText[1].Split('\t');
        // Text[] playerTexts = leaderboardText[5].GetComponentsInChildren<Text>(true);
        // playerTexts[0].text = splitPlayerData[0];
        // playerTexts[2].text = splitPlayerData[1];
        // leaderboardText[5].gameObject.SetActive(true);
    }

    // reset all leaderboard field
    void resetLeaderboardField()
    {
        foreach (GameObject text in leaderboardText)
            text.SetActive(false);
    }
}
