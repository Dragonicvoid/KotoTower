using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsTesting : MonoBehaviour
{
    public static GameEventsTesting current;

    private void Awake()
    {
        current = this;
    }

    public event Action onTowerOverwhelmEnter;
    public void TowerOverwhelmEnter()
    {
        if (onTowerOverwhelmEnter != null)
        {
            onTowerOverwhelmEnter();
        }
    }
}
