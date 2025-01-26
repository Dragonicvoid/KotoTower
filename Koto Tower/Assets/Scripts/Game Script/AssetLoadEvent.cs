using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoadEvent : MonoBehaviour
{
    public static AssetLoadEvent current;

    private void Awake()
    {
        current = this;
    }

    public event Action<AssetConfig> onDownloadAssetSuccess;
    public void DownloadAssetSuccessEnter(AssetConfig conf)
    {
        onDownloadAssetSuccess(conf);
    }

    public event Action<AssetConfig> onDownloadAssetFailed;
    public void DownloadAssetFailedEnter(AssetConfig conf)
    {
        onDownloadAssetFailed(conf);
    }

    public event Action onDownloadAssetCompleted;
    public void DownloadAssetCompletedEnter()
    {
        onDownloadAssetCompleted();
    }
}
