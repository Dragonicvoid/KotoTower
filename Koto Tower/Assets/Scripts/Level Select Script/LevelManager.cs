using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // filter
    [SerializeField] GameObject filter = null;
    // all list of level select
    List<LevelSelect> levels;
    LevelSelect currOpenLevel;
    bool isOpenLevel;

    // initialization
    private void Start()
    {
        isOpenLevel = false;
        currOpenLevel = null;
        levels = new List<LevelSelect>(this.gameObject.GetComponentsInChildren<LevelSelect>(true));
    }

    // back to main menu
    public void backToMainMenu()
    {
        if(GameManager.instance.selectedSummaryIndex == -1)
            GameManager.instance.loadGame((int)Levels.MAIN_MENU);
        else
        {
            GameManager.instance.selectedSummaryIndex = -1;
            GameManager.instance.loadGame((int)Levels.CHOOSE_LEVEL);
        }
    }

    // go to rangkuman menu
    public void goToRangkuman(int selectedIdx)
    {
        GameManager.instance.loadGame((int)Levels.RANGKUMAN, selectedIdx);
    }

    // open level at index
    public void openLevel(int idx)
    {
        if (!isOpenLevel)
        {
            isOpenLevel = true;
            currOpenLevel = levels[idx];
            Debug.Log(currOpenLevel);
            filter.gameObject.SetActive(true);
            currOpenLevel.gameObject.SetActive(true);
        }
    }

    // close the curently open level
    public void closeCurr()
    {
        if (isOpenLevel)
        {
            isOpenLevel = false;
            currOpenLevel.gameObject.SetActive(false);
            filter.gameObject.SetActive(false);
            currOpenLevel = null;
        }
    }
}
