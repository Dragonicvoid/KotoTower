using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBehaviourTesting : MonoBehaviour
{
    // The Koto Tower
    [SerializeField] PointTesting kotoTower;

    // The generator
    [SerializeField] GeneratorBehaviourTesting generator;

    // Enemy attribute
    [SerializeField] float speed;
    [SerializeField] float maxHealth = 10;
    float currHealth;
    PointTesting currTarget;
    Vector2 position;
    Renderer render;
    Color defaultColor;
    public bool isDead;

    // material id name to optimize color material
    int colorPropertyId = Shader.PropertyToID("_Color");

    // timer and bool for hit detection
    float hitTime;
    bool isHit;

    // charCharge (Answer) that the truck bring
    AnswerTesting charCharge;

    private void OnEnable()
    {
        // Reseting health
        resetAttribute();
    }

    // use start since the object is not active at the begining
    private void Start()
    {
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
        currTarget = point.getSecondIndexPoint();
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
        this.transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        position = this.gameObject.transform.position;

        if (isNowAt(point.getCurrPosition()) && point.getIsGenerator())
        {
            // Check if the answer you give is correct answer and despawn the truck
            generator.checkAnswer(charCharge);
            despawn();
        }
        else if (isNowAt(point.getCurrPosition()))
        {
            // If the agent reach a destination that is not end point
            changeTargetFromCurrPoint(currTarget);
        }
    }

    // to get this object position since it keeps moving
    public Vector2 getPosition()
    {
        return this.position;
    }

    // Set the charCharge (Answer) that the truck brings
    public void setCharCharge(AnswerTesting charCharge)
    {
        this.charCharge = charCharge;
    }

    // Checking if the agent is at a point
    bool isNowAt(Vector2 point)
    {
        if (Vector2.Distance(point, position) < 0.1f)
            return true;

        return false;
    }
    
    // To reset the timer when you got hit
    void resetHit()
    {
        // If it is hit then add the timer
        if (isHit)
            hitTime += Time.deltaTime;

        // If it passed 0.2 second then bring it back to normal
        if (hitTime >= 0.2f)
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
