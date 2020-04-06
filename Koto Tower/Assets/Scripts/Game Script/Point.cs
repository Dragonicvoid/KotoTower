using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    // Point for spawn, target, tower, and everything else
    [SerializeField] List<Point> neighbor = new List<Point>();
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
    public Point getFirstIndexPoint()
    {
        return this.neighbor[0];
    }

    // This is for Truck path
    public Point getSecondIndexPoint()
    {
        return this.neighbor[1];
    }

    // Get all possible path for the truck if there are more than one path
    // the index starts at 1 since 0 is for enemy path
    public List<Point> getAllPossiblePathForTruck()
    {
        List<Point> list = new List<Point>(neighbor.GetRange(1, neighbor.Count - 1));
        return list;
    }

    // Get all path for blocking the grid
    public List<Point> getAllNeighbor()
    {
        List<Point> list = new List<Point>(neighbor.GetRange(0, neighbor.Count));
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
