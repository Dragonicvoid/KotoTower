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
}
