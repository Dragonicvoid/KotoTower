using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotoTowerBehaviourTesting : MonoBehaviour
{
    [SerializeField] float health = 50f;
    float maxHealth;
    HealthControllerTesting healthController;

    // initilization
    private void Start()
    {
        maxHealth = health;
        healthController = this.gameObject.GetComponentInChildren<HealthControllerTesting>();
    }

    // damage
    public void addDamage(float damage)
    {
        health -= damage;

        Debug.Log(health);

        if (health <= 0f)
            GameEventsTesting.current.KotoTowerDestroyed();

        gotDamage();
    }

    // got damage then show it to player
    void gotDamage()
    {
        healthController.gotDamaged(health, maxHealth);
    }
}
