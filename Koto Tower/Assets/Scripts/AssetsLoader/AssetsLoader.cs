using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssetsLoader : MonoBehaviour
{
    [SerializeField]
    Slider downloadSlider = null;

    uint totalAssets = 0;
    uint downloadAsset = 0;

    IEnumerator downloadEnumerator = null;

    private void Awake()
    {
        downloadEnumerator = startAssetsDownload();
    }

    private void Start()
    {
        StartCoroutine(downloadEnumerator);
    }

    List<AssetConfig> getAssetsConf()
    {
        List<AssetConfig> assets = new List<AssetConfig>() {
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882025/Materi/Tutorial_do4nug.jpg",
                key = ASSET_KEY.MATERI_LEVEL_0,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882022/Materi/Level_1_qn82yt.jpg",
                key = ASSET_KEY.MATERI_LEVEL_1,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882024/Materi/Level_2_szsaur.jpg",
                key = ASSET_KEY.MATERI_LEVEL_2,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882023/Materi/Level_3_zshmnx.jpg",
                key = ASSET_KEY.MATERI_LEVEL_3,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882034/Materi/Level_4_qqxr4r.jpg",
                key = ASSET_KEY.MATERI_LEVEL_4,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882025/Materi/Level_5_ffstqn.jpg",
                key = ASSET_KEY.MATERI_LEVEL_5,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882024/Materi/Level_6_myhyzw.jpg",
                key = ASSET_KEY.MATERI_LEVEL_6,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882026/Materi/Level_7_ayr6wc.jpg",
                key = ASSET_KEY.MATERI_LEVEL_7,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882024/Materi/Level_8_lettpz.jpg",
                key = ASSET_KEY.MATERI_LEVEL_8,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882025/Materi/Level_9_xxspd6.jpg",
                key = ASSET_KEY.MATERI_LEVEL_9,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882025/Materi/Level_10_zilisr.jpg",
                key = ASSET_KEY.MATERI_LEVEL_10,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },

            // Rangkuman
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897924/Header%20Rangkuman/Tutorial_qlmak9.jpg",
                key = ASSET_KEY.HEADER_LEVEL_0,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897915/Header%20Rangkuman/Level_1_j5bqcu.jpg",
                key = ASSET_KEY.HEADER_LEVEL_1,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897916/Header%20Rangkuman/Level_2_zcyzvr.png",
                key = ASSET_KEY.HEADER_LEVEL_2,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897917/Header%20Rangkuman/Level_3_dkabm8.png",
                key = ASSET_KEY.HEADER_LEVEL_3,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897917/Header%20Rangkuman/Level_4_gkhpey.png",
                key = ASSET_KEY.HEADER_LEVEL_4,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897918/Header%20Rangkuman/Level_5_gan6uk.png",
                key = ASSET_KEY.HEADER_LEVEL_5,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897919/Header%20Rangkuman/Level_6_ynhjdc.png",
                key = ASSET_KEY.HEADER_LEVEL_6,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897920/Header%20Rangkuman/Level_7_ptoby5.png",
                key = ASSET_KEY.HEADER_LEVEL_7,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897921/Header%20Rangkuman/Level_8_uk1ror.png",
                key = ASSET_KEY.HEADER_LEVEL_8,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897922/Header%20Rangkuman/Level_9_eofyyr.png",
                key = ASSET_KEY.HEADER_LEVEL_9,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737897923/Header%20Rangkuman/Level_10_t4u7xd.png",
                key = ASSET_KEY.HEADER_LEVEL_10,
                type = ASSET_TYPE.IMAGE,
                opts = new DownloadOpts(),
            },

            // TEXT
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/raw/upload/v1737902329/Config/LevelSelect_jmli0t.json",
                key = ASSET_KEY.LEVEL_SELECT_JSON,
                type = ASSET_TYPE.TEXT,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/raw/upload/v1737981055/Config/LevelConfig_naysqh.json",
                key = ASSET_KEY.LEVEL_CONFIG_JSON,
                type = ASSET_TYPE.TEXT,
                opts = new DownloadOpts(),
            }
        };

        totalAssets = (uint)assets.Count;
        return assets;
    }

    IEnumerator startAssetsDownload()
    {
        yield return null;

        List<AssetConfig> confs = this.getAssetsConf();
        downloadAsset = 2;
        updateSlider();

        AssetLoadEvent.current.onDownloadAssetCompleted += onDownloadCompleted;
        AssetLoadEvent.current.onDownloadAssetFailed += onAssetFailed;
        AssetLoadEvent.current.onDownloadAssetSuccess += onAssetSuccess;

        confs.ForEach((asset) =>
        {
            switch (asset.type)
            {
                case ASSET_TYPE.IMAGE:
                    StartCoroutine(downloadImage(asset));
                    break;
                case ASSET_TYPE.TEXT:
                    StartCoroutine(downloadText(asset));
                    break;
                default:
                    break;
            }
        });
    }

    void onAssetSuccess(AssetConfig _)
    {
        downloadAsset++;
        updateSlider();
    }

    void onAssetFailed(AssetConfig conf)
    {
        Debug.Log("Missing Assets: " + conf.key);
    }

    void onDownloadCompleted()
    {
        GameManager.instance.loadGame((int)Levels.MAIN_MENU);
    }

    void updateSlider()
    {
        if (downloadSlider == null) return;

        float value;
        if (totalAssets != 0)
        {
            value = (float)downloadAsset / (float)totalAssets;
        }
        else
        {
            value = 1;
        }

        downloadSlider.value = value;

        if (value >= 1)
        {
            AssetLoadEvent.current.DownloadAssetCompletedEnter();
        }
    }

    IEnumerator downloadImage(AssetConfig conf, uint tries = 0)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(conf.url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            if (conf.opts.Retries < tries)
            {
                downloadImage(conf, tries++);
            }
            else
            {
                AssetLoadEvent.current.DownloadAssetFailedEnter(conf);
            }
        }
        else
        {
            Texture2D tex = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (!tex) yield break;
            AssetManager.current.AddTexture(conf.key, ((DownloadHandlerTexture)request.downloadHandler).texture);
            AssetLoadEvent.current.DownloadAssetSuccessEnter(conf);
        }
    }

    IEnumerator downloadText(AssetConfig conf, uint tries = 0)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(conf.url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            if (conf.opts.Retries < tries)
            {
                downloadText(conf, tries++);
            }
            else
            {
                AssetLoadEvent.current.DownloadAssetFailedEnter(conf);
            }
        }
        else
        {
            AssetManager.current.AddTextAsset(conf.key, request.downloadHandler.text);
            AssetLoadEvent.current.DownloadAssetSuccessEnter(conf);
        }
    }

    private void OnDestroy()
    {
        AssetLoadEvent.current.onDownloadAssetCompleted -= onDownloadCompleted;
        AssetLoadEvent.current.onDownloadAssetFailed -= onAssetFailed;
        AssetLoadEvent.current.onDownloadAssetSuccess -= onAssetSuccess;

        StopCoroutine(downloadEnumerator);
        downloadEnumerator = null;
    }
}
