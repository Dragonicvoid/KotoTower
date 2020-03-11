using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTargetTesting : MonoBehaviour
{
    // tower attribute
    [SerializeField] float damage = 3f;
    [SerializeField] float radius = 3f;
    [SerializeField] float fireRate = 3f;
    Vector2 currentPosition;
    bool hasTarget;

    // Fire flash object (the flash when tower is shooting)
    [SerializeField] GameObject fireFlash;

    // Timer so the tower doesn't keep shooting every frame
    float shootTimer;

    // Enemy attribute
    Vector2 currentTargetPosition;
    EnemyBehaviourTesting enemy;

    // Initialize variables so we dont have to keep using transform class and for shoot method
    void Start()
    {
        currentPosition = this.transform.position;
        hasTarget = false;
        shootTimer = fireRate;
    }

    // Create gizmos so we can see the tower range when selected
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 1, 0.20f);
        Gizmos.DrawSphere(this.transform.position, radius);
    }

    // Calling the hit detection
    void Update()
    {
        // Even tho it is not currently shooting, we have to clear the fire flash, putting if to avoid floating point error
        if (shootTimer < fireRate)
            shootTimer += Time.deltaTime;

        // disable the fire Flash if the timer is greater than 75% of the fire rate
        disableFireFlash();

        // Only has 2 states, which are checking if enemy nearby and shoot it
        switch (hasTarget)
        {
            case false:
                checkAround();
                break;
            case true:
                shootTarget();
                break;
        }
    }

    // Checking around the tower and damage the enemy if found
    void checkAround()
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(currentPosition, radius, (1 << 8));

        if (hitCollider != null && hitCollider.tag.Equals("Enemy"))
        {
            // Keeping this found enemy as target
            hasTarget = true;
            enemy = hitCollider.GetComponent<EnemyBehaviourTesting>();
        }
    }

    // Method to shoot the enemy until it's dead or escape the radius
    void shootTarget()
    {
        rotateToTarget();
        currentTargetPosition = enemy.getPosition();
        
        // If the enemy ran, or died change target
        if (Vector2.Distance(currentPosition, currentTargetPosition) > radius || enemy.status == EnemyBehaviourTesting.EnemyStatus.DEAD)
            hasTarget = false;
        else if (shootTimer >= fireRate)
        {
            // Hit enemy and reset timer
            fireFlash.gameObject.SetActive(true);
            enemy.addDamage(damage);
            shootTimer = 0f;
        }
        else if (shootTimer >= (fireRate * (75 / 100)))
            fireFlash.gameObject.SetActive(false);
    }

    // Method to rotate to target position
    void rotateToTarget()
    {
        // Get Radian
        float angleRad = Mathf.Atan2(currentTargetPosition.y - currentPosition.y, 
                                    currentTargetPosition.x - currentPosition.x);
        // Get the angle degree
        float angle = (180 / Mathf.PI) * angleRad;
        // Rotate object
        this.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    // disable the fire Flash if the timer is greater than 75% of the fire rate
    void disableFireFlash()
    {
        if (shootTimer >= (fireRate * (75 / 100)))
            fireFlash.gameObject.SetActive(false);
    }
}
