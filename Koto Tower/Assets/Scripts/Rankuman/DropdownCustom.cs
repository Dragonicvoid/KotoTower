using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownCustom: MonoBehaviour
{
    [SerializeField] GameObject filter = null;
    [SerializeField] int maxLevel = 2;

    private Dropdown dropdownBab;
    private List<DropdownProperty> dptList;
    private List<LevelSelect> levels;
    private DropdownProperty currSummary;

    // initialization
    private void Start()
    {
        dptList = new List<DropdownProperty>();
        levels = new List<LevelSelect>();
        dptList.AddRange(this.gameObject.GetComponentsInChildren<DropdownProperty>(true));
        levels.AddRange(this.gameObject.transform.parent.GetComponentsInChildren<LevelSelect>(true));
        dropdownBab = this.gameObject.GetComponentInChildren<Dropdown>();

        List<string> listOfString = new List<string>();
        for (int i = 0; i < GameManager.instance.saveFile.levelDone && i < maxLevel; i++)
            listOfString.Add(dptList[i].namaBab);

        dropdownBab.AddOptions(listOfString);
        currSummary = dptList[GameManager.instance.selectedSummaryIndex != -1 ? GameManager.instance.selectedSummaryIndex : 0];
        currSummary.gameObject.SetActive(true);
        dropdownBab.value = currSummary.index;
    }

    // Go to the next lesson
    public void next()
    {
        if(currSummary.index != GameManager.instance.saveFile.levelDone - 1 && currSummary.index != maxLevel - 1)
        {
            currSummary.gameObject.SetActive(false);
            currSummary = dptList[dropdownBab.value + 1];
            currSummary.gameObject.SetActive(true);
            dropdownBab.value++;
        }
    }

    // Go back to the previous lesson
    public void prev()
    {
        if (currSummary.index != 0)
        {
            currSummary.gameObject.SetActive(false);
            currSummary = dptList[dropdownBab.value - 1];
            currSummary.gameObject.SetActive(true);
            dropdownBab.value--;
        }
    }

    // change the dropdown 
    public void dropdown()
    {
        currSummary.gameObject.SetActive(false);
        currSummary = dptList[dropdownBab.value];
        currSummary.gameObject.SetActive(true);
    }

    // open the current level for current summary
    public void openLevel()
    {
        levels[currSummary.index > 1 ? 2 : currSummary.index].gameObject.SetActive(true);
        filter.gameObject.SetActive(true);
    }
}
