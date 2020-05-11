using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsBehaviour : MonoBehaviour
{
    // attribute
    [SerializeField] TrapPropertiesScriptableObject property = null;
    // when buying tower, show the decrease money
    [SerializeField] GameObject moneyAdd = null;

    // other variables for explosion
    bool hasExplode;
    bool isActive;
    Vector3 currentPosition;
    float timer;

    // audio source
    AudioSource audioSource;

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
        audioSource = this.GetComponent<AudioSource>();
        hasExplode = false;
        isActive = false;
        timer = 0f;
        currentPosition = this.transform.position;
    }

    // Create gizmos so we can see the tower range when selected
    void OnDrawGizmosSelected()
    {
        switch (property.type)
        {
            case TrapType.BOMB_TRAP:
                Gizmos.color = new Color(1, 1, 1, 0.20f);
                Gizmos.DrawSphere(this.transform.position, property.radius);
                break;
            case TrapType.TIME_TRAP:
                Gizmos.color = new Color(0, 0.75f, 0.75f, 0.20f);
                Gizmos.DrawSphere(this.transform.position, property.radius);
                break;
            case TrapType.FREEZE_TRAP:
                Gizmos.color = new Color(0, 0, 0.75f, 0.20f);
                Gizmos.DrawSphere(this.transform.position, property.radius);
                break;
        }
    }

    // countdown to explosion
    private void Update()
    {
        if (isActive && !GameManager.instance.isPaused)
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
                    hitCollider.GetComponent<EnemyBehaviour>().addStatusFromTraps(property.type, property.damage);

            yield return new WaitForSeconds(1f);
        }
    }

    // when the timer ends, it explode
    void explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(currentPosition, property.radius, (1 << 8));

        if (hitColliders.Length > 0)
            foreach (Collider2D hitCollider in hitColliders)
                hitCollider.GetComponent<EnemyBehaviour>().addStatusFromTraps(property.type, property.damage);

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
                hitCollider.GetComponent<EnemyBehaviour>().removeStatusFromTrap(property.type);

        Destroy(this.gameObject);
    }

    //activate
    public void activate()
    {
        GameObject moneyAddObj = Instantiate(moneyAdd);
        MoneyAddedBehaviour moneyAddBehave = moneyAddObj.GetComponent<MoneyAddedBehaviour>();
        currentPosition = this.gameObject.transform.position;
        isActive = true;
        moneyAddBehave.activateMinus((int)GameManager.instance.getPrice(property.type), currentPosition);
    }

    // get is active
    public bool getIsActive()
    {
        return this.isActive;
    }
}
