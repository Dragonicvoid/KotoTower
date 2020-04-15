using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // continue
    [SerializeField] Button continueButton;
    // Rangkuman 
    [SerializeField] Button rangkumanButton;
    // Confirmation Panel
    [SerializeField] GameObject confirmationPanel;

    bool isNowConfirmation;

    Camera mainCamera;

    // check if there is save file, if yes activate the continue
    private void Awake()
    {
        continueButton.interactable = PlayerPrefs.HasKey("save");
        rangkumanButton.interactable = PlayerPrefs.HasKey("save");
    }

    // get main camera of the scene
    private void Start()
    {
        isNowConfirmation = false;
        mainCamera = Camera.main;
    }

    // go to level select by new Game
    public void goToLevelSelectNewGame()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            confirmationPanel.SetActive(true);
            isNowConfirmation = true;
        }
        else
        {
            SaveManager.instance.saveNewGame();
            GameManager.instance.loadGame((int)Levels.CHOOSE_LEVEL);
        }
    }

    // go to level select by new Game after confirmation
    public void goToLevelSelectNewGameAfterConfirmation()
    {
        PlayerPrefs.DeleteAll();
        SaveManager.instance.saveNewGame();
        GameManager.instance.loadGame((int)Levels.CHOOSE_LEVEL);
    }

    public void closeConfirmation()
    {
        confirmationPanel.SetActive(false);
        isNowConfirmation = false;
    }

    // go to level select by load Game
    public void goToLevelSelectLoadGame()
    {
        if (!isNowConfirmation)
        {
            SaveManager.instance.load();
            GameManager.instance.loadGame((int)Levels.CHOOSE_LEVEL);
        }
    }

    // go to rangkuman
    public void goToRangkuman()
    {
        if (!isNowConfirmation)
        {
            SaveManager.instance.load();
            GameManager.instance.loadGame((int)Levels.RANGKUMAN);
        }
    }
}
