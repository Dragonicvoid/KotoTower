using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownProperty : MonoBehaviour
{
    [SerializeField]
    private Image image;
    private int index;
    public int Index
    {
        set
        {
            index = value;
            updateImage();
        }

        get
        {
            return index;
        }
    }

    private ASSET_KEY[] imageKeys = {
        ASSET_KEY.MATERI_LEVEL_0,
        ASSET_KEY.MATERI_LEVEL_1,
        ASSET_KEY.MATERI_LEVEL_2,
        ASSET_KEY.MATERI_LEVEL_3,
        ASSET_KEY.MATERI_LEVEL_4,
        ASSET_KEY.MATERI_LEVEL_5,
        ASSET_KEY.MATERI_LEVEL_6,
        ASSET_KEY.MATERI_LEVEL_7,
        ASSET_KEY.MATERI_LEVEL_8,
        ASSET_KEY.MATERI_LEVEL_9,
        ASSET_KEY.MATERI_LEVEL_10,
    };

    public void updateImage()
    {
        ASSET_KEY key = getKeyFromIdx();

        Sprite prevSprite = image.sprite;

        Texture2D confTex;
        bool hasAsset = AssetManager.current.assetsTexture.TryGetValue(key, out confTex);

        if (hasAsset)
        {
            Rect rec = new Rect(0, 0, confTex.width, confTex.height);
            Sprite confSprite = Sprite.Create(confTex, rec, new Vector2(0, 0), 0.01f);
            image.rectTransform.sizeDelta = new Vector2(0, confTex.height);
            image.sprite = confSprite;
        }
        else
        {
            image.rectTransform.sizeDelta = new Vector2(image.rectTransform.sizeDelta.x, image.rectTransform.sizeDelta.y);
            image.sprite = prevSprite;
        }
    }

    private ASSET_KEY getKeyFromIdx()
    {
        return imageKeys[Index];
    }
}
