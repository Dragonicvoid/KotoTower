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

public class ArrowsDirection : MonoBehaviour, IPointerClickHandler
{
    // Attribute
    public Direction direction;
    // Direction manager
    ChooseDirection chooseDirection;
    // Next point for this direction
    Point point;

    // Initialization
    private void Start()
    {
        chooseDirection = GetComponentInParent<ChooseDirection>();
    }

    // setter getter
    public void setPoint(Point point)
    {
        this.point = point;
    }

    public Point GetPoint()
    {
        return this.point;
    }

    // When click send a message to the direction manager (Choose Direction class)
    public void OnPointerClick(PointerEventData eventData)
    {
        chooseDirection.moveTruckTo(this);
    }
}
