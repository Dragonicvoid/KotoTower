public class AssetConfig
{
    public string url { get; set; }
    public ASSET_KEY key { get; set; }
    public ASSET_TYPE type { get; set; }
    public DownloadOpts opts { get; set; }
}

public class DownloadOpts
{
    private uint retries = 4;
    public uint Retries { get { return retries; } set { retries = value; } }
}