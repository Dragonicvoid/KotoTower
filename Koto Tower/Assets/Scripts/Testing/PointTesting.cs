using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTesting : MonoBehaviour
{
    // Point for spawn, target, tower, and everything else
    [SerializeField] List<PointTesting> neighbor = new List<PointTesting>();
    [SerializeField] bool isEndPoint = false;
    [SerializeField] bool isGenerator = false;
    Transform currPosition;

    // Get its position
    private void Awake()
    {
        this.currPosition = this.transform;
    }

    // Getting this point position
    public Vector2 getCurrPosition()
    {
        return this.currPosition.position;
    }

    // This is for enemy path
    public PointTesting getFirstIndexPoint()
    {
        return this.neighbor[0];
    }

    // This is for Truck path
    public PointTesting getSecondIndexPoint()
    {
        return this.neighbor[1];
    }

    // Check if this is final point for the enemy (Koto Tower)
    public bool getIsEndPoint()
    {
        return this.isEndPoint;
    }

    // Check if this is final point for the truck (Generator)
    public bool getIsGenerator()
    {
        return this.isGenerator;
    }
}
