using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTesting : MonoBehaviour
{
    // Point for spawn, target, tower, and everything else
    [SerializeField] List<PointTesting> neighbor = new List<PointTesting>();
    [SerializeField] bool isStartPoint = false;
    [SerializeField] bool isEndPoint = false;
    [SerializeField] bool isGenerator = false;
    [SerializeField] bool isBranchingPath = false;
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

    // Get all possible path for the truck if there are more than one path
    // the index starts at 1 since 0 is for enemy path
    public List<PointTesting> getAllPossiblePathForTruck()
    {
        List<PointTesting> list = new List<PointTesting>(neighbor.GetRange(1, neighbor.Count - 1));
        return list;
    }

    // Get all path for blocking the grid
    public List<PointTesting> getAllNeighbor()
    {
        List<PointTesting> list = new List<PointTesting>(neighbor.GetRange(0, neighbor.Count));
        return list;
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

    // Check if this is branching path for the truck (player have to choose the path)
    public bool getIsBranchingPath()
    {
        return this.isBranchingPath;
    }

    // Check if this is start point for enemy, when they got activated for the first time
    public bool getIsStartPoint()
    {
        return this.isStartPoint;
    }
}
