using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTesting : MonoBehaviour
{
    // Enemy attribute
    [SerializeField] EnemyPropertiesScriptableObject property = null;
    EnemiesPoolingTesting pooler = null;
    float shockTimer;
    float attackTimer;
    float currHealth;
    bool conditionChanged;
    PointTesting currTarget;
    Vector2 position;
    Renderer render;
    Color defaultColor;
    public EnemyStatus status;

    // koto Tower
    KotoTowerBehaviourTesting kotoTower;

    // material id name to optimize color material
    int colorPropertyId = Shader.PropertyToID("_Color");

    //timer and bool for hit detection
    float hitTime;
    bool isHit;

    private void OnEnable()
    {
        // Reseting health
        resetAttribute();
        // reset hit since it just spawn
        hitTime = 0f;
        isHit = false;
        render.material.SetColor(colorPropertyId, defaultColor);
    }

    // use start since the object is not active at the begining
    private void Awake()
    {
        // to get renderer and default color
        render = this.GetComponent<Renderer>();
        pooler = this.transform.GetComponentInParent<EnemiesPoolingTesting>();
        defaultColor = render.material.GetColor(colorPropertyId);
        // reset health
        resetAttribute();
    }

    // Call Update method
    private void Update()
    {
        // if it is not frozen then it is still actively moving
        if (status != EnemyStatus.FROZEN)
        {
            if (status != EnemyStatus.MOVING)
                // Moving the target
                moveTo(currTarget);
            else
                attackKotoTower();

            // Reset attribute when not hitted
            resetHit();
        }
        // Change color based on condition
        if(conditionChanged)
            render.material.SetColor(colorPropertyId, colorBaseOnCondition());
    }

    // Spawning the agent from a point
    public void spawn(Vector2 pointPosition)
    {
        this.transform.position = pointPosition;
        position = pointPosition;
        status = EnemyStatus.MOVING;
        this.gameObject.SetActive(true);
    }

    // Despawning the enemy
    public void despawn()
    {
        status = EnemyStatus.DEAD;
        this.gameObject.SetActive(false);
    }

    // A method to change current targeted point so the agent can move to the point
    public void changeTargetFromCurrPoint(PointTesting point)
    {
        currTarget = point.getFirstIndexPoint();
    }

    // A method to change this agent parent, for the sake of tidiness for developer
    public void changeParent(Transform parent)
    {
        this.transform.parent = parent;
    }

    // Moving agent to a point
    public void moveTo(PointTesting point)
    {
        // Moving is now using translate to make the game more consistent
        Vector2 dir = currTarget.getCurrPosition() - position;

        // half the movement speed if the enemy are shocked, stop when freeze or stopped
        this.transform.Translate(dir.normalized * property.speed * Time.deltaTime
            * (shockTimer > 0 ? 0.5f : 1f)
            * (status == EnemyStatus.STOPPED || status == EnemyStatus.FROZEN ? 0f : 1f), Space.World);

        position = this.gameObject.transform.position;

        if (isNowAt(point.getCurrPosition()) && point.getIsStartPoint())
        {
            // activate the enemy (change layer)
            activate();
        }
        if (isNowAt(point.getCurrPosition()) && point.getIsEndPoint())
        {
            // attack
            attackKotoTower();
        }
        else if (isNowAt(point.getCurrPosition()))
        {
            // If the agent reach a destination that is not end point
            changeTargetFromCurrPoint(currTarget);
        }
    }

    // attack kotoTower
    void attackKotoTower()
    {
        if (attackTimer < property.hitRate && status == EnemyStatus.ATTACKING)
            attackTimer += Time.deltaTime;

        if(status != EnemyStatus.ATTACKING && status == EnemyStatus.MOVING)
        {
            status = EnemyStatus.ATTACKING;
            kotoTower = GameObject.FindGameObjectWithTag("Koto Tower").GetComponent<KotoTowerBehaviourTesting>();
        }
        else if(attackTimer > property.hitRate)
        {
            kotoTower.addDamage(property.damage);
            attackTimer = 0f;
        }
    }

    // Checking if the agent is at a point
    bool isNowAt(Vector2 point)
    {
        if (Vector2.Distance(point, position) < 0.1f)
            return true;

        return false;
    }

    // Used for initialization on spawn
    public void changePooler(EnemiesPoolingTesting pooler)
    {
        this.pooler = pooler;
    }

    // give status according to the trap type
    public void addStatusFromTraps(TrapType type, float damage = 0f)
    {
        switch (type)
        {
            case TrapType.BOMB_TRAP:
                addDamage(damage, TowerType.SNIPER); // the bomb has the same hit attribute as the sniper
                break;
            case TrapType.FREEZE_TRAP:
                status = EnemyStatus.FROZEN;
                conditionChanged = true;
                break;
            case TrapType.TIME_TRAP:
                if(status != EnemyStatus.FROZEN)
                {
                    status = EnemyStatus.STOPPED;
                    conditionChanged = true;
                }
                break;
            default:
                break;
        }

    }

    // remove a certain status if called
    public void removeStatusFromTrap(TrapType type)
    {
        switch (type)
        {
            case TrapType.TIME_TRAP:
                if(status != EnemyStatus.FROZEN)
                {
                    status = EnemyStatus.MOVING;
                    conditionChanged = true;
                }
                break;
            default:
                break;
        }
    }

    // Method when enemy got hit
    public void addDamage(float damage, TowerType tower)
    {
        // to indicate hitting, damage must be more than 0
        if (damage > 0)
        {
            hitTime = 0f;
            if (property.type == EnemyType.ARMORED && tower != TowerType.SNIPER)
                damage *= 0.25f;
            currHealth -= damage;
            isHit = true;
        }

        // if enemy got shocked
        if(tower == TowerType.ELECTRIC)
            shockTimer = property.shockRecovery;

        // if it dies just put it to the pooler
        if (currHealth <= 0f || status == EnemyStatus.FROZEN)
        {
            pooler.insertBack(this);
            GameManager.addMoney(property.rewardPrice);
        }

        conditionChanged = true;
    }

    // to get this object position since it keeps moving
    public Vector2 getPosition()
    {
        return this.position;
    }

    // To reset the timer when you got hit
    void resetHit()
    {
        // If it is hit then add the timer
        if (isHit)
            hitTime += Time.deltaTime;

        // Counting when the enemy are shocked
        if (shockTimer > 0f)
            shockTimer -= Time.deltaTime;
        else
            conditionChanged = true;    

        // If it passed 0.2 second then bring it back to normal
        if(hitTime >= 0.2f)
        {
            hitTime = 0f;
            isHit = false;
            conditionChanged = true;
        }
    }

    // get color base on condition
    Color colorBaseOnCondition()
    {
        conditionChanged = false;

        if (isHit) // got hit
            return Color.red;
        else if (status == EnemyStatus.FROZEN) // frozen status
            return Color.blue;
        else if (status == EnemyStatus.STOPPED) // stopped status
            return new Color(1, 0, 1);
        else if (shockTimer > 0f) // shock status
            return Color.cyan;
        else // nothing happen
            return defaultColor;
    }

    // activate the enemy
    void activate()
    {
        this.gameObject.layer = 8;
    }

    // Reseting other attribute like health, and many more
    void resetAttribute()
    {
        currHealth = property.maxHealth;
        conditionChanged = true;
        status = EnemyStatus.MOVING;
        shockTimer = 0f;
        isHit = false;
        hitTime = 0f;
        conditionChanged = true;
    }
}
