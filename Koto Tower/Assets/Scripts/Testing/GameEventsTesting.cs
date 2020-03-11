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

    public event Action onKotoTowerDestroyedEnter;
    public void KotoTowerDestroyedEnter()
    {
        if (onKotoTowerDestroyedEnter != null)
        {
            onKotoTowerDestroyedEnter();
        }
    }

    // Event that plays when Koto Tower or Generator is off screen
    // 0 is for Koto Tower and 1 is for Generator
    public event Action<int> onObjectOffScreenEnter;
    public void ObjectOffScreenEnter(int id)
    {
        if (onObjectOffScreenEnter != null)
        {
            onObjectOffScreenEnter(id);
        }
    }

    // Event that plays when Koto Tower or Generator is off screen
    // 0 is for Koto Tower and 1 is for Generator
    public event Action<int> onObjectOnScreenEnter;
    public void ObjectOnScreenEnter(int id)
    {
        if (onObjectOnScreenEnter != null)
        {
            onObjectOnScreenEnter(id);
        }
    }
}
