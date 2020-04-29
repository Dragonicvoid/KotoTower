using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardsManager : MonoBehaviour
{
    [SerializeField] GameObject loading = null;
    [SerializeField] Dropdown dropdown = null;
    [SerializeField] Button prevButton = null;
    [SerializeField] Button nextButton = null;
    [SerializeField] Text errorText = null;

    List<LeaderboardsRow> rowList;
    GameObject rowLeaderboards;
    int currDropDownIdx;
    int maxDropDownIdx;

    // Start is called before the first frame update
    void Start()
    {
        rowList = new List<LeaderboardsRow>(this.gameObject.GetComponentsInChildren<LeaderboardsRow>(true));
        rowLeaderboards = rowList[0].gameObject;

        // add option based on level done by player
        List<string> options = new List<string>();
        for (int i = 0; i < GameManager.instance.saveFile.levelDone; i++)
            options.Add("Level " + (i + 1));
        maxDropDownIdx = options.Count - 1;
        dropdown.AddOptions(options);

        currDropDownIdx = 0;
        dropdown.value = currDropDownIdx;
        getLeaderboardsRow(currDropDownIdx + 1);
    }

    // clear all leaderboards row
    void clearRows()
    {
        errorText.text = "";
        errorText.gameObject.SetActive(false);
        foreach (LeaderboardsRow row in rowList)
            row.gameObject.SetActive(false);
    }

    // next level leaderboards
    public void next()
    {
        if (currDropDownIdx != maxDropDownIdx)
        {
            StopAllCoroutines();
            clearRows();
            currDropDownIdx++;
            dropdown.value = currDropDownIdx;
            getLeaderboardsRow(currDropDownIdx + 1);
        }
    }

    // prev level leaderboards
    public void prev()
    {
        if (currDropDownIdx != 0)
        {
            StopAllCoroutines();
            clearRows();
            currDropDownIdx--;
            dropdown.value = currDropDownIdx;
            getLeaderboardsRow(currDropDownIdx + 1);
        }
    }

    // get data based on 
    public void getDataBasedOnValueChange()
    {
        StopAllCoroutines();
        clearRows();
        currDropDownIdx = dropdown.value;
        getLeaderboardsRow(currDropDownIdx + 1);
    }

    // get leadboards data at level
    public void getLeaderboardsRow(int levelIdx)
    {
        clearRows();
        StartCoroutine(getLeaderboardsData(levelIdx));
    }

    // enumerator
    IEnumerator getLeaderboardsData(int levelIdx)
    {
        yield return null;
        // if the requirement is valid
        loading.SetActive(true);
        WWWForm form = new WWWForm();
        form.AddField("levelIdx", levelIdx);
        WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/getData/getLeaderboardsDataMainMenu.php", form);
        yield return www;

        if (www.text == null || "".Equals(www.text))
            setErrorText("Error ditemukan : " + "server sedang mati, silahkan coba beberapa saat lagi");
        else if (www.text[0] == '0')
            setRowData(www.text);
        else
            setErrorText("Error ditemukan : " + www.text.Substring(3, www.text.Length - 3));

        loading.SetActive(false);
    }

    // set error text
    void setErrorText(string error)
    {
        errorText.text = error;
        errorText.gameObject.SetActive(true);
    }

    // set row data
    void setRowData(string text)
    {
        prevButton.interactable = false;
        nextButton.interactable = false;
        dropdown.interactable = false;
        // separate the data
        List<string> rowText = new List<string>(text.Split('\n'));
        // set curr index for collected data, starts at 1
        int currIdx = 1;

        // set data to already created row
        for(int i = 0; i < rowList.Count && currIdx < rowText.Count - 1; i++)
        {
            LeaderboardsRow currRow = rowList[i];
            string[] colText = rowText[currIdx].Split('\t');
            // 0 : peringkat, 1 : nama, 2 : score, 3 : tanggal
            currRow.changeText(colText[0] ,colText[1], colText[2], colText[3]);
            currRow.gameObject.SetActive(true);
            currIdx++;
        }

        // add new row if it ran out of existing row
        while (currIdx < rowText.Count - 1)
        {
            GameObject currObj = Instantiate(rowLeaderboards, this.gameObject.transform);
            LeaderboardsRow currRow = currObj.GetComponent<LeaderboardsRow>();
            rowList.Add(currRow);

            string[] colText = rowText[currIdx].Split('\t');
            // 0 : peringkat, 1 : nama, 2 : score, 3 : tanggal
            currRow.changeText(colText[0], colText[1], colText[2], colText[3]);
            currRow.gameObject.SetActive(true);
            currIdx++;
        }

        prevButton.interactable = true;
        nextButton.interactable = true;
        dropdown.interactable = true;
    }

    // back to main menu
    public void backToMainMenu()
    {
        StopAllCoroutines();
        GameManager.instance.loadGame((int)Levels.MAIN_MENU);
    }
}
