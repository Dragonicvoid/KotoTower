using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TrapType
{
    BOMB_TRAP,
    TIME_TRAP,
    FREEZE_TRAP
}


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/TrapPropertiesScriptableObject", order = 3)]
public class TrapPropertiesScriptableObject : ScriptableObject
{
    public string prefabName = "";

    public float explodeTimer = 3f;
    public float duration = 3f;
    public float radius = 3f;
    public float damage = 50f;
    public bool isStopTime = false;
    public float sizeMultiplyer = 1.5f;
    public AnimationCurve curve = null;
    public TrapType type = TrapType.BOMB_TRAP;
}
