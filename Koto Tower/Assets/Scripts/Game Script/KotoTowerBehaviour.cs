using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotoTowerBehaviour : MonoBehaviour
{
    [SerializeField] float health = 50f;
    bool isHit;
    float maxHealth;
    float damageTimer = 3f;
    HealthController healthController;

    // initilization
    private void Start()
    {
        maxHealth = health;
        damageTimer = -1f;
        isHit = false;
        healthController = this.gameObject.GetComponentInChildren<HealthController>();
    }

    // decreasing the hit timer
    private void Update()
    {
        if (!GameManager.instance.isPaused)
        {
            if (damageTimer > 0f && isHit)
                damageTimer -= Time.deltaTime;
            else
                isHit = false;
        }
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
        isHit = true;
        damageTimer = 3f;
        healthController.gotDamaged(health, maxHealth);
    }

    // get the is hit property
    public bool getIsHit()
    {
        return isHit;
    }
}
