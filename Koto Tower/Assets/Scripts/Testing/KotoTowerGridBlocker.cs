using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KotoTowerGridBlocker : MonoBehaviour
{
    PointTesting neighbor;

    // Blocks the area of koto tower
    private void Start()
    {
        Vector2 position = this.transform.position;

        // get x,y of the tower in the grid
        int x, y;
        GridTesting.getXYFromPosition(position, out x, out y);

        x += GridTesting.offsetX;
        y += GridTesting.offsetY;

        // blocks the inside of tower
        GridTesting.cells[x, y].cellContent = CellContent.KOTO_TOWER;
        Debug.DrawLine(GridTesting.getWorldSpace(x, y), GridTesting.getWorldSpace(x + 1, y), Color.blue, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x, y), GridTesting.getWorldSpace(x, y + 1), Color.blue, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x + 1, y), GridTesting.getWorldSpace(x + 1, y + 1), Color.blue, 100f, false);

        GridTesting.cells[x, y - 1].cellContent = CellContent.KOTO_TOWER;
        Debug.DrawLine(GridTesting.getWorldSpace(x, y - 1), GridTesting.getWorldSpace(x, y), Color.blue, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x, y - 1), GridTesting.getWorldSpace(x + 1, y - 1), Color.blue, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x + 1, y - 1), GridTesting.getWorldSpace(x + 1, y), Color.blue, 100f, false);

        GridTesting.cells[x - 1, y].cellContent = CellContent.KOTO_TOWER;
        Debug.DrawLine(GridTesting.getWorldSpace(x - 1, y), GridTesting.getWorldSpace(x, y), Color.blue, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x - 1, y), GridTesting.getWorldSpace(x - 1, y + 1), Color.blue, 100f, false);

        GridTesting.cells[x - 1, y - 1].cellContent = CellContent.KOTO_TOWER;
        Debug.DrawLine(GridTesting.getWorldSpace(x - 1, y - 1), GridTesting.getWorldSpace(x, y - 1), Color.blue, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x - 1, y - 1), GridTesting.getWorldSpace(x - 1, y), Color.blue, 100f, false);

        // blocks behind of the tower
        for (int i = x - 2; i >= 0; i--)
            for (int j = 0; j < GridTesting.height; j++)
            {
                GridTesting.cells[i, j].cellContent = CellContent.KOTO_TOWER;
                Debug.DrawLine(GridTesting.getWorldSpace(i, j), GridTesting.getWorldSpace(i + 1, j), Color.blue, 100f, false);
                Debug.DrawLine(GridTesting.getWorldSpace(i, j), GridTesting.getWorldSpace(i, j + 1), Color.blue, 100f, false);
                Debug.DrawLine(GridTesting.getWorldSpace(i + 1, j), GridTesting.getWorldSpace(i + 1, j + 1), Color.blue, 100f, false);
                Debug.DrawLine(GridTesting.getWorldSpace(i, j + 1), GridTesting.getWorldSpace(i + 1, j + 1), Color.blue, 100f, false);
            }

        // blocks top of the tower
        for (int i = x - 1, k = x - 1; i < (k + 4); i++)
            for (int j = y + 1; j < GridTesting.height; j++)
            {
                GridTesting.cells[i, j].cellContent = CellContent.KOTO_TOWER;
                Debug.DrawLine(GridTesting.getWorldSpace(i, j), GridTesting.getWorldSpace(i + 1, j), Color.blue, 100f, false);
                Debug.DrawLine(GridTesting.getWorldSpace(i, j), GridTesting.getWorldSpace(i, j + 1), Color.blue, 100f, false);
                Debug.DrawLine(GridTesting.getWorldSpace(i + 1, j), GridTesting.getWorldSpace(i + 1, j + 1), Color.blue, 100f, false);
                Debug.DrawLine(GridTesting.getWorldSpace(i, j + 1), GridTesting.getWorldSpace(i + 1, j + 1), Color.blue, 100f, false);
            }

        // get neighbor (usually right path)
        neighbor = this.GetComponent<PointTesting>().getSecondIndexPoint();

        int xNeighbor, yNeighbor;
        GridTesting.getXYFromPosition(neighbor.getCurrPosition(), out xNeighbor, out yNeighbor);

        xNeighbor += GridTesting.offsetX;
        yNeighbor += GridTesting.offsetY;

        // blocks the path (usually right)
        for (int i = x + 1; i < (xNeighbor - 1); i++)
        {
            GridTesting.cells[i, y].cellContent = CellContent.PATH;
            Debug.DrawLine(GridTesting.getWorldSpace(i, y), GridTesting.getWorldSpace(i + 1, y), Color.gray, 100f, false);
            Debug.DrawLine(GridTesting.getWorldSpace(i, y), GridTesting.getWorldSpace(i, y + 1), Color.gray, 100f, false);
            Debug.DrawLine(GridTesting.getWorldSpace(i, y + 1), GridTesting.getWorldSpace(i + 1, y + 1), Color.gray, 100f, false);

            GridTesting.cells[i, y - 1].cellContent = CellContent.PATH;
            Debug.DrawLine(GridTesting.getWorldSpace(i, y - 1), GridTesting.getWorldSpace(i + 1, y - 1), Color.gray, 100f, false);
            Debug.DrawLine(GridTesting.getWorldSpace(i, y - 1), GridTesting.getWorldSpace(i, y), Color.gray, 100f, false);
        }
    }
}
