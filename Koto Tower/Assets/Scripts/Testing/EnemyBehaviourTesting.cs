using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTesting : MonoBehaviour
{
    // Enemy attribute
    [SerializeField] float speed;
    [SerializeField] EnemiesPoolingTesting pooler;
    [SerializeField] float maxHealth = 10;
    float currHealth;
    PointTesting currTarget;
    Vector2 position;
    Renderer render;
    Color defaultColor;
    public bool isDead;

    // material id name to optimize color material
    int colorPropertyId = Shader.PropertyToID("_Color");

    // timer for movement distance
    float startTime;
    float timerPassed;
    float currJourneyLenght;

    //timer and bool for hit detection
    float hitTime;
    bool isHit;

    private void OnEnable()
    {
        // to get startTime when it was activated again
        // using timerPassed to avoid floating point error
        resetTimer();
        // Reseting health
        resetAttribute();
    }

    // use start since the object is not active at the begining
    private void Start()
    {
        // to get 0.0 s
        resetTimer();
        // to get renderer and default color
        render = this.GetComponent<Renderer>();
        defaultColor = render.material.GetColor(colorPropertyId);
        // reset health
        resetAttribute();
    }

    // Call Update method
    private void Update()
    {
        // Moving the target
        moveTo(currTarget);
        // Reset color when not hitted
        resetHit();
    }

    // Spawning the agent from a point
    public void spawn(Vector2 pointPosition)
    {
        this.transform.position = pointPosition;
        position = pointPosition;
        isDead = false;
        this.gameObject.SetActive(true);
    }

    // Despawning the enemy
    public void despawn()
    {
        isDead = true;
        this.gameObject.SetActive(false);
    }

    // A method to change current targeted point so the agent can move to the point
    public void changeTargetFromCurrPoint(PointTesting point)
    {
        currTarget = point.getFirstIndexPoint();
        currJourneyLenght = Vector2.Distance(position, currTarget.getCurrPosition());
        resetTimer();
    }

    // A method to change this agent parent, for the sake of tidiness for developer
    public void changeParent(Transform parent)
    {
        this.transform.parent = parent;
    }

    // Moving agent to a point
    public void moveTo(PointTesting point)
    {
        timerPassed += Time.deltaTime;
        // Moving the target using calculated fraction of current position and target
        float distCovered = (timerPassed - startTime) * speed;
        float fractionOfJourney = distCovered / currJourneyLenght;
        Vector2 currPos = this.gameObject.transform.position = Vector2.LerpUnclamped(position, currTarget.getCurrPosition(), fractionOfJourney);
        position = currPos;

        if (isNowAt(point.getCurrPosition()) && point.getIsEndPoint())
        {
            // Just for testing, checking if current position is the end point
            // so the agent can be disabled and put it on another parent (for the sake of tidiness)
            pooler.insertBack(this);
        }
        else if (isNowAt(point.getCurrPosition()))
        {
            // If the agent reach a destination that is not end point
            changeTargetFromCurrPoint(currTarget);
        }
    }

    // Checking if the agent is at a point
    bool isNowAt(Vector2 point)
    {
        if ( Mathf.Abs(Vector2.Distance(point, position)) < 0.1f)
            return true;

        return false;
    }

    // Used for initialization on spawn
    public void changePooler(EnemiesPoolingTesting pooler)
    {
        this.pooler = pooler;
    }

    // Method when enemy got hit
    public void addDamage(float damage)
    {
        render.material.SetColor(colorPropertyId, Color.red);
        hitTime = 0f;
        currHealth -= damage;
        isHit = true;

        if (currHealth <= 0f)
            pooler.insertBack(this);
    }

    // to get this object position since it keeps moving
    public Vector2 getPosition()
    {
        return this.position;
    }

    // Can be use to reset timer everytime we reach a destination
    void resetTimer()
    {
        startTime = 0f;
        timerPassed = 0f;
    }

    // To reset the timer when you got hit
    void resetHit()
    {
        // If it is hit then add the timer
        if (isHit)
            hitTime += Time.deltaTime;

        // If it passed 0.2 second then bring it back to normal
        if(hitTime >= 0.2f)
        {
            hitTime = 0f;
            isHit = false;
            render.material.SetColor(colorPropertyId, defaultColor);
        }
    }

    // Reseting other attribute like health, and many more
    void resetAttribute()
    {
        currHealth = maxHealth;
    }
}
