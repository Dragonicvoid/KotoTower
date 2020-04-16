using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // continue
    [SerializeField] Button continueButton = null;
    // Rangkuman 
    [SerializeField] Button rangkumanButton = null;
    // Confirmation Panel
    [SerializeField] GameObject confirmationPanel = null;
    // filter
    [SerializeField] GameObject filter = null;
    // peringkat (leaderboards)
    [SerializeField] Button leaderboards = null;
    // login 
    [SerializeField] GameObject login = null;
    // register
    [SerializeField] GameObject register = null;

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
            filter.SetActive(true);
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
        filter.SetActive(false);
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

    // open login page and the filter
    public void openLogin()
    {
        if (!isNowConfirmation)
        {
            filter.SetActive(true);
            login.SetActive(true);
        }
    }

    // open Register page and close the login
    public void openRegister()
    {
        if (!isNowConfirmation)
        {
            login.SetActive(false);
            register.SetActive(true);
        }
    }

    // close login page and the filter
    public void closeLogin()
    {
        if (!isNowConfirmation)
        {
            login.SetActive(false);
            filter.SetActive(false);
        }
    }

    // close Register page and filter
    public void closeRegister()
    {
        if (!isNowConfirmation)
        {
            register.SetActive(false);
            filter.SetActive(false);
        }
    }

    // kembali dari register menuju login
    public void backLogin()
    {
        if (!isNowConfirmation)
        {
            register.SetActive(false);
            login.SetActive(true);
        }
    }
}
