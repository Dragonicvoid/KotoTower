// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using System;
using System.Collections.Generic;

[Serializable]
public class LevelConfig
{
    public int levelId;
    public int startingCoin;
    public int enemyVariance;
    public int timeEfficient;
    public List<TowerConfig> towers;
    public List<TrapConfig> traps;
    public int tutorialId;
}

[Serializable]
public class LevelData
{
    public List<LevelConfig> data;
}

[Serializable]
public class TowerConfig
{
    public int id;
    public int price;
}

[Serializable]
public class TrapConfig
{
    public int id;
    public int price;
}