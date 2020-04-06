using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGridBlocker : MonoBehaviour
{
    // create gizmos on start
    public void changeGridStatus()
    {
        int x, y;
        GridCustom.getXYFromPosition(this.transform.position, out x, out y);

        x += GridCustom.offsetX;
        y += GridCustom.offsetY;

        // Draw gismoz on tower
        GridCustom.cells[x, y].cellContent = CellContent.TOWER;
        Debug.DrawLine(GridCustom.getWorldSpace(x, y), GridCustom.getWorldSpace(x + 1, y), Color.black, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x, y), GridCustom.getWorldSpace(x, y + 1), Color.black, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x + 1, y), GridCustom.getWorldSpace(x + 1, y + 1), Color.black, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x, y + 1), GridCustom.getWorldSpace(x + 1, y + 1), Color.black, 100f, false);
    }

    // create gizmos when destroyed
    public void removeGridStatus()
    {
        int x, y;
        GridCustom.getXYFromPosition(this.transform.position, out x, out y);

        x += GridCustom.offsetX;
        y += GridCustom.offsetY;

        // Draw gismoz on tower
        GridCustom.cells[x, y].cellContent = CellContent.OPEN_FIELD;
        Debug.DrawLine(GridCustom.getWorldSpace(x, y), GridCustom.getWorldSpace(x + 1, y), Color.green, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x, y), GridCustom.getWorldSpace(x, y + 1), Color.green, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x + 1, y), GridCustom.getWorldSpace(x + 1, y + 1), Color.green, 100f, false);
        Debug.DrawLine(GridCustom.getWorldSpace(x, y + 1), GridCustom.getWorldSpace(x + 1, y + 1), Color.green, 100f, false);
    }
}
