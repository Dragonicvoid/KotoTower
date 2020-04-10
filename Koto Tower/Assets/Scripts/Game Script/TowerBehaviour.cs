using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    // tower attribute
    [SerializeField] TowerPropertiesScriptableObject property = null;

    // Tower isActive
    bool isActive;

    // Tower fire flash that seen when shooting
    GameObject fireFlash;
    GameObject circle;

    Vector2 currentPosition;
    bool hasTarget;

    // Timer so the tower doesn't keep shooting every frame
    float shootTimer;

    // Enemy attribute
    Vector2 currentTargetPosition;
    List<EnemyBehaviour> enemy;

    // Initialize variables so we dont have to keep using transform class and for shoot method
    void Start()
    {
        fireFlash = transform.GetChild(0).gameObject;
        circle = transform.GetChild(1).gameObject;
        enemy = new List<EnemyBehaviour>();
        isActive = false;
    }

    // Create gizmos so we can see the tower range when selected
    void OnDrawGizmosSelected()
    {
        switch (property.type)
        {
            case TowerType.MACHINE_GUN:
                Gizmos.color = new Color(1, 0, 1, 0.20f);
                Gizmos.DrawSphere(this.transform.position, property.radius);
                break;
            case TowerType.SNIPER:
                Gizmos.color = new Color(1, 0, 0, 0.20f);
                Gizmos.DrawSphere(this.transform.position, property.radius);
                break;
            case TowerType.ELECTRIC:
                Gizmos.color = new Color(0, 0, 1, 0.20f);
                Gizmos.DrawSphere(this.transform.position, property.radius);
                break;
        }   
    }

    // Calling the hit detection
    void Update()
    {
        if (isActive && !GameManager.instance.isPaused)
        {
            // Even tho it is not currently shooting, we have to clear the fire flash, putting if to avoid floating point error
            if (shootTimer < property.fireRate)
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
                    // for shoot around type of tower find target
                    if (property.shootAround)
                        checkAround();
                    break;
            }
        }
    }

    // Checking around the tower and damage the enemy if found
    void checkAround()
    {
        if (property.shootAround)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(currentPosition, property.radius, (1 << 8));

            if (hitColliders.Length > 0)
            {
                hasTarget = true;
                enemy.Clear();
                foreach (Collider2D hitCollider in hitColliders)
                    enemy.Add(hitCollider.GetComponent<EnemyBehaviour>());
            }
            else
                hasTarget = false;
        }
        else
        {
            Collider2D hitCollider = Physics2D.OverlapCircle(currentPosition, property.radius, (1 << 8));

            if (hitCollider != null)
            {
                // Keeping this found enemy as target
                hasTarget = true;
                enemy.Clear();
                enemy.Add(hitCollider.GetComponent<EnemyBehaviour>());
            }
        }
        
    }

    // Method to shoot the enemy until it's dead or escape the radius
    void shootTarget()
    {
        // if its shoot around type of tower and doesnt have target just move back
        if (property.shootAround)
        {
            if (shootTimer >= property.fireRate)
            {
                fireFlash.gameObject.SetActive(true);
                // Hit enemy and reset timer
                foreach (EnemyBehaviour target in enemy)
                    target.addDamage(property.damage, property.type);
                shootTimer = 0f;
            }
            else if (shootTimer >= (property.fireRate * (75 / 100)))
                fireFlash.gameObject.SetActive(false);
        }
        else
        {
            rotateToTarget();
            currentTargetPosition = enemy[0].getPosition();

            // If the enemy ran, or died change target
            if (Vector2.Distance(currentPosition, currentTargetPosition) > property.radius || enemy[0].status == EnemyStatus.DEAD)
                hasTarget = false;
            else if (shootTimer >= property.fireRate)
            {
                // Hit enemy and reset timer
                fireFlash.gameObject.SetActive(true);
                enemy[0].addDamage(property.damage, property.type);
                shootTimer = 0f;
            }
            else if (shootTimer >= (property.fireRate * (75 / 100)))
                fireFlash.gameObject.SetActive(false);
        }
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
        if (shootTimer >= (property.fireRate * (75 / 100)))
            fireFlash.gameObject.SetActive(false);
    }

    // get radius for creating circle
    public float getRadius()
    {
        return this.property.radius;
    }

    // activate means start shooting
    public void activate()
    {
        hasTarget = false;
        shootTimer = property.fireRate;
        currentPosition = this.transform.position;
        isActive = true;
        circle.SetActive(false);
    }

    // show Circle
    public void showCircle()
    {
        circle.gameObject.SetActive(true);
    }

    // unshow circle
    public void unshowCircle()
    {
        circle.gameObject.SetActive(false);
    }

    // get type of tower
    public TowerType getTowerType()
    {
        return this.property.type;
    }
}
