using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TruckStatus
{
    WAITING,
    DRIVING,
    BOOSTED,
    EXPLODING,
    DESTROYED
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/TruckPropertiesScriptableObject", order = 4)]
public class TruckPropertiesScriptableObject : ScriptableObject
{
    public string prefabName = "";

    // Truck attribute
    public float speed = 1f;
    public float maxHealth = 10f;
    public float damageTimer = 1f;
    public float radius = 1f;
    public float explodeTime = 2f;
    public float explodeDamage = 50f;
    public float boostedDuration = 2f;
}
