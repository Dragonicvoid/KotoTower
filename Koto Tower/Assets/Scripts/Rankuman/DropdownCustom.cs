using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownCustom : MonoBehaviour
{
    [SerializeField] GameObject filter = null;
    [SerializeField] int maxLevel = 10;

    private Dropdown dropdownBab;
    private List<DropdownProperty> dptList;
    private DropdownProperty currSummary;
    private LevelManager levelManager;

    private int currIdx = 0;

    // initialization
    private void Start()
    {
        currIdx = 0;
        dptList = new List<DropdownProperty>();
        dptList.AddRange(gameObject.GetComponentsInChildren<DropdownProperty>(true));
        dropdownBab = gameObject.GetComponentInChildren<Dropdown>();

        // including tutorial
        string json;
        List<string> listOfString = new List<string>();
        AssetManager.current.assetsText.TryGetValue(ASSET_KEY.LEVEL_SELECT_JSON, out json);

        if (json != "")
        {
            LevelSelectData levelSelectData = JsonUtility.FromJson<LevelSelectData>(json);
            int idx = 0;
            levelSelectData.data.ForEach((data) =>
            {
                if (idx > GameManager.instance.saveFile.levelDone) return;
                listOfString.Add(data.header);
                idx++;
            });
        }

        dropdownBab.AddOptions(listOfString);
        currSummary = dptList[0];
        currSummary.Index = currIdx;
        currSummary.gameObject.SetActive(true);
        dropdownBab.value = currIdx;

        levelManager = gameObject.GetComponentInParent<LevelManager>();
    }

    // Go to the next lesson
    public void next()
    {
        if (currSummary.Index <= GameManager.instance.saveFile.levelDone && currSummary.Index < maxLevel)
        {
            currIdx++;
            GameManager.instance.makeButtonPressSound();
            currSummary.Index = currIdx;
            currSummary.gameObject.SetActive(true);
            dropdownBab.value = currIdx;
        }
    }

    // Go back to the previous lesson
    public void prev()
    {
        if (currSummary.Index > 0)
        {
            currIdx--;
            GameManager.instance.makeButtonPressSound();
            currSummary.Index = currIdx;
            currSummary.gameObject.SetActive(true);
            dropdownBab.value = currIdx;
        }
    }

    // change the dropdown 
    public void dropdown()
    {
        GameManager.instance.makeButtonPressSound();
        currIdx = dropdownBab.value;
        currSummary.Index = currIdx;
        currSummary.gameObject.SetActive(true);
    }

    // open the current level for current summary
    public void openLevel()
    {
        filter.gameObject.SetActive(true);
        levelManager.openLevel(currSummary.Index > 10 ? 11 : currSummary.Index);
    }
}
