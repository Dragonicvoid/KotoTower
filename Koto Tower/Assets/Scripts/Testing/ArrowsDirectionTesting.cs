using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Direction
{
    UP,
    RIGHT,
    DOWN,
    LEFT,
    NONE
}

public class ArrowsDirectionTesting : MonoBehaviour, IPointerClickHandler
{
    // Attribute
    public Direction direction;
    // Direction manager
    ChooseDirectionTesting chooseDirection;
    // Next point for this direction
    PointTesting point;

    // Initialization
    private void Start()
    {
        chooseDirection = GetComponentInParent<ChooseDirectionTesting>();
    }

    // setter getter
    public void setPoint(PointTesting point)
    {
        this.point = point;
    }

    public PointTesting GetPoint()
    {
        return this.point;
    }

    // When click send a message to the direction manager (Choose Direction class)
    public void OnPointerClick(PointerEventData eventData)
    {
        chooseDirection.moveTruckTo(this);
    }
}
