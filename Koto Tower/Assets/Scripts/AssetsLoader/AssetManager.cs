using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public Dictionary<ASSET_KEY, Texture2D> assetsTexture = new Dictionary<ASSET_KEY, Texture2D>();

    public Dictionary<ASSET_KEY, string> assetsText = new Dictionary<ASSET_KEY, string>();

    public static AssetManager current;

    private void Awake()
    {
        current = this;
    }

    public void AddTexture(ASSET_KEY key, Texture2D tex)
    {
        assetsTexture.Add(key, tex);
    }

    public void AddTextAsset(ASSET_KEY key, string text)
    {
        assetsText.Add(key, text);
    }

    public void Remove(ASSET_KEY key)
    {
        assetsTexture.Remove(key);
    }
}
