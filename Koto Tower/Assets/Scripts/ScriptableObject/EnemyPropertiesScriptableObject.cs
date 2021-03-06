﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStatus
{
    DEAD,
    MOVING,
    FROZEN,
    STOPPED,
    ATTACKING
}

public enum EnemyType
{
    NORMAL,
    ARMORED,
    GIANT
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/EnemyPropertiesScriptableObject", order = 2)]
public class EnemyPropertiesScriptableObject : ScriptableObject
{
    public string prefabName;

    public float speed = 1f;
    public float maxHealth = 10f;
    public float damage = 10f;
    public float shockRecovery = 10f;
    public float rewardPrice = 20f;
    public float hitRate = 2f;
    public EnemyType type = EnemyType.NORMAL;
}
