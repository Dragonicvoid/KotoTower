using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotoTowerGridBlocker : MonoBehaviour
{
    Point neighbor;

    // Blocks the area of koto tower
    private void Start()
    {
        Vector2 position = this.transform.position;

        // get x,y of the tower in the grid
        int x, y;
        GridCustom.getXYFromPosition(position, out x, out y);

        x += GridCustom.offsetX;
        y += GridCustom.offsetY;

        // blocks the inside of tower
        GridCustom.cells[x, y].cellContent = CellContent.KOTO_TOWER;
        Debug.DrawLine(GridCustom.getWorldSpace(x, y), GridCustom.getWorldSpace(x + 1, y), Color.blue, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x, y), GridCustom.getWorldSpace(x, y + 1), Color.blue, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x + 1, y), GridCustom.getWorldSpace(x + 1, y + 1), Color.blue, 100f, false);

        GridCustom.cells[x, y - 1].cellContent = CellContent.KOTO_TOWER;
        Debug.DrawLine(GridCustom.getWorldSpace(x, y - 1), GridCustom.getWorldSpace(x, y), Color.blue, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x, y - 1), GridCustom.getWorldSpace(x + 1, y - 1), Color.blue, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x + 1, y - 1), GridCustom.getWorldSpace(x + 1, y), Color.blue, 100f, false);

        GridCustom.cells[x - 1, y].cellContent = CellContent.KOTO_TOWER;
        Debug.DrawLine(GridCustom.getWorldSpace(x - 1, y), GridCustom.getWorldSpace(x, y), Color.blue, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x - 1, y), GridCustom.getWorldSpace(x - 1, y + 1), Color.blue, 100f, false);

        GridCustom.cells[x - 1, y - 1].cellContent = CellContent.KOTO_TOWER;
        Debug.DrawLine(GridCustom.getWorldSpace(x - 1, y - 1), GridCustom.getWorldSpace(x, y - 1), Color.blue, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x - 1, y - 1), GridCustom.getWorldSpace(x - 1, y), Color.blue, 100f, false);

        // blocks behind of the tower
        for (int i = x - 2; i >= 0; i--)
            for (int j = 0; j < GridCustom.height; j++)
            {
                GridCustom.cells[i, j].cellContent = CellContent.KOTO_TOWER;
                Debug.DrawLine(GridCustom.getWorldSpace(i, j), GridCustom.getWorldSpace(i + 1, j), Color.blue, 100f, false);
                Debug.DrawLine(GridCustom.getWorldSpace(i, j), GridCustom.getWorldSpace(i, j + 1), Color.blue, 100f, false);
                Debug.DrawLine(GridCustom.getWorldSpace(i + 1, j), GridCustom.getWorldSpace(i + 1, j + 1), Color.blue, 100f, false);
                Debug.DrawLine(GridCustom.getWorldSpace(i, j + 1), GridCustom.getWorldSpace(i + 1, j + 1), Color.blue, 100f, false);
            }

        // blocks top of the tower
        for (int i = x - 1, k = x - 1; i < (k + 4); i++)
            for (int j = y + 1; j < GridCustom.height; j++)
            {
                GridCustom.cells[i, j].cellContent = CellContent.KOTO_TOWER;
                Debug.DrawLine(GridCustom.getWorldSpace(i, j), GridCustom.getWorldSpace(i + 1, j), Color.blue, 100f, false);
                Debug.DrawLine(GridCustom.getWorldSpace(i, j), GridCustom.getWorldSpace(i, j + 1), Color.blue, 100f, false);
                Debug.DrawLine(GridCustom.getWorldSpace(i + 1, j), GridCustom.getWorldSpace(i + 1, j + 1), Color.blue, 100f, false);
                Debug.DrawLine(GridCustom.getWorldSpace(i, j + 1), GridCustom.getWorldSpace(i + 1, j + 1), Color.blue, 100f, false);
            }

        // get neighbor (usually right path)
        neighbor = this.GetComponent<Point>().getSecondIndexPoint();

        int xNeighbor, yNeighbor;
        GridCustom.getXYFromPosition(neighbor.getCurrPosition(), out xNeighbor, out yNeighbor);

        xNeighbor += GridCustom.offsetX;
        yNeighbor += GridCustom.offsetY;

        // blocks the path (usually right)
        for (int i = x + 1; i < (xNeighbor - 1); i++)
        {
            GridCustom.cells[i, y].cellContent = CellContent.PATH;
            Debug.DrawLine(GridCustom.getWorldSpace(i, y), GridCustom.getWorldSpace(i + 1, y), Color.gray, 100f, false);
            Debug.DrawLine(GridCustom.getWorldSpace(i, y), GridCustom.getWorldSpace(i, y + 1), Color.gray, 100f, false);
            Debug.DrawLine(GridCustom.getWorldSpace(i, y + 1), GridCustom.getWorldSpace(i + 1, y + 1), Color.gray, 100f, false);

            GridCustom.cells[i, y - 1].cellContent = CellContent.PATH;
            Debug.DrawLine(GridCustom.getWorldSpace(i, y - 1), GridCustom.getWorldSpace(i + 1, y - 1), Color.gray, 100f, false);
            Debug.DrawLine(GridCustom.getWorldSpace(i, y - 1), GridCustom.getWorldSpace(i, y), Color.gray, 100f, false);
        }
    }
}
