using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

enum UiType
{
    TEXT,
    DIFFICULTY_SELECT
}

public class LevelSelectTesting : MonoBehaviour
{
    // 0 : easy, 1: medium, 2: hard
    [SerializeField] List<Text> difficultyDescText = null;
    [SerializeField] Text difficultyText = null;
    [SerializeField] Image backgroundDiffImage = null;

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

        easyColor = difficultyDescText[0].color;
        mediumColor = difficultyDescText[1].color;
        hardColor = difficultyDescText[2].color;

        difficultyIdx = 0;
    }

    // apabila menekan lanjut
    public void next()
    {
        if(difficultyIdx != 2)
        {
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
        foreach (Text text in difficultyDescText)
            text.gameObject.SetActive(false);

        for (int i = 0; i <= difficultyIdx; i++)
            difficultyDescText[i].gameObject.SetActive(true);
    }

    // bila pemain pilih untuk bermain
    public void play()
    {
        GameManager.instance.setDifficulty(difficultyIdx);
        GameManager.instance.loadGame(2);
    }
}
