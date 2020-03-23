using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsBehaviourTesting : MonoBehaviour
{
    [SerializeField] TrapPropertiesScriptableObject property = null;
    bool hasExplode;
    bool isActive;
    Vector3 currentPosition;
    float timer;

    // initialization
    private void Awake()
    {
        hasExplode = false;
        timer = 0f;
        currentPosition = this.transform.position;
    }

    // when they are spawned
    private void Start()
    {
        hasExplode = false;
        isActive = false;
        timer = 0f;
        currentPosition = this.transform.position;
    }

    // countdown to explosion
    private void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;

            if (timer > property.explodeTimer && !hasExplode)
                explode();

            if (timer >= property.duration && hasExplode)
                expired();
            else if (hasExplode)
                StartCoroutine(active());
        }
    }

    // when it has explode but not expired
    IEnumerator active()
    {
        while(timer < property.duration)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(currentPosition, property.radius, (1 << 8));

            if (hitColliders.Length > 0)
                foreach (Collider2D hitCollider in hitColliders)
                    hitCollider.GetComponent<EnemyBehaviourTesting>().addStatusFromTraps(property.type, property.damage);

            yield return new WaitForSeconds(1f);
        }
    }

    // when the timer ends, it explode
    void explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(currentPosition, property.radius, (1 << 8));

        if (hitColliders.Length > 0)
            foreach (Collider2D hitCollider in hitColliders)
                hitCollider.GetComponent<EnemyBehaviourTesting>().addStatusFromTraps(property.type, property.damage);

        hasExplode = true;
        // Reset Timer
        timer = 0;

        // For debug
        if(property.type != TrapType.TIME_TRAP)
            Destroy(this.gameObject);
    }

    // when the trap has exced the duration
    void expired()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(currentPosition, property.radius, (1 << 8));

        if (hitColliders.Length > 0)
            foreach (Collider2D hitCollider in hitColliders)
                hitCollider.GetComponent<EnemyBehaviourTesting>().removeStatusFromTrap(property.type);

        Destroy(this.gameObject);
    }

    //activate
    public void activate()
    {
        isActive = true;
    }
}
