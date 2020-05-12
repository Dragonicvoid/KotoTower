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
        GameManager.instance.makeButtonPressSound();
        GameManager.instance.startMainMenuMusic();
        if (GameManager.instance.selectedSummaryIndex == -1)
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
        GameManager.instance.stopMainMenuMusic();
        GameManager.instance.makeButtonPressSound();
        GameManager.instance.loadGame((int)Levels.RANGKUMAN, selectedIdx);
    }

    // go to tutorial
    public void goToTutorial()
    {
        GameManager.instance.makeButtonPressSound();
        GameManager.instance.isTutorial = true;
        GameManager.instance.isPractice = false;
        GameManager.instance.stopMainMenuMusic();
        GameManager.instance.currentLevelIndex = 0;
        GameManager.instance.setDifficulty(0);
        GameManager.instance.loadGame((int)Levels.TUTORIAL);
    }

    // open level at index
    public void openLevel(int idx)
    {
        if (!isOpenLevel)
        {
            GameManager.instance.makeButtonPressSound();
            isOpenLevel = true;
            currOpenLevel = levels[idx];
            filter.gameObject.SetActive(true);
            currOpenLevel.gameObject.SetActive(true);
        }
    }

    // close the curently open level
    public void closeCurr()
    {
        if (isOpenLevel)
        {
            GameManager.instance.makeButtonPressSound();
            isOpenLevel = false;
            currOpenLevel.gameObject.SetActive(false);
            filter.gameObject.SetActive(false);
            currOpenLevel = null;
        }
    }
}
