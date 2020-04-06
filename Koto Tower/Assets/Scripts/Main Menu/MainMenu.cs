using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // go to level select
    public void goToLevelSelect()
    {
        GameManager.instance.loadGame((int)Levels.CHOOSE_LEVEL);
    }
}
