using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLevelBasedOnSaveFile : MonoBehaviour
{
    // Maximum levels
    [SerializeField] int maxLevel = 2;
    // list of levels
    List<LevelUi> levelsUi;

    // get all level
    private void Start()
    {
        levelsUi = new List<LevelUi>();
        levelsUi.AddRange(GetComponentsInChildren<LevelUi>(true));

        if (GameManager.instance.saveFile.levelDone > maxLevel)
            GameManager.instance.saveFile.levelDone = maxLevel;

        for (int i = 0; i < GameManager.instance.saveFile.levelDone; i++)
            levelsUi[i].gameObject.SetActive(true);
    }
}
