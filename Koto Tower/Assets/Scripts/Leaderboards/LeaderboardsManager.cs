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
    [SerializeField] Text peringkatPemain = null;
    [SerializeField] Text namaPemain = null;
    [SerializeField] Text skorPemain = null;
    [SerializeField] Text belumMenyelesaikan = null;
    [SerializeField] int maxLevel = 0;

    List<LeaderboardsRow> rowList;
    GameObject rowLeaderboards;
    int currDropDownIdx;
    int maxDropDownIdx;

    // Start is called before the first frame update
    void Start()
    {
        rowList = new List<LeaderboardsRow>(this.gameObject.GetComponentsInChildren<LeaderboardsRow>(true));
        rowLeaderboards = rowList[0].gameObject;

        if(GameManager.instance.saveFile != null)
        {
            // add option based on level done by player
            List<string> options = new List<string>();
            for (int i = 0; i < GameManager.instance.saveFile.levelDone && i < maxLevel; i++)
                options.Add("Level " + (i + 1));

            if(GameManager.instance.saveFile.levelDone == 0)
                options.Add("Level " + 1);

            maxDropDownIdx = options.Count - 1;
            dropdown.AddOptions(options);

            currDropDownIdx = 0;
            dropdown.value = currDropDownIdx;
            getLeaderboardsRow(currDropDownIdx + 1);
        }
        else
        {
            clearRows();
            setErrorText("Mulai game baru untuk dapat melihat papan peringkat!");
            belumMenyelesaikan.gameObject.SetActive(true);
            belumMenyelesaikan.text = "";
        }   
    }

    // clear all leaderboards row even the error and player leaderboards
    void clearRows()
    {
        errorText.text = "";
        errorText.gameObject.SetActive(false);

        peringkatPemain.gameObject.SetActive(false);
        peringkatPemain.text = "";

        namaPemain.gameObject.SetActive(false);
        namaPemain.text = "";

        skorPemain.gameObject.SetActive(false);
        skorPemain.text = "";

        belumMenyelesaikan.gameObject.SetActive(false);
        belumMenyelesaikan.text = "";

        foreach (LeaderboardsRow row in rowList)
            row.gameObject.SetActive(false);
    }

    // next level leaderboards
    public void next()
    {
        if (currDropDownIdx < maxDropDownIdx)
        {
            GameManager.instance.makeButtonPressSound();
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
        if (currDropDownIdx > 0)
        {
            GameManager.instance.makeButtonPressSound();
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
        GameManager.instance.makeButtonPressSound();
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
        form.AddField("userId", GameManager.instance.userId);
        form.AddField("mySQLPassword", GameManager.instance.getMySQLPassword());
        // WWW www = new WWW("https://tranquil-fjord-77396.herokuapp.com/getData/getLeaderboardsDataMainMenu.php", form);
        // yield return www;

        // if (www.text == null || "".Equals(www.text))
        //     setErrorText("Error ditemukan : " + "server sedang mati, silahkan coba beberapa saat lagi");
        if (true) {
            string text = "0\n1\tMain Player\t25000\n1\tMain Player\t25000\n2\tPlayer 1\t20000\n3\tPlayer 2\t15000\n4\tPlayer 3\t10000";
            setRowData(text);
        }
        // else
            // setErrorText("Error ditemukan : " + www.text.Substring(3, www.text.Length - 3));

        loading.SetActive(false);
    }

    // set error text and deactivate pemain's score
    void setErrorText(string error)
    {
        peringkatPemain.gameObject.SetActive(false);
        peringkatPemain.text = "";

        namaPemain.gameObject.SetActive(false);
        namaPemain.text = "";

        skorPemain.gameObject.SetActive(false);
        skorPemain.text = "";

        errorText.text = error;
        errorText.gameObject.SetActive(true);
    }

    // set row data and dsiable error text
    void setRowData(string text)
    {
        errorText.gameObject.SetActive(false);
        errorText.text = "";

        prevButton.interactable = false;
        nextButton.interactable = false;
        dropdown.interactable = false;
        // separate the data
        List<string> rowText = new List<string>(text.Split('\n'));
        // set curr index for collected data, starts at 1
        int currIdx = 1;

        // set player's data
        List<string> colTextForPemain = new List<string>(rowText[currIdx].Split('\t'));
        if(colTextForPemain.Count > 1)
        {
            peringkatPemain.gameObject.SetActive(true);
            peringkatPemain.text = colTextForPemain[0];

            namaPemain.gameObject.SetActive(true);
            namaPemain.text = colTextForPemain[1];

            skorPemain.gameObject.SetActive(true);
            skorPemain.text = colTextForPemain[2];
        }
        else
        {
            belumMenyelesaikan.gameObject.SetActive(true);
            belumMenyelesaikan.text = rowText[currIdx];
        }
        currIdx++;

        // set data to already created row
        for (int i = 0; i < rowList.Count && currIdx < rowText.Count - 1; i++)
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
