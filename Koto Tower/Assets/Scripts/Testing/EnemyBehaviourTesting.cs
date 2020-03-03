using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTesting : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] EnemiesPoolingTesting pooler;
    PointTesting currTarget;
    Vector2 position;
    float startTime;
    float timerPassed;
    float currJourneyLenght;

    private void OnEnable()
    {
        // to get startTime when it was activated again
        // using timerPassed to avoid floating point error
        resetTimer();
    }

    private void Start()
    {
        // to get 0.0 s
        resetTimer();
    }

    private void Update()
    {
        moveTo(currTarget);
    }

    // Spawning the agent from a point
    public void spawn(Vector2 pointPosition)
    {
        this.transform.position = pointPosition;
        position = pointPosition;
        this.gameObject.SetActive(true);
    }

    // Despawning the enemy
    public void despawn()
    {
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

    // Can be use to reset timer everytime we reach a destination
    void resetTimer()
    {
        startTime = 0f;
        timerPassed = 0f;
    }
}
