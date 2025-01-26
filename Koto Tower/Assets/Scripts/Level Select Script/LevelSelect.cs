using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] Text header = null;
    [SerializeField] Text subHeader = null;
    [SerializeField] Image image = null;

    // 0 : easy, 1: medium, 2: hard
    [SerializeField] Text difficultyText = null;
    [SerializeField] Text descText = null;
    [SerializeField] Image backgroundDiffImage = null;
    [SerializeField] Levels level;
    [SerializeField] public int levelIndex = 0;

    // get color property id
    int colorPropertyId;

    // all color for this
    Color easyColor;
    Color mediumColor;
    Color hardColor;

    // difficulty index 0: easy, 1: medium, 2: hard;
    int difficultyIdx;

    LevelSelectData levelSelectData = new LevelSelectData();

    // Index 0: Tutorial, Index 1 - Next: Level 1,2,3
    ASSET_KEY[] imageKeys = {
        ASSET_KEY.HEADER_LEVEL_0,
        ASSET_KEY.HEADER_LEVEL_1,
        ASSET_KEY.HEADER_LEVEL_2,
        ASSET_KEY.HEADER_LEVEL_3,
        ASSET_KEY.HEADER_LEVEL_4,
        ASSET_KEY.HEADER_LEVEL_5,
        ASSET_KEY.HEADER_LEVEL_6,
        ASSET_KEY.HEADER_LEVEL_7,
        ASSET_KEY.HEADER_LEVEL_8,
        ASSET_KEY.HEADER_LEVEL_9,
        ASSET_KEY.HEADER_LEVEL_10,
    };

    // initialization
    private void Awake()
    {

        string json;
        AssetManager.current.assetsText.TryGetValue(ASSET_KEY.LEVEL_SELECT_JSON, out json);

        if (json != "")
        {
            levelSelectData = JsonUtility.FromJson<LevelSelectData>(json);
        }
    }

    private void Start()
    {
        colorPropertyId = Shader.PropertyToID("_Color");
        difficultyIdx = 0;
        easyColor = new Color((float)54 / 255, (float)176 / 255, 0);
        mediumColor = new Color((float)176 / 255, (float)153 / 255, 0);
        hardColor = new Color((float)207 / 255, 0, (float)9 / 255);
        setupConfig();
    }

    private void OnEnable()
    {
        setupConfig();
    }

    // apabila menekan lanjut
    public void next()
    {
        if (difficultyIdx != 2)
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

        setupConfig();
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

        setupConfig();
    }

    // show the text according to the difficulty
    void setupConfig()
    {
        LevelSelectConf conf = levelSelectData.data.Find((c) => { return c.level == levelIndex; });

        if (conf == null) return;

        ASSET_KEY key = getKeyFromLevel();

        Sprite prevSprite = image.sprite;
        Rect rec = new Rect(0, 0, image.sprite.rect.width, image.sprite.rect.height);

        Texture2D confTex;
        AssetManager.current.assetsTexture.TryGetValue(key, out confTex);
        Sprite confSprite = Sprite.Create(confTex, rec, new Vector2(0, 0), 0.01f);

        if (confSprite)
        {
            image.sprite = confSprite;
        }
        else
        {
            image.sprite = prevSprite;
        }

        header.text = conf.header;
        subHeader.text = conf.subHeader;

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
                totalQuestion = 8;
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

        string finalText = conf.description
            .Replace("{question}", "<color=#" + ColorUtility.ToHtmlStringRGB(color) + "><b>" + totalQuestion.ToString() + "</b></color>")
            .Replace("{level_mudah}", "<color=#" + ColorUtility.ToHtmlStringRGB(easyColor) + "><b>" + (difficultyIdx >= 0 ? conf.easyText.ToString() : "") + "</b></color>")
            .Replace("{level_medium}", "<color=#" + ColorUtility.ToHtmlStringRGB(mediumColor) + "><b>" + (difficultyIdx >= 1 ? conf.mediumText.ToString() : "") + "</b></color>")
            .Replace("{level_hard}", "<color=#" + ColorUtility.ToHtmlStringRGB(hardColor) + "><b>" + (difficultyIdx >= 2 ? conf.hardText.ToString() : "") + "</b></color>")
            .Replace("{min_difficulty}", "<color=#" + ColorUtility.ToHtmlStringRGB(hardColor) + "><b>Hard</b></color>");

        descText.text = finalText;
    }

    ASSET_KEY getKeyFromLevel()
    {
        return imageKeys[levelIndex];
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
