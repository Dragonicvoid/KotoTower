using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { set; get; }
    public SaveState state;

    // ref the static var to this
    private void Awake()
    {
        instance = this;
    }

    //Save the whole state of this saveState
    public void saveNewGame()
    {
        state = new SaveState(true);
        PlayerPrefs.SetString("save", OtherMethod.serialize<SaveState>(state));
        GameManager.instance.saveFile = state;
    }

    // save the game when updated
    public void saveAndUpdate()
    {
        state = GameManager.instance.saveFile;
        PlayerPrefs.SetString("save", OtherMethod.serialize<SaveState>(state));
    }

    // load the game
    public void load()
    {
        // Do we have the save?
        if (PlayerPrefs.HasKey("save"))
            state = OtherMethod.deserialize<SaveState>(PlayerPrefs.GetString("save"));

        GameManager.instance.saveFile = state;
    }
}
