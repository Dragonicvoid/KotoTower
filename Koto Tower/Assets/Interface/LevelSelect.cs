using System;
using System.Collections.Generic;

[Serializable]
public class LevelSelectConf
{
    public string header;
    public string subHeader;
    public string description;
    public uint level;
    public string easyText;
    public string mediumText;
    public string hardText;
}

[Serializable]
public class LevelSelectData
{
    public List<LevelSelectConf> data;
}