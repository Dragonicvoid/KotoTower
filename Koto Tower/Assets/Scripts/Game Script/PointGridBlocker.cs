using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGridBlocker : MonoBehaviour
{
    List<Point> neighbors;
    Direction direction;

    // create blockers for path
    private void Start()
    {
        Vector3 currPosition = this.transform.position;
        // block the area of move point
        int xPoint, yPoint;
        GridCustom.getXYFromPosition(currPosition, out xPoint, out yPoint);

        xPoint += GridCustom.offsetX;
        yPoint += GridCustom.offsetY;

        GridCustom.cells[xPoint, yPoint].cellContent = CellContent.PATH;
        Debug.DrawLine(GridCustom.getWorldSpace(xPoint, yPoint), GridCustom.getWorldSpace(xPoint + 1, yPoint), Color.gray, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(xPoint, yPoint), GridCustom.getWorldSpace(xPoint, yPoint + 1), Color.gray, 100f, false);

        GridCustom.cells[xPoint - 1, yPoint].cellContent = CellContent.PATH;
        Debug.DrawLine(GridCustom.getWorldSpace(xPoint - 1, yPoint), GridCustom.getWorldSpace(xPoint, yPoint), Color.gray, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(xPoint - 1, yPoint), GridCustom.getWorldSpace(xPoint - 1, yPoint + 1), Color.gray, 100f, false);

        GridCustom.cells[xPoint, yPoint - 1].cellContent = CellContent.PATH;
        Debug.DrawLine(GridCustom.getWorldSpace(xPoint, yPoint - 1), GridCustom.getWorldSpace(xPoint, yPoint), Color.gray, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(xPoint, yPoint - 1), GridCustom.getWorldSpace(xPoint + 1, yPoint - 1), Color.gray, 100f, false);

        GridCustom.cells[xPoint - 1, yPoint - 1].cellContent = CellContent.PATH;
        Debug.DrawLine(GridCustom.getWorldSpace(xPoint - 1, yPoint - 1), GridCustom.getWorldSpace(xPoint - 1, yPoint), Color.gray, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(xPoint - 1, yPoint - 1), GridCustom.getWorldSpace(xPoint, yPoint - 1), Color.gray, 100f, false);

        // Block the path to neighbor
        neighbors = new List<Point>(this.GetComponent<Point>().getAllNeighbor());

        foreach (Point neighbor in neighbors)
        {
            Vector3 neighborPosition = neighbor.getCurrPosition();

            int xNeighbor, yNeighbor;
            GridCustom.getXYFromPosition(neighborPosition, out xNeighbor, out yNeighbor);

            xNeighbor += GridCustom.offsetX;
            yNeighbor += GridCustom.offsetY;

            direction = checkDirection(xPoint, xNeighbor, yPoint, yNeighbor);

            // only draw a right and up path
            switch (direction)
            {
                case Direction.RIGHT:
                    // draw right path, if it is neighbor then the path iteration is different since its smaller
                    for (int i = xPoint + 1, k = (neighbor.getIsGenerator() ? xNeighbor : xNeighbor - 1); i < k; i++)
                    {
                        GridCustom.cells[i, yPoint].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridCustom.getWorldSpace(i, yPoint), GridCustom.getWorldSpace(i + 1, yPoint), Color.gray, 100f, false);
                        Debug.DrawLine(GridCustom.getWorldSpace(i, yPoint), GridCustom.getWorldSpace(i, yPoint + 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridCustom.getWorldSpace(i, yPoint + 1), GridCustom.getWorldSpace(i + 1, yPoint + 1), Color.gray, 100f, false);

                        GridCustom.cells[i, yPoint - 1].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridCustom.getWorldSpace(i, yPoint - 1), GridCustom.getWorldSpace(i + 1, yPoint - 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridCustom.getWorldSpace(i, yPoint - 1), GridCustom.getWorldSpace(i, yPoint), Color.gray, 100f, false);
                    }
                    break;
                case Direction.UP:
                    // draw upward path, if it is neighbor then the path iteration is different since its smaller
                    for (int i = yPoint + 1, k = (neighbor.getIsGenerator() ? yNeighbor : yNeighbor - 1); i < k; i++)
                    {
                        GridCustom.cells[xPoint, i].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint, i), GridCustom.getWorldSpace(xPoint + 1, i), Color.gray, 100f, false);
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint, i), GridCustom.getWorldSpace(xPoint, i + 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint, i + 1), GridCustom.getWorldSpace(xPoint + 1, i + 1), Color.gray, 100f, false);

                        GridCustom.cells[xPoint - 1, i].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint - 1, i), GridCustom.getWorldSpace(xPoint - 1, i + 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint - 1, i), GridCustom.getWorldSpace(xPoint, i), Color.gray, 100f, false);
                    }
                    break;
                case Direction.DOWN:
                    // draw downward path
                    for (int i = yPoint - 1; i > yNeighbor; i--)
                    {
                        GridCustom.cells[xPoint, i].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint, i), GridCustom.getWorldSpace(xPoint + 1, i), Color.gray, 100f, false);
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint, i), GridCustom.getWorldSpace(xPoint, i - 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint + 1, i), GridCustom.getWorldSpace(xPoint + 1, i - 1), Color.gray, 100f, false);

                        GridCustom.cells[xPoint - 1, i].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint - 1, i), GridCustom.getWorldSpace(xPoint, i), Color.gray, 100f, false);
                        Debug.DrawLine(GridCustom.getWorldSpace(xPoint - 1, i), GridCustom.getWorldSpace(xPoint - 1, i - 1), Color.gray, 100f, false);
                    }
                    break;
                default:
                    break;
            }

        }
    }

    // check the direction 1 is for curr, and 2 is for neighbor
    Direction checkDirection(int x1, int x2, int y1, int y2)
    {
        if (x1 < x2)
            return Direction.RIGHT;
        else if (y1 < y2)
            return Direction.UP;
        else if (x1 > x2)
            return Direction.LEFT;
        else if (y1 > y2)
            return Direction.DOWN;
        else
            return Direction.NONE;
    }
}
