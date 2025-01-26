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
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882022/Materi/Level_1_qn82yt.jpg",
                key = ASSET_KEY.MATERI_LEVEL_1,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882024/Materi/Level_2_szsaur.jpg",
                key = ASSET_KEY.MATERI_LEVEL_2,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882023/Materi/Level_3_zshmnx.jpg",
                key = ASSET_KEY.MATERI_LEVEL_3,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882034/Materi/Level_4_qqxr4r.jpg",
                key = ASSET_KEY.MATERI_LEVEL_4,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882025/Materi/Level_5_ffstqn.jpg",
                key = ASSET_KEY.MATERI_LEVEL_5,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882024/Materi/Level_6_myhyzw.jpg",
                key = ASSET_KEY.MATERI_LEVEL_6,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882026/Materi/Level_7_ayr6wc.jpg",
                key = ASSET_KEY.MATERI_LEVEL_7,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882024/Materi/Level_8_lettpz.jpg",
                key = ASSET_KEY.MATERI_LEVEL_8,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882025/Materi/Level_9_xxspd6.jpg",
                key = ASSET_KEY.MATERI_LEVEL_9,
                opts = new DownloadOpts(),
            },
            new AssetConfig {
                url = "https://res.cloudinary.com/dyfgknhce/image/upload/v1737882025/Materi/Level_10_zilisr.jpg",
                key = ASSET_KEY.MATERI_LEVEL_10,
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
            Debug.Log(asset.key);
            StartCoroutine(downloadImage(asset));
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
            AssetManager.current.AddTexture(conf.key, ((DownloadHandlerTexture)request.downloadHandler).texture);
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
