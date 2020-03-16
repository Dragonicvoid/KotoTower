using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGridBlocker : MonoBehaviour
{
    List<PointTesting> neighbors;
    Direction direction;

    // create blockers for path
    private void Start()
    {
        Vector3 currPosition = this.transform.position;
        // block the area of move point
        int xPoint, yPoint;
        GridTesting.getXYFromPosition(currPosition, out xPoint, out yPoint);

        xPoint += GridTesting.offsetX;
        yPoint += GridTesting.offsetY;

        GridTesting.cells[xPoint, yPoint].cellContent = CellContent.PATH;
        Debug.DrawLine(GridTesting.getWorldSpace(xPoint, yPoint), GridTesting.getWorldSpace(xPoint + 1, yPoint), Color.gray, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(xPoint, yPoint), GridTesting.getWorldSpace(xPoint, yPoint + 1), Color.gray, 100f, false);

        GridTesting.cells[xPoint - 1, yPoint].cellContent = CellContent.PATH;
        Debug.DrawLine(GridTesting.getWorldSpace(xPoint - 1, yPoint), GridTesting.getWorldSpace(xPoint, yPoint), Color.gray, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(xPoint - 1, yPoint), GridTesting.getWorldSpace(xPoint - 1, yPoint + 1), Color.gray, 100f, false);

        GridTesting.cells[xPoint, yPoint - 1].cellContent = CellContent.PATH;
        Debug.DrawLine(GridTesting.getWorldSpace(xPoint, yPoint - 1), GridTesting.getWorldSpace(xPoint, yPoint), Color.gray, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(xPoint, yPoint - 1), GridTesting.getWorldSpace(xPoint + 1, yPoint - 1), Color.gray, 100f, false);

        GridTesting.cells[xPoint - 1, yPoint - 1].cellContent = CellContent.PATH;
        Debug.DrawLine(GridTesting.getWorldSpace(xPoint - 1, yPoint - 1), GridTesting.getWorldSpace(xPoint - 1, yPoint), Color.gray, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(xPoint - 1, yPoint - 1), GridTesting.getWorldSpace(xPoint, yPoint - 1), Color.gray, 100f, false);

        // Block the path to neighbor
        neighbors = new List<PointTesting>(this.GetComponent<PointTesting>().getAllNeighbor());

        foreach (PointTesting neighbor in neighbors)
        {
            Vector3 neighborPosition = neighbor.getCurrPosition();

            int xNeighbor, yNeighbor;
            GridTesting.getXYFromPosition(neighborPosition, out xNeighbor, out yNeighbor);

            xNeighbor += GridTesting.offsetX;
            yNeighbor += GridTesting.offsetY;

            direction = checkDirection(xPoint, xNeighbor, yPoint, yNeighbor);

            // only draw a right and up path
            switch (direction)
            {
                case Direction.RIGHT:
                    // draw right path, if it is neighbor then the path iteration is different since its smaller
                    for (int i = xPoint + 1, k = (neighbor.getIsGenerator() ? xNeighbor : xNeighbor - 1); i < k; i++)
                    {
                        GridTesting.cells[i, yPoint].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridTesting.getWorldSpace(i, yPoint), GridTesting.getWorldSpace(i + 1, yPoint), Color.gray, 100f, false);
                        Debug.DrawLine(GridTesting.getWorldSpace(i, yPoint), GridTesting.getWorldSpace(i, yPoint + 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridTesting.getWorldSpace(i, yPoint + 1), GridTesting.getWorldSpace(i + 1, yPoint + 1), Color.gray, 100f, false);

                        GridTesting.cells[i, yPoint - 1].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridTesting.getWorldSpace(i, yPoint - 1), GridTesting.getWorldSpace(i + 1, yPoint - 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridTesting.getWorldSpace(i, yPoint - 1), GridTesting.getWorldSpace(i, yPoint), Color.gray, 100f, false);
                    }
                    break;
                case Direction.UP:
                    // draw upward path, if it is neighbor then the path iteration is different since its smaller
                    for (int i = yPoint + 1, k = (neighbor.getIsGenerator() ? yNeighbor : yNeighbor - 1); i < k; i++)
                    {
                        GridTesting.cells[xPoint, i].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint, i), GridTesting.getWorldSpace(xPoint + 1, i), Color.gray, 100f, false);
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint, i), GridTesting.getWorldSpace(xPoint, i + 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint, i + 1), GridTesting.getWorldSpace(xPoint + 1, i + 1), Color.gray, 100f, false);

                        GridTesting.cells[xPoint - 1, i].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint - 1, i), GridTesting.getWorldSpace(xPoint - 1, i + 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint - 1, i), GridTesting.getWorldSpace(xPoint, i), Color.gray, 100f, false);
                    }
                    break;
                case Direction.DOWN:
                    // draw downward path
                    for (int i = yPoint - 1; i > yNeighbor; i--)
                    {
                        GridTesting.cells[xPoint, i].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint, i), GridTesting.getWorldSpace(xPoint + 1, i), Color.gray, 100f, false);
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint, i), GridTesting.getWorldSpace(xPoint, i - 1), Color.gray, 100f, false);
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint + 1, i), GridTesting.getWorldSpace(xPoint + 1, i - 1), Color.gray, 100f, false);

                        GridTesting.cells[xPoint - 1, i].cellContent = CellContent.PATH;
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint - 1, i), GridTesting.getWorldSpace(xPoint, i), Color.gray, 100f, false);
                        Debug.DrawLine(GridTesting.getWorldSpace(xPoint - 1, i), GridTesting.getWorldSpace(xPoint - 1, i - 1), Color.gray, 100f, false);
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
