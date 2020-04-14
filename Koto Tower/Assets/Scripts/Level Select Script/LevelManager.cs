using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // back to main menu
    public void backToMainMenu()
    {
        GameManager.instance.loadGame((int)Levels.MAIN_MENU);
    }

    // go to rangkuman menu
    public void goToRangkuman(int selectedIdx)
    {
        GameManager.instance.loadGame((int)Levels.RANGKUMAN, selectedIdx);
    }
}
