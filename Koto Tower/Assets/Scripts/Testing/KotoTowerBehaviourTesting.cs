using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotoTowerBehaviourTesting : MonoBehaviour
{
    [SerializeField] float health = 50f;

    // damage
    public void addDamage(float damage)
    {
        health -= damage;

        Debug.Log(health);

        if (health <= 0f)
            GameEventsTesting.current.KotoTowerDestroyed();
    }
}
