using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTesting : MonoBehaviour
{
    [SerializeField] List<PointTesting> neighbor;
    [SerializeField] bool isEndPoint;
    Transform currPosition;

    private void Start()
    {
        currPosition = this.transform;
    }

    public Vector2 getCurrPosition()
    {
        return currPosition.position;
    }

    public PointTesting getFirstIndexPoint()
    {
        return neighbor[0];
    }

    public bool getIsEndPoint()
    {
        return isEndPoint;
    }
}
