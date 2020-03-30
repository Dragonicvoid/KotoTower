using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGridBlocker : MonoBehaviour
{
    // create gizmos on start
    public void changeGridStatus()
    {
        int x, y;
        GridTesting.getXYFromPosition(this.transform.position, out x, out y);

        x += GridTesting.offsetX;
        y += GridTesting.offsetY;

        // Draw gismoz on tower
        GridTesting.cells[x, y].cellContent = CellContent.TOWER;
        Debug.DrawLine(GridTesting.getWorldSpace(x, y), GridTesting.getWorldSpace(x + 1, y), Color.black, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x, y), GridTesting.getWorldSpace(x, y + 1), Color.black, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x + 1, y), GridTesting.getWorldSpace(x + 1, y + 1), Color.black, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x, y + 1), GridTesting.getWorldSpace(x + 1, y + 1), Color.black, 100f, false);
    }

    // create gizmos when destroyed
    public void removeGridStatus()
    {
        int x, y;
        GridTesting.getXYFromPosition(this.transform.position, out x, out y);

        x += GridTesting.offsetX;
        y += GridTesting.offsetY;

        // Draw gismoz on tower
        GridTesting.cells[x, y].cellContent = CellContent.OPEN_FIELD;
        Debug.DrawLine(GridTesting.getWorldSpace(x, y), GridTesting.getWorldSpace(x + 1, y), Color.green, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x, y), GridTesting.getWorldSpace(x, y + 1), Color.green, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x + 1, y), GridTesting.getWorldSpace(x + 1, y + 1), Color.green, 100f, false);
        Debug.DrawLine(GridTesting.getWorldSpace(x, y + 1), GridTesting.getWorldSpace(x + 1, y + 1), Color.green, 100f, false);
    }
}
