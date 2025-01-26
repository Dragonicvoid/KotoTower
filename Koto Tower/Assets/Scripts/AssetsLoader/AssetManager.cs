using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    Dictionary<ASSET_KEY, Texture2D> assetsTex = new Dictionary<ASSET_KEY, Texture2D>();

    public static AssetManager current;

    private void Awake()
    {
        current = this;
    }

    public void AddTexture(ASSET_KEY key, Texture2D tex)
    {
        assetsTex.Add(key, tex);
    }

    public void Remove(ASSET_KEY key)
    {
        assetsTex.Remove(key);
    }
}
