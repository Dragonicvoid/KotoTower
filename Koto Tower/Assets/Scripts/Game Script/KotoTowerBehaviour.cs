using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotoTowerBehaviour : MonoBehaviour
{
    [SerializeField] float health = 50f;
    float maxHealth;
    HealthController healthController;

    // initilization
    private void Start()
    {
        maxHealth = health;
        healthController = this.gameObject.GetComponentInChildren<HealthController>();
    }

    // damage
    public void addDamage(float damage)
    {
        health -= damage;

        if (health <= 0f)
            GameEvents.current.KotoTowerDestroyed();

        gotDamage();
    }

    // got damage then show it to player
    void gotDamage()
    {
        healthController.gotDamaged(health, maxHealth);
    }
}
