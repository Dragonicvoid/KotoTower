using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TowerType
{
    MACHINE_GUN,
    SNIPER,
    ELECTRIC
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/TowerPropertiesScriptableObject", order = 1)]
public class TowerPropertiesScriptableObject : ScriptableObject
{
    public string prefabName;

    public float damage = 0f;
    public float radius = 0f;
    public float fireRate = 0f;
    public bool shootAround = false;
    public TowerType type = TowerType.MACHINE_GUN;
}
