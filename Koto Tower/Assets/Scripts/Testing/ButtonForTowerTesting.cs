using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonForTowerTesting : MonoBehaviour
{
    public static bool isSelectTower;
    [SerializeField] List<Toggle> toggles;

    // property id for color;
    int colorPropertyId = Shader.PropertyToID("_Color");

    // Default value is not Selected
    private void Awake()
    {
        isSelectTower = false;
        foreach (Toggle toggle in toggles)
            toggle.isOn = false;
    }

    // Is change the state if it is to spawn tower, or want to move camera (can't be both)
    public void selectTower()
    {
        isSelectTower = !isSelectTower;
    }

    // disable the toggle
    public void disableToogle(int idx)
    {
        Toggle selectedToggle = toggles[idx];
        selectedToggle.isOn = false;
        isSelectTower = false;
    }
}
