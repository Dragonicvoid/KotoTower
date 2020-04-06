using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDirection: MonoBehaviour
{
    // the truck
    TruckBehaviour truck;
    // The Arrows
    List<ArrowsDirection> arrows;
    // All position for possible path
    List<Point> allPossiblePath;

    // Initialization
    void Start()
    {
        truck = this.transform.parent.GetComponent<TruckBehaviour>();
        arrows = new List<ArrowsDirection>(this.GetComponentsInChildren<ArrowsDirection>(true));
    }

    public void openDirectionsFromPoint(Point point, Vector2 truckPosition)
    {
        // Get the possible path for Truck (index from 1 since 0 is for enemy path)
        allPossiblePath = point.getAllPossiblePathForTruck();

        foreach (Point path in allPossiblePath)
        {
            Vector2 deltaVector = path.getCurrPosition() - truckPosition;

            // there is a path on the right
            if (deltaVector.x >= 0.5f)
                openDirection(Direction.RIGHT, path);

            if (deltaVector.y >= 0.5f)
                openDirection(Direction.UP, path);
            else if (deltaVector.y <= -0.5f)
                openDirection(Direction.DOWN, path);
        }
    }

    // Send message to the truck where to move
    public void moveTruckTo(ArrowsDirection arrow)
    {
        closeAllPossiblePath();
        truck.choosePath(arrow.GetPoint());
    }

    // open a direction and add the coresponding point to that direction
    void openDirection(Direction direction, Point path)
    {
        foreach (ArrowsDirection arrow in arrows)
        {
            if(arrow.direction == direction)
            {
                arrow.gameObject.SetActive(true);
                arrow.setPoint(path);
                break;
            }
        }
    }
    
    // close all direction when the truck start moving again
    public void closeAllPossiblePath()
    {
        foreach (ArrowsDirection arrow in arrows)
            arrow.gameObject.SetActive(false);
    }
}
