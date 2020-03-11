using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseDirectionTesting : MonoBehaviour
{
    // the truck
    TruckBehaviourTesting truck;
    // The Arrows
    List<ArrowsDirectionTesting> arrows;
    // All position for possible path
    List<PointTesting> allPossiblePath;

    // Initialization
    void Start()
    {
        truck = this.transform.parent.GetComponent<TruckBehaviourTesting>();
        arrows = new List<ArrowsDirectionTesting>(this.GetComponentsInChildren<ArrowsDirectionTesting>(true));
    }

    public void openDirectionsFromPoint(PointTesting point, Vector2 truckPosition)
    {
        // Get the possible path for Truck (index from 1 since 0 is for enemy path)
        allPossiblePath = point.getAllPossiblePathForTruck();

        foreach (PointTesting path in allPossiblePath)
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
    public void moveTruckTo(ArrowsDirectionTesting arrow)
    {
        closeAllPossiblePath();
        truck.choosePath(arrow.GetPoint());
    }

    // open a direction and add the coresponding point to that direction
    void openDirection(Direction direction, PointTesting path)
    {
        foreach (ArrowsDirectionTesting arrow in arrows)
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
    void closeAllPossiblePath()
    {
        foreach (ArrowsDirectionTesting arrow in arrows)
            arrow.gameObject.SetActive(false);
    }
}
