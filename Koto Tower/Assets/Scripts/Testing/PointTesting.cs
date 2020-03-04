using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTesting : MonoBehaviour
{
    // Point for spawn, target, tower, and everything else
    [SerializeField] List<PointTesting> neighbor = new List<PointTesting>();
    [SerializeField] bool isEndPoint = false;
    Transform currPosition;

    // Get its position
    private void Start()
    {
        currPosition = this.transform;
    }

    // Getting this point position
    public Vector2 getCurrPosition()
    {
        return currPosition.position;
    }

    // This is for enemy path
    public PointTesting getFirstIndexPoint()
    {
        return neighbor[0];
    }

    // Check if this is final point for the enemy
    public bool getIsEndPoint()
    {
        return isEndPoint;
    }
}
