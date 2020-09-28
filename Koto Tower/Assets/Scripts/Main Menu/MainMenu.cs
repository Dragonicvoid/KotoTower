using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // register
    [SerializeField] Button newGameButton = null;
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

    LoginRegister loginRegister;
    bool isNowConfirmation;

    Camera mainCamera;

    // check if there is save file, if yes activate the continue
    private void Awake()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            SaveManager.instance.load();
            newGameButton.interactable = GameManager.instance.hasLogin;
            continueButton.interactable = GameManager.instance.hasLogin && GameManager.instance.saveFile.levelDone != 0;
            rangkumanButton.interactable = GameManager.instance.hasLogin && GameManager.instance.saveFile.levelDone != 0;
            leaderboards.interactable = GameManager.instance.hasLogin && GameManager.instance.saveFile.levelDone != 0;
        }
        else
        {
            GameManager.instance.saveFile = new SaveState();
            newGameButton.interactable = false;
            continueButton.interactable = false;
            rangkumanButton.interactable = false;
            leaderboards.interactable = false;
        }
    }

    // get main camera of the scene
    private void Start()
    {
        isNowConfirmation = false;
        mainCamera = Camera.main;
        loginRegister = this.gameObject.GetComponent<LoginRegister>();
        loginRegister.changeText(GameManager.instance.hasLogin);

        if ((!PlayerPrefs.HasKey("save") || (PlayerPrefs.HasKey("save") && "".Equals(GameManager.instance.saveFile.username)) ) && !GameManager.instance.hasLogin)
        {
            loginRegister.resetFields();
            filter.SetActive(true);
            login.SetActive(true);
        }
    }

    // go to level select by new Game
    public void goToLevelSelectNewGame()
    {
        GameManager.instance.makeButtonPressSound();
        if (PlayerPrefs.HasKey("save") && GameManager.instance.saveFile.levelDone != 0)
        {
            filter.SetActive(true);
            confirmationPanel.SetActive(true);
            isNowConfirmation = true;
        }
        else
        {
            SaveManager.instance.saveNewGame();
            GameManager.instance.stopMainMenuMusic();
            GameManager.instance.isTutorial = true;
            GameManager.instance.setDifficulty(0);
            GameManager.instance.loadGame((int)Levels.TUTORIAL);
        }
    }

    // go to level select by new Game after confirmation
    public void goToLevelSelectNewGameAfterConfirmation()
    {
        GameManager.instance.makeButtonPressSound();
        PlayerPrefs.DeleteAll();
        SaveManager.instance.saveNewGame();
        GameManager.instance.stopMainMenuMusic();
        GameManager.instance.isTutorial = true;
        GameManager.instance.setDifficulty(0);
        GameManager.instance.loadGame((int)Levels.TUTORIAL);
    }

    // close the confirmation
    public void closeConfirmation()
    {
        GameManager.instance.makeButtonPressSound();
        confirmationPanel.SetActive(false);
        filter.SetActive(false);
        isNowConfirmation = false;
    }

    // go to level select by load Game
    public void goToLevelSelectLoadGame()
    {
        if (!isNowConfirmation)
        {
            GameManager.instance.makeButtonPressSound();
            SaveManager.instance.load();
            GameManager.instance.loadGame((int)Levels.CHOOSE_LEVEL);
        }
    }

    // go to rangkuman
    public void goToRangkuman()
    {
        if (!isNowConfirmation)
        {
            GameManager.instance.makeButtonPressSound();
            SaveManager.instance.load();
            GameManager.instance.stopMainMenuMusic();
            GameManager.instance.loadGame((int)Levels.RANGKUMAN);
        }
    }

    // go to leaderboards
    public void goToLeaderboards()
    {
        if (!isNowConfirmation)
        {
            GameManager.instance.makeButtonPressSound();
            SaveManager.instance.load();
            GameManager.instance.loadGame((int)Levels.LEADERBOARDS);
        }
    }

    // open login page and the filter
    public void openLogin()
    {
        if (!isNowConfirmation && !GameManager.instance.hasLogin)
        {
            GameManager.instance.makeButtonPressSound();
            loginRegister.resetFields();
            filter.SetActive(true);
            login.SetActive(true);
        }
        else if(!isNowConfirmation)
        {
            GameManager.instance.makeButtonPressSound();
            GameManager.instance.hasLogin = false;

            newGameButton.interactable = false;
            continueButton.interactable = false;
            rangkumanButton.interactable = false;
            leaderboards.interactable = false;

            // update if player has a save
            if (PlayerPrefs.HasKey("save"))
            {
                GameManager.instance.userId = -1;
                GameManager.instance.saveFile.username = "";
                GameManager.instance.saveFile.setPassword("");
                SaveManager.instance.saveAndUpdate();
            }
            loginRegister.changeText(GameManager.instance.hasLogin);
        }
    }

    // open Register page and close the login
    public void openRegister()
    {
        if (!isNowConfirmation)
        {
            GameManager.instance.makeButtonPressSound();
            loginRegister.resetFields();
            login.SetActive(false);
            register.SetActive(true);
        }
    }

    // close login page and the filter
    public void closeLogin()
    {
        if (!isNowConfirmation)
        {
            GameManager.instance.makeButtonPressSound();
            login.SetActive(false);
            filter.SetActive(false);
        }
    }

    // close Register page and filter
    public void closeRegister()
    {
        if (!isNowConfirmation)
        {
            GameManager.instance.makeButtonPressSound();
            register.SetActive(false);
            filter.SetActive(false);
        }
    }

    // kembali dari register menuju login
    public void backLogin()
    {
        if (!isNowConfirmation)
        {
            GameManager.instance.makeButtonPressSound();
            register.SetActive(false);
            login.SetActive(true);
        }
    }

    // exit the game
    public void quit()
    {
        if (!isNowConfirmation)
        {
            GameManager.instance.makeButtonPressSound();
            if(PlayerPrefs.HasKey("save"))
                SaveManager.instance.saveAndUpdate();
            Application.Quit(0);
        }
    }
}
