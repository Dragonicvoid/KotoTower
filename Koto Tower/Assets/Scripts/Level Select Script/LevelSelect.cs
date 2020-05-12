using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum UiType
{
    TEXT,
    DIFFICULTY_SELECT
}

public class LevelSelect : MonoBehaviour
{
    // 0 : easy, 1: medium, 2: hard
    [SerializeField] List<Text> difficultyDescText = null;
    [SerializeField] Text difficultyText = null;
    [SerializeField] Text descText = null;
    [SerializeField] Image backgroundDiffImage = null;
    [SerializeField] Levels level;
    [SerializeField] int levelIndex = 0;

    // get color property id
    int colorPropertyId;

    // all color for this
    Color easyColor;
    Color mediumColor;
    Color hardColor;

    // difficulty index 0: easy, 1: medium, 2: hard;
    int difficultyIdx;

    // initialization
    private void Start()
    {
        colorPropertyId = Shader.PropertyToID("_Color");

        difficultyIdx = 0;

        if (difficultyDescText.Count > 0)
        {
            easyColor = difficultyDescText[0].color;
            mediumColor = difficultyDescText[1].color;
            hardColor = difficultyDescText[2].color;
            showText();
        }
    }

    // apabila menekan lanjut
    public void next()
    {
        if(difficultyIdx != 2)
        {
            GameManager.instance.makeButtonPressSound();
            difficultyIdx++;
            switch (difficultyIdx)
            {
                case 1:
                    backgroundDiffImage.color = mediumColor;
                    difficultyText.text = "Menengah";
                    break;
                case 2:
                    backgroundDiffImage.color = hardColor;
                    difficultyText.text = "Sulit";
                    break;
                default:
                    backgroundDiffImage.color = Color.white;
                    difficultyText.text = "Null";
                    break;
            }
        }

        showText();
    }

    // apabila menekan sebelum
    public void prev()
    {
        if (difficultyIdx != 0)
        {
            GameManager.instance.makeButtonPressSound();
            difficultyIdx--;
            switch (difficultyIdx)
            {
                case 0:
                    backgroundDiffImage.color = easyColor;
                    difficultyText.text = "Mudah";
                    break;
                case 1:
                    backgroundDiffImage.color = mediumColor;
                    difficultyText.text = "Menengah";
                    break;
                default:
                    backgroundDiffImage.color = Color.white;
                    difficultyText.text = "Null";
                    break;
            }

        }

        showText();
    }

    // show the text according to the difficulty
    void showText()
    {
        Color color;
        int totalQuestion;
        switch (difficultyIdx)
        {
            case 0:
                color = easyColor;
                totalQuestion = 5;
                break;
            case 1:
                color = mediumColor;
                totalQuestion = 7;
                break;
            case 2:
                color = hardColor;
                totalQuestion = 10;
                break;
            default:
                color = easyColor;
                totalQuestion = 5;
                break;
        }
        descText.text = "Untuk memenangkan level pemain harus menjawab <color=#" + ColorUtility.ToHtmlStringRGB(color) + "><b>" + totalQuestion.ToString() + "</b></color> pertanyaan berupa:";

        foreach (Text text in difficultyDescText)
            text.gameObject.SetActive(false);

        for (int i = 0; i <= difficultyIdx; i++)
            difficultyDescText[i].gameObject.SetActive(true);
    }

    // bila pemain pilih untuk latihan
    public void practice()
    {
        GameManager.instance.makeButtonPressSound();
        GameManager.instance.isTutorial = false;
        GameManager.instance.isPractice = true;
        GameManager.instance.stopMainMenuMusic();
        GameManager.instance.currentLevelIndex = levelIndex;
        GameManager.instance.setDifficulty(difficultyIdx);
        GameManager.instance.loadGame((int)level);
    }

    // bila pemain pilih untuk bermain
    public void play()
    {
        GameManager.instance.makeButtonPressSound();
        GameManager.instance.isTutorial = false;
        GameManager.instance.isPractice = false;
        GameManager.instance.stopMainMenuMusic();
        GameManager.instance.currentLevelIndex = levelIndex;
        GameManager.instance.setDifficulty(difficultyIdx);
        GameManager.instance.loadGame((int)level);
    }

    // bila pemain membuka level pertama
    public void tutorial()
    {
        GameManager.instance.makeButtonPressSound();
        GameManager.instance.isTutorial = true;
        GameManager.instance.isPractice = false;
        GameManager.instance.stopMainMenuMusic();
        GameManager.instance.currentLevelIndex = levelIndex;
        GameManager.instance.setDifficulty(difficultyIdx);
        GameManager.instance.loadGame((int)level);
    }
}
